using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Basic stats for the player
    public int health = 100;
    public int mana = 50;
    public int attack = 10;
    public int defense = 5;
    public int level = 1;
    public int experience = 0;

    // Optionally add methods to modify stats
    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Max(0, health); // Ensure health doesn't go below 0
        Debug.Log($"Player took {damage} damage. Current health: {health}");
    }

    public void Heal(int amount)
    {
        health += amount;
        Debug.Log($"Player healed {amount}. Current health: {health}");
    }
}
