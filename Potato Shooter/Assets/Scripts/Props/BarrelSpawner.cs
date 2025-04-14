using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject barrelPrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float lastSpawnTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastSpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSpawnTime >= 20f)
        {
            SpawnBarrel();
            lastSpawnTime = Time.time;
        }
    }

    private void SpawnBarrel()
    {
        Instantiate(barrelPrefab, transform.position, Quaternion.identity);
    }
}
