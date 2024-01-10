using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(int damage);

    public void Knockback(Vector3 vector, float force, float timeWeight);
    void Stun(float stun);
}
