using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class PlayerStatus
{

    private const int startAttackValue = 2;
    private const int startDefenseValue = 1;
    private const int startHealValue = 1;
    private const int startMaxHealth = 10;

    public static int LevelID = 0;
    public static int PlayerHealth = startMaxHealth;
    public static int PlayerMaxHealth = startMaxHealth;
    public static int PlayerHealValue = startHealValue;
    public static int PlayerAttackValue = startAttackValue;
    public static int PlayerDefenseValue = startDefenseValue;

    public static List<Item> PlayerItems = new List<Item>();
    public static Dictionary<Item.ItemType, Item> EquipedItems = new Dictionary<Item.ItemType, Item> 
    {
        { Item.ItemType.Weapon, null },
        { Item.ItemType.Armor, null },
        { Item.ItemType.Charm, null },
    };

    public static Item weaponItem = null;
    public static Item armorItem = null;
    public static Item charmItem = null;    

    public static (bool, string) TakeDamage(int attackValue)
    {
        if (attackValue <= PlayerDefenseValue) return (true, "Player's armor blocked enemy's attack");

        PlayerHealth -= (attackValue - PlayerDefenseValue);
        PlayerHealth += PlayerHealValue;
        if (PlayerHealth > PlayerMaxHealth) PlayerHealth = PlayerMaxHealth;
        if (PlayerHealth > 0)
        {
            return (true, "Player is damaged by " + (attackValue - PlayerDefenseValue).ToString() + " points \n Player recovers " + PlayerHealValue + " points \n");
        }

        else
        {
            return (false, "Player is defeated!");
        }
    }
    public static string GetItem(Item item)
    {
        if (item == null) return "Nothing found";
        
        PlayerItems.Add(item);
        return "Loot a new item " + item.itemName + "!";
    }
    public static string ShowEquippedItem()
    {
        string equippedText = "";
        foreach (Item.ItemType type in Enum.GetValues(typeof(Item.ItemType)))
        {
            equippedText += type.ToString() + ": ";
            
            if (EquipedItems[type] == null)
            {
                equippedText += "Empty\n"; 
                continue;
            }

            equippedText += EquipedItems[type].itemName.ToString() + "\n";
        }
        return equippedText;
    }

    public static void EquipItem(Item item)
    {
        Debug.Log("Equip " + item.itemName);
        if (!PlayerItems.Contains(item)) return;
        EquipedItems[item.itemType] = item;
        CountItemBonus();
    }
    public static void UnequipItem(Item item)
    {
        Debug.Log("Unequip " + item.itemName);
        if (!PlayerItems.Contains(item)) return;
        if (EquipedItems[item.itemType] != item) return;

        //PlayerAttackValue -= item.attackValue;
        //PlayerDefenseValue -= item.defenseValue;
        //PlayerHealValue -= item.healValue;
        EquipedItems[item.itemType] = null;
        CountItemBonus();
    }
    public static void CountItemBonus()
    {
        PlayerAttackValue = startAttackValue;
        PlayerDefenseValue = startDefenseValue;
        PlayerHealValue = startHealValue;
        PlayerMaxHealth = startMaxHealth;

        foreach (Item.ItemType type in Enum.GetValues(typeof(Item.ItemType)))
        {
            if (EquipedItems[type] == null) continue;

            PlayerAttackValue += EquipedItems[type].attackValue;
            PlayerDefenseValue += EquipedItems[type].defenseValue;
            PlayerHealValue += EquipedItems[type].healValue;

            PlayerMaxHealth += EquipedItems[type].healValue;
        }
    }

    public static void NextLevel()
    {
        LevelID++;
    }

    public static string DisplayStatus()
    {
        return "HP: " + PlayerHealth + " / " + PlayerMaxHealth + "\n" +
               "Attack: " + PlayerAttackValue + "\n" +
               "Defense: " + PlayerDefenseValue + "\n" +
               "Heal: " + PlayerHealValue;
    }
    public static (string, int) DisplayInventory(Vector3 infoTextPosition) 
    {
        if (PlayerItems.Count == 0) return ("There's nothing in Inventory", 0);

        string inventoryList = "";

        for (int i = 0; i < PlayerItems.Count; i++)
        {
            inventoryList += i+1 + ". " + PlayerItems[i].itemName + "\n" + 
                             "Type: " + PlayerItems[i].itemType + "   Attack: " + PlayerItems[i].attackValue + "    Defense: " + PlayerItems[i].defenseValue + "    Heal: " + PlayerItems[i].healValue + "\n\n";

        }

        return (inventoryList, PlayerItems.Count);
    }

    public static void ResetStatus()
    {
        PlayerItems.Clear();
        LevelID = 0;
        PlayerHealth = startMaxHealth;
        PlayerMaxHealth = startMaxHealth;
        PlayerHealValue = startHealValue;
        PlayerAttackValue = startAttackValue;
        PlayerDefenseValue = startDefenseValue;
    }
}
