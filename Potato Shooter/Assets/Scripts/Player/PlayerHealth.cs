using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float maxHealth;
    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth <= 0)
        {
            currentHealth = 0f;
            OnDeath();
        }
    }

    void OnDeath()
    {
        Destroy(gameObject);
        GameManager.Instance.ShowDeathScreen();
    }


    public void AddBonusHealth(int bonus)
        {
            maxHealth += bonus;
            currentHealth += bonus;
        }


    public float getHealth()
    {
        return Mathf.Round(currentHealth * 10) / 10;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void DoDamage(float damage)
    {
        TakeDamage(damage);
    }
}