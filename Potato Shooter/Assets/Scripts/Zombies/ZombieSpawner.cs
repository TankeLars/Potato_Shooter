using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public float zombieSpawnDelay = 1f; // Delay between zombie spawns (in seconds)
    private float nextSpawnTime; // Time when the next zombie should spawn
    public int zombieAmount; // Number of zombies spawned
    public int minZombiesAmount = 1; // Minimum number of zombies to spawn
    public int maxZombiesAmount = 3; // Maximum number of zombies to spawn
    public int maxZombies = 10; // Maximum number of zombies that can be spawned
    public int currentZombies = 0; // Current number of zombies spawned
    public GameObject zombiePrefab; // Reference to the zombie prefab

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextSpawnTime = Time.time + zombieSpawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnZombies();
            nextSpawnTime = Time.time + zombieSpawnDelay;
        }
    }

    void SpawnZombies()
    {
        if (currentZombies >= maxZombies)
        {
            return;
        }

        zombieAmount = Random.Range(minZombiesAmount, maxZombiesAmount + 1);

        for (int i = 0; i < zombieAmount; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            InstantiateZombie(spawnPosition);
            currentZombies++;
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 playerPosition = transform.position;
        float spawnRadius = 10f; // Radius within which zombies can spawn
        float minDistance = 5f; // Minimum distance between player and zombie spawn position

        Vector2 randomCirclePoint = Random.insideUnitCircle.normalized * (spawnRadius - minDistance);
        Vector3 spawnPosition = playerPosition + new Vector3(randomCirclePoint.x, 0f, randomCirclePoint.y);

        return spawnPosition;
    }

    void InstantiateZombie(Vector3 spawnPosition)
    {
        // Instantiate zombie prefab at the specified spawn position
        // Replace "ZombiePrefab" with the actual name of your zombie prefab
        GameObject zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
    }
}
