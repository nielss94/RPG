using UnityEngine;

public interface IDamageable
{
    void TakeDamage(Damage damage, Character source);
    void Die();
    void KnockBack(Vector2 direction);
}