using UnityEngine;

public class PotatoSpawn : MonoBehaviour
{
    [SerializeField] private GameObject potatoPrefab; // Only potatoes will spawn
    [SerializeField] private float spawnInterval = 5f; // Time between spawn attempts

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= spawnInterval)
        {
            TrySpawnPotato();
            timer = 0f;
        }
    }

    void TrySpawnPotato()
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
            SpawnPoint spawnPoint = tile.GetComponent<SpawnPoint>();
            if (spawnPoint != null && !spawnPoint.HasPotato)
            {
                //Debug.Log("Spawning potato at " + tile.transform.position);
                Instantiate(potatoPrefab, tile.transform.position, Quaternion.identity);
                return; // Exit after successful spawn
            }
            Debug.Log("Position blocked at " + tile.transform.position);
        }
        
        Debug.Log("No available spawn positions found");
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