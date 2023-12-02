using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlayerStatus
{
    public static int LevelID = 0;
    public static int PlayerHealth = 10;
    public static int PlayerMaxHealth = 10;
    public static int PlayerHealValue = 2;
    public static int PlayerAttackValue = 2;
    public static int PlayerDefenseValue = 1;

    public static List<Item> playerItems = new List<Item>();
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
    public static void GetItem(Item item)
    {
        if (item == null) return;
        playerItems.Add(item);
    }
    public static void RemoveItem(Item item)
    {
        if (!playerItems.Contains(item)) return;
        playerItems.Remove(item);
    }
    public static void EquipItem(Item item)
    {
        if (!playerItems.Contains(item)) return;
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

    public static void ResetStatus()
    {
        LevelID = 0;
        PlayerHealth = 10;
        PlayerMaxHealth = 10;
        PlayerHealValue = 2;
        PlayerAttackValue = 2;
        PlayerDefenseValue = 1;
    }
}
