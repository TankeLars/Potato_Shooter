using UnityEngine;
public interface IDamageable
{
    Vector3 GetPosition();
    void DoDamage(float damage);
}
