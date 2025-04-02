using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float movementDebuff;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackCooldownTimer;

    [SerializeField] private GameObject player;

    [SerializeField] private bool canAttack = true;
    [SerializeField] private bool isPlayerInRange = false;

    private SpriteRenderer spriteRenderer;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player GameObject
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component

    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange) // Only move if the player is within range
        {
            MoveToPlayer();
        }
    }

    private void MoveToPlayer()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > attackRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
            }
            else
            {
                if (canAttack)
                {
                    IDamageable damageable = player.GetComponent<IDamageable>();
                    damageable.DoDamage(attackDamage);
                }
            }
        }

        // Flip the sprite horizontally if the player is on the left
        if (player != null && player.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

}
