using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class PlayerStatus
{
    public static int LevelID = 0;
    public static int PlayerHealth = 10;
    public static int PlayerMaxHealth = 10;
    public static int PlayerHealValue = 2;
    public static int PlayerAttackValue = 2;
    public static int PlayerDefenseValue = 1;

    public static List<Item> PlayerItems = new List<Item>();


    public static (bool, string) TakeDamage(int attackValue)
    {
        if (attackValue <= PlayerDefenseValue) return (true, "Player's armor blocked enemy's attack");

        PlayerHealth -= (attackValue - PlayerDefenseValue);

        if (PlayerHealth > 0)
        {
            return (true, "Player is damaged by " + (attackValue - PlayerDefenseValue).ToString() + " points \n");
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
    public static void RemoveItem(Item item)
    {
        if (!PlayerItems.Contains(item)) return;
        PlayerItems.Remove(item);
    }
    public static void EquipItem(Item item)
    {
        if (!PlayerItems.Contains(item)) return;
    }

    public static void NextLevel()
    {
        LevelID++;
    }

    public static string DisplayStatus()
    {
        return "HP: " + PlayerHealth + " / " + PlayerMaxHealth+ "\n" +
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
                             "Attack Bonus: " + PlayerItems[i].attackValue + "    Defense Bonus: " + PlayerItems[i].defenseValue + "\n\n";

        }


        return (inventoryList, PlayerItems.Count);
    }

    public static void ResetStatus()
    {
        PlayerItems.Clear();
        LevelID = 0;
        PlayerHealth = 10;
        PlayerMaxHealth = 10;
        PlayerHealValue = 2;
        PlayerAttackValue = 2;
        PlayerDefenseValue = 1;
    }
}
