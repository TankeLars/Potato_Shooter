using System.Collections.Generic;
using UnityEngine;

public class ExplodingBarrel : MonoBehaviour, IDamageable
{
    [SerializeField] public float range = 5f;
    [SerializeField] public float damage = 10f;
    [SerializeField] public float barrelHealth = 0.5f;

    List<IDamageable> m_AllDamageables = new List<IDamageable>();
    private bool hasExploded = false;

    void Start()
    {

    }

    public void DoDamage(float damage)
    {
        if (hasExploded) return;

        barrelHealth -= damage;
        if (barrelHealth <= 0)
        {
            Explode();
        }
    }

    public void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D collider in colliders)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.DoDamage(damage);
            }
        }

        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
