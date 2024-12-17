using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    // Basic stats for the enemy
    public int health = 0;
    public int mana = 0;
    public int attack = 0;
    public int defense = 0;
    public int level = 0;
    public int experience = 0;
    public int currentHealth = 0;

    void Start()
    {
        currentHealth = health;
    }

    // Optionally add methods to modify stats
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth); // Ensure health doesn't go below 0
        Debug.Log($"Enemy took {damage} damage!. Current health: {currentHealth}");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy defeated!");
        // Add death logic (e.g., destroy the enemy or play an animation)
    }
}
