using UnityEngine;

public class ZombieHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int debuffLevel = 0;
    [SerializeField] private float potatoesEaten = 0;
    [SerializeField] private float maxPotatoes = 5;

    Vector3 Position { get { return transform.position; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
        if (potatoesEaten >= maxPotatoes)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public Vector3 GetPosition()
    {
        return Position;
    }
}
