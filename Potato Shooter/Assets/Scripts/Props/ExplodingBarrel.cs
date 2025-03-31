using System.Collections.Generic;
using UnityEngine;

public class ExplodingBarrel : MonoBehaviour, IDamageable
{
    [SerializeField] public float range = 10f;
    [SerializeField] public float damage = 10f;
    [SerializeField] public float barrelHealth = 0.5f;
    Vector3 Position { get { return transform.position; } }

    List<IDamageable> m_AllDamageables = new List<IDamageable>();
    private bool hasExploded = false;

    void Start()
    {
        MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();
        for (int i = 0; i < allScripts.Length; i++)
        {
            if (allScripts[i] is IDamageable)
                m_AllDamageables.Add(allScripts[i] as IDamageable);
        }
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

        for (int i = 0; i < m_AllDamageables.Count; i++)
        {
            if (Vector3.Distance(m_AllDamageables[i].GetPosition(), transform.position) < range)
            {
                m_AllDamageables[i].DoDamage(damage);
            }
        }

        Destroy(gameObject);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
