using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackCooldownTimer;

    [SerializeField] private GameObject player;

    [SerializeField] private bool isPlayerInRange = false;

    [SerializeField] public int debuffLevel;

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
        debuffLevel = GetComponent<ZombieHealth>().debuffLevel; // Get the debuffLevel component
        if (isPlayerInRange) // Only move if the player is within range
        {
            MoveToPlayer();
        }
        CheckForTeleport();

    }

    private void MoveToPlayer()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > attackRange)
            {
                float currentMovementSpeed = movementSpeed;

                if (debuffLevel == 1)
                {
                    currentMovementSpeed *= 0.75f; // Move 25% slower at debuff level 1
                }
                else if (debuffLevel == 2)
                {
                    currentMovementSpeed *= 0.5f; // Move 50% slower at debuff level 2
                }

                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, currentMovementSpeed * Time.deltaTime);
            }
            else
            {
                if (attackCooldownTimer <= 0 && Vector3.Distance(transform.position, player.transform.position) <= attackRange)
                {
                    IDamageable damageable = player.GetComponent<IDamageable>();

                    if (debuffLevel == 0)
                    {
                        damageable.DoDamage(attackDamage);
                        attackCooldownTimer = attackCooldown;
                    }
                    else if (debuffLevel == 1)
                    {
                        damageable.DoDamage(attackDamage * 2);
                        attackCooldownTimer = attackCooldown;
                    }
                    else if (debuffLevel == 2)
                    {
                        damageable.DoDamage(attackDamage * 3);
                        attackCooldownTimer = attackCooldown;
                    }
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

        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, attackRange);
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

    private void CheckForTeleport()
    {
        if (transform.position.x > 50)
        {
            transform.position = new Vector3(-48, transform.position.y, 0);
            /*            cinemachineCamera.GetComponent<CinemachineCamera>().enabled = false;
                        cinemachineCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -5.43f);
                        cinemachineCamera.GetComponent<CinemachineCamera>().enabled = true;*/

        }
        else if (transform.position.x < -48)
        {
            transform.position = new Vector3(50, transform.localPosition.y, 0);
            /*            cinemachineCamera.GetComponent<CinemachineFollow>().enabled = false;
                        cinemachineCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -5.43f);
                        cinemachineCamera.GetComponent<CinemachineCamera>().enabled = true;*/

        }

        if (transform.position.y > 50)
        {
            transform.position = new Vector3(transform.position.x, -26, 0);
            /*            cinemachineCamera.GetComponent<CinemachineCamera>().enabled = false;
                        cinemachineCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -5.43f);
                        cinemachineCamera.GetComponent<CinemachineCamera>().enabled = true;*/

        }
        else if (transform.position.y < -26)
        {
            transform.position = new Vector3(transform.localPosition.x, 50, 0);
            /*            cinemachineCamera.GetComponent<CinemachineFollow>().enabled = false;
                        cinemachineCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -5.43f);
                        cinemachineCamera.GetComponent<CinemachineCamera>().enabled = true;*/

        }
    }

}
