using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject barrelPrefab;
    [SerializeField] private float spawnInterval = 20f;
    [SerializeField] private float lastSpawnTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastSpawnTime = Time.time;
        SpawnBarrel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSpawnTime >= spawnInterval)
        {
            SpawnBarrel();
            lastSpawnTime = Time.time;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
    }

    private void SpawnBarrel()
    {
        Instantiate(barrelPrefab, transform.position, Quaternion.identity);
    }
}
