using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DirectionOfAttack
{
    None,
    Foward,
    LeftRight,
    UpDown
}

public abstract class WeaponBase : MonoBehaviour
{
    public PlayerMove playerMove;

    public WeaponData weaponData;

    public WeaponStats weaponStats;

    
    float timer;

    Character wielder;
    public Vector2 vectorOfAttack;
    [SerializeField] DirectionOfAttack attackDirection;

    PoolManager poolManager;

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }


    public void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            Attack();
            timer = weaponStats.timeToAttack;
        }
    }

    public void ApplyDamage(Collider2D[] colliders)
    {
        int damage = GetDamage();
        for (int i = 0; i < colliders.Length; i++)
        {
            IDamageable e = colliders[i].GetComponent<IDamageable>();
            if (e != null)
            {
                ApplyDamage(colliders[i].transform.position, damage, e);
            }

        }
    }

    public void ApplyDamage(Vector3 position, int damage, IDamageable e)
    {
        PostDamage(damage, position);
        e.TakeDamage(damage);
        ApplyAdditionalEffects(e, position);
    }

    private void ApplyAdditionalEffects(IDamageable e, Vector3 enemyPosition)
    {
        e.Stun(weaponStats.stun);
        e.Knockback((enemyPosition - transform.position).normalized, weaponStats.knockback, weaponStats.knockbackTimeWeight);
    }

    public virtual void SetData(WeaponData wd)
    {
        weaponData = wd;
        

        weaponStats = new WeaponStats(wd.stats);
    }

    public void SetPoolManager(PoolManager poolManager)
    {
        this.poolManager = poolManager;
    }

    public abstract void Attack();

    public int GetDamage()
    {
        int damage = (int)(weaponData.stats.damage * wielder.damageBonus);
        return damage;
    }

    public virtual void PostDamage(int damage, Vector3 targetPosition)
    {
        MessageSysten.instance.PostMessage(damage.ToString(), targetPosition);
    }

    public void AddOwnerCharacter(Character character)
    {
        wielder = character;
    }

    public void Upgrade(UpgradeData upgradeData)
    {
        weaponStats.Sum(upgradeData.weaponUpgradeStats);
    }

    public void UpdateVectorOfAttack()
    {
        if(attackDirection == DirectionOfAttack.None)
        {
            vectorOfAttack = Vector2.zero;
            return;
        }

        switch (attackDirection)
        {
            case DirectionOfAttack.Foward:
                vectorOfAttack.x = playerMove.lastHorizontalCoupledVector;
                vectorOfAttack.y = playerMove.lastVerticalCoupledVector;
                break;
            case DirectionOfAttack.LeftRight:
                vectorOfAttack.x = playerMove.lastHorizontalDeCoupledVector;
                vectorOfAttack.y = 0f;
                break;
            case DirectionOfAttack.UpDown:
                vectorOfAttack.x = 0f;
                vectorOfAttack.y = playerMove.lastVerticalDeCoupledVector;
                break;
        }
        vectorOfAttack = vectorOfAttack.normalized;

    }

    public GameObject SpawnProjectile(PoolObjectData poolObjectData, Vector3 position)
    {
        GameObject projectileGO = poolManager.GetObject(poolObjectData);

        projectileGO.transform.position = position;

        Projectile projectile = projectileGO.GetComponent<Projectile>();
        projectile.SetDirection(vectorOfAttack.x, vectorOfAttack.y);
        projectile.SetStats(this);

        return projectileGO;
    }


}
