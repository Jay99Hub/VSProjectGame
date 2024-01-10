using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PlayerPersistentUpgrades
{
    HP,
    Damage
}

[Serializable]
public class PlayerUpgrades
{
    public PlayerPersistentUpgrades persistentUpgrades;
    public int level = 0;
    public int max_level = 10;
    public int costToUpgrade = 100;
}


[CreateAssetMenu]
public class DataContainer : ScriptableObject
{
    public int coins;

    public List<PlayerUpgrades> upgrades;

    public CharacterData selectedCharacter;

    internal int GetUpgradeLevel(PlayerPersistentUpgrades persistentUpgrade)
    {
        return upgrades[(int)persistentUpgrade].level;
    }

    public void SetSelectedCharacter(CharacterData character)
    {
        selectedCharacter = character;
    }
}
