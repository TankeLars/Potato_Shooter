using UnityEngine;

public class ZombieHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int debuffLevel = 0;
    [SerializeField] private float potatoesEaten = 0;
    [SerializeField] private float maxPotatoes = 5;
    [SerializeField] private GameObject player;

    [SerializeField] private Animator animator;


    Vector3 Position { get { return transform.position; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player GameObject
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void DoDamage(float damage)
    {
        EatPotatoes(damage);
    }

    public void EatPotatoes(float potatoes)
    {
        potatoesEaten += potatoes; // add the potatoes to the counter
        if (potatoesEaten >= maxPotatoes * 0.2f && debuffLevel == 0)
        {
            Debug.Log("Chonk 1");
            animator.SetTrigger("GoChonk1");
            debuffLevel = 1;
        }

        if (potatoesEaten >= maxPotatoes)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        ZombieSpawner zombieSpawner = player.GetComponent<ZombieSpawner>();
        zombieSpawner.currentZombies--;
    }

    public Vector3 GetPosition()
    {
        return Position;
    }
}
