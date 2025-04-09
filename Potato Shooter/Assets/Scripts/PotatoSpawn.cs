using UnityEngine;

public class PotatoSpawn : MonoBehaviour
{
    [SerializeField] private GameObject itemToSpawn; // Only one item type to spawn
    [SerializeField] private float spawnInterval = 5f; // Time between spawn attempts
    [SerializeField] private LayerMask spawnBlockingLayers; // Layers that block spawning
    [SerializeField] private Vector2 spawnCheckSize = Vector2.one; // Area to check for existing items

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= spawnInterval)
        {
            TrySpawnItem();
            timer = 0f;
        }
    }

    void TrySpawnItem()
    {
        GameObject[] spawnTiles = GameObject.FindGameObjectsWithTag("AmmoSpawn");
        
        if (spawnTiles.Length == 0)
        {
            Debug.LogWarning("No spawn tiles found with tag 'AmmoSpawn'");
            return;
        }

        // Shuffle the spawn tiles array to get random order
        ShuffleArray(spawnTiles);

        foreach (GameObject tile in spawnTiles)
        {
            if (!IsPositionBlocked(tile.transform.position))
            {
                Debug.Log("spawning potato");
                Instantiate(itemToSpawn, tile.transform.position, Quaternion.identity);
                return; // Exit after successful spawn
            }
            Debug.Log("blocked");
        }
        
        Debug.Log("No available spawn positions found");
    }

    bool IsPositionBlocked(Vector2 position)
    {
        // Check if there's already an item at this position
        Collider2D hit = Physics2D.OverlapBox(position, spawnCheckSize, 0f, spawnBlockingLayers);
        return hit != null;
    }

    // Fisher-Yates shuffle algorithm
    void ShuffleArray(GameObject[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

}