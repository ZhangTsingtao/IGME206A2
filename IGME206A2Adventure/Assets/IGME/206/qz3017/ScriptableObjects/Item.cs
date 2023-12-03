using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : ScriptableObject
{
    public string itemName = "Item";
    public int attackValue = 0;
    public int defenseValue = 0;
    public int healValue = 0;
    public ItemType itemType;
    public enum ItemType
    {
        Weapon,
        Armor,
        Charm
    }
}
