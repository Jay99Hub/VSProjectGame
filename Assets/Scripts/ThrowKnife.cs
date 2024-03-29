using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowKnife : WeaponBase
{
  

    [SerializeField] PoolObjectData knifePrefab;
    [SerializeField] float spread = 0.5f;


    public override void Attack()
    {
        UpdateVectorOfAttack();
        for(int i = 0; i < weaponStats.numberOfAttacks; i++)
        {

            Vector3 newKnifePosition = transform.position;
            if(weaponStats.numberOfAttacks > 1)
            {
                newKnifePosition.y -= (spread * (weaponStats.numberOfAttacks-1)) / 2; //calculating offset
                newKnifePosition.y += i * spread; //spreading the knives along line
            }

            SpawnProjectile(knifePrefab, newKnifePosition);
        }
        
    }
}
