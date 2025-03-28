using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth < 0)
        {
            currentHealth = 0f;
            OnDeath();
        }
    }

    void OnDeath()
    {
        //handle death here
    }

    void Heal(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public float getHealth()
    {
        return Mathf.Round(currentHealth * 10) / 10;
    }
}
