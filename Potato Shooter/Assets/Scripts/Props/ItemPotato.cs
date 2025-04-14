using UnityEngine;

public class ItemPotato : MonoBehaviour
{
    [SerializeField] private int potatoValue;
    [SerializeField] private float hoverHeight = 0.1f; // How high it moves
    [SerializeField] private float hoverSpeed = 1f;    // How fast it bobs
    [SerializeField] private int steps = 5;

    private PlayerShoot playerShoot;
    private Vector3 startPosition;
    private float randomOffset; // Makes potatoes hover out of sync

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerShoot = player.GetComponent<PlayerShoot>();
        }
        
        startPosition = transform.position;
        randomOffset = Random.Range(0f, 2f * Mathf.PI); // Random starting point in hover cycle
    }

    void Update()
{
    // Calculate raw sine wave
    float rawSine = Mathf.Sin((Time.time + randomOffset) * hoverSpeed);

    // Quantize it to create stair-like motion
    float steppedSine = Mathf.Round(rawSine * steps) / steps;

    float hover = steppedSine * hoverHeight;
    transform.position = startPosition + Vector3.up * hover;
}


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerShoot != null)
            {
                playerShoot.PotatoPickup(potatoValue);
            }
            Destroy(gameObject);
        }
    }
}