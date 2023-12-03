using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPC : MonoBehaviour
{
    [Header("Assign")]
    public int hp = 5;
    public int attackValue = 1;
    public int defenseValue = 0;
    public Item item;

    [Header("No Assign")]
    public bool Alive = true;

    private void Start()
    {
        Alive = true;
    }
    public string TakeDamage(int attackValue)
    {
        if (attackValue <= defenseValue) return "Enemy's armor too heavy, the attack is in vain!";

        hp -= (attackValue - defenseValue);

        if (hp > 0)
        {
            return "Enemy is damaged by " + (attackValue - defenseValue).ToString() + " points \n" +
                   "Their Defense is " + defenseValue.ToString() + "\n" +
                   "Their HP is " + hp.ToString();
        }

        else
        {
            Alive = false;
            return "Enemy is defeated";
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
