using UnityEngine;
using System.Collections;

public class ZombieHealth : MonoBehaviour, IDamageable
{
    [SerializeField] public int debuffLevel = 0;
    [SerializeField] private float potatoesEaten = 0;
    [SerializeField] private float maxPotatoes = 5;
    [SerializeField] private int xpOnDeath = 25;
    [SerializeField] private GameObject player;

    [SerializeField] private Animator animator;

    [SerializeField] SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component


    Vector3 Position { get { return transform.position; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player GameObject
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(spriteRenderer.color);
    }

    public void DoDamage(float damage)
    {
        StartCoroutine(FlashRoutine());
        EatPotatoes(damage);

    }
    private IEnumerator FlashRoutine()
    {
        Color normalColor = spriteRenderer.color; // Store the current color
        spriteRenderer.color = new Color(1f, 0.392f, 0.392f, 1f); // Change color to red
        yield return new WaitForSeconds(0.2f); // Wait for 0.2s
        spriteRenderer.color = normalColor; // Change color back to normal

    }

    public void EatPotatoes(float potatoes)
    {
        potatoesEaten += potatoes; // add the potatoes to the counter

        if (potatoesEaten > maxPotatoes * 0.2f && potatoesEaten < maxPotatoes * 0.65f && debuffLevel == 0)
        {
            Debug.Log("Chonk 1");
            animator.SetTrigger("GoChonk1");
            debuffLevel = 1;
        }

        if (potatoesEaten >= maxPotatoes * 0.65f && debuffLevel == 1)
        {
            Debug.Log("Chonk 2");
            animator.SetTrigger("GoChonk2");
            debuffLevel = 2;
        }

        if (potatoesEaten >= maxPotatoes)
        {
            Die();
        }
    }

    private void Die()
    {
        PlayerLevel.Instance.AddXP(xpOnDeath);
        Destroy(gameObject);
        ZombieSpawner zombieSpawner = player.GetComponent<ZombieSpawner>();
        zombieSpawner.currentZombies--;
    }

    public Vector3 GetPosition()
    {
        return Position;
    }
}
