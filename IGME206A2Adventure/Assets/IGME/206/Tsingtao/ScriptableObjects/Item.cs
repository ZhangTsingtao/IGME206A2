using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 2)]
public class Item : ScriptableObject
{
    public string itemName = "Item";
    public int hpValue = 0;
    public int attackValue = 0;
    public int defenseValue = 0;

}
