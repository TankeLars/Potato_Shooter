using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    [SerializeField] private int debuffLevel = 0;
    [SerializeField] private int potatoesEaten = 0;
    [SerializeField] private int maxPotatoes = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EatPotatoes()
    {
        potatoesEaten ++; // add the potatoes to the counter
        if (potatoesEaten >= maxPotatoes)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
