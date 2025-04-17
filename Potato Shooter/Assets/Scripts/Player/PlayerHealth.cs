using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float maxHealth;
    public float currentHealth;
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
    }

    void TakeDamage(float damageAmount)
    {
        StartCoroutine(FlashRoutine());
        currentHealth -= damageAmount;
        if(currentHealth < 0)
        {
            currentHealth = 0f;
            OnDeath();
        }
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.color = new Color(1f, 0.392f, 0.392f, 1f); // Change color to red
        yield return new WaitForSeconds(0.2f); // Wait for 0.2s
        spriteRenderer.color = new Color(1, 1, 1, 1); // Change color to normal

    }

    void OnDeath()
    {
        Destroy(gameObject);
        GameManager.Instance.ShowDeathScreen();
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

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void DoDamage(float damage)
    {
        TakeDamage(damage);
    }
}