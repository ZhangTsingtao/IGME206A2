using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 2)]
public class Item : ScriptableObject
{
    public string itemName = "Item";

    public int attackValue = 0;
    public int defenseValue = 0;

    public void EquipItem()
    {
        Debug.Log("Item " + itemName + " equipped!");
    }
    public void UnequipItem()
    {
        Debug.Log("Item " + itemName + " unequipped!");
    }

}
