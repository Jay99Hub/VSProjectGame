using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickUpObject : MonoBehaviour, IPickUpObject
{
    [SerializeField] int healAmout;

    public void OnPickUp(Character character)
    {
        character.Heal(healAmout);
    }
}
