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
    public GameObject normalZombie; // Reference to the zombie prefab
    public GameObject crawlerZombie; // Reference to the crawler zombie prefab
    public GameObject buffZombie; // Reference to the buff zombie prefab
    public float spawnRadius = 25f; // Radius within which zombies can spawn
    public float minDistance = 10f; // Minimum distance between player and zombie spawn position

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Make the ZombieSpawner instance available from any script
        DontDestroyOnLoad(gameObject);

    }

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

        Vector2 randomCirclePoint = Random.insideUnitCircle.normalized * (spawnRadius - minDistance);
        Vector3 spawnPosition = playerPosition + new Vector3(randomCirclePoint.x, randomCirclePoint.y, 0f);
        return spawnPosition;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minDistance);

    }

    void InstantiateZombie(Vector3 spawnPosition)
    {
        // Choose a random zombie prefab from the available options
        int randomIndex = Random.Range(0, 10);
        GameObject selectedPrefab = null;

        if (randomIndex < 5)
        {
            selectedPrefab = normalZombie;
        }
        else if (randomIndex < 8)
        {
            selectedPrefab = crawlerZombie;
        }
        else
        {
            selectedPrefab = buffZombie;
        }

        // Instantiate the selected zombie prefab at the specified spawn position
        GameObject zombie = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
    }
}
