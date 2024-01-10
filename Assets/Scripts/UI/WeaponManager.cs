using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform weaponObjectContainer;
    [SerializeField] PoolManager poolManager;

    List<WeaponBase> weapons;

    Character character;

    private void Awake()
    {
        weapons = new List<WeaponBase>();
        character = GetComponent<Character>();
    }


    public void AddWeapon(WeaponData weaponData)
    {
        GameObject weaponGameObject = Instantiate(weaponData.weaponBasePrefab, weaponObjectContainer);

        WeaponBase weaponBase = weaponGameObject.GetComponent<WeaponBase>();

        weaponBase.SetData(weaponData);
        weaponBase.SetPoolManager(poolManager);
        weapons.Add(weaponBase);
        weaponBase.AddOwnerCharacter(character);
        
        Level level = GetComponent<Level>();
        if(level != null)
        {
            level.AddUpgradesIntoTheListOfAvaiableUpgrades(weaponData.upgrades);
        }
    }

    internal void UpgradeWeapon(UpgradeData upgradeData)
    {
        WeaponBase weaponToUpgrade = weapons.Find(wd => wd.weaponData == upgradeData.weaponData);
        weaponToUpgrade.Upgrade(upgradeData);
    }
}
