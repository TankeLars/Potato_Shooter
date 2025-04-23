using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float maxHealth;
    public float currentHealth;
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    public Color normalColor; // Variable to store the normal color of the sprite

    [SerializeField]
    private AudioClip hurtSound;


    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        normalColor = spriteRenderer.color; // Store the current color

    }

    void TakeDamage(float damageAmount)
    {
        StartCoroutine(FlashRoutine());
        currentHealth -= damageAmount;
        if(hurtSound != null)
        {
            audioSource.PlayOneShot(hurtSound);
        }
        if(currentHealth <= 0)
        {
            currentHealth = 0f;
            OnDeath();
        }
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.color = new Color(1f, 0.392f, 0.392f, 1f); // Change color to red
        yield return new WaitForSeconds(0.2f); // Wait for 0.2s
        spriteRenderer.color = normalColor; // Change color to normal

    }

    void OnDeath()
    {
        GameData.Instance.isDead = true;
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