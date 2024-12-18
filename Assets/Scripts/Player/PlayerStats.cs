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

    public float criticalChance = 0.2f; // 20% chance for a critical hit
    public float criticalDamage = 2.0f; // Critical hits deal 2x damage

    public int currentHealth;

    private const string HEALTH_KEY = "PlayerHealth"; // Key untuk menyimpan health di PlayerPrefs

    public int temporaryDefense = 0;
    public int totalDefense = 0;
    void Start()
    {
        // Load saved health saat game dimulai, jika ada
        if (PlayerPrefs.HasKey(HEALTH_KEY))
        {
            currentHealth = PlayerPrefs.GetInt(HEALTH_KEY);
            Debug.Log($"Health Loaded: {currentHealth}");
        }
        else
        {
            currentHealth = health; // Default health
        }
    }

    // Optionally add methods to modify stats
    public void TakeDamage(int damage)
    {
        totalDefense = defense + temporaryDefense;
        int actualDamage = damage - totalDefense;
        if(actualDamage <= 0)
        {
            actualDamage = 1;
        }
        currentHealth -= actualDamage;
        currentHealth = Mathf.Max(0, currentHealth); // Ensure health doesn't go below 0
        Debug.Log($"Player took {actualDamage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
        temporaryDefense = 0;
    }

    void Die()
    {
        Debug.Log("Player is defeated!");
        // Add player death logic here
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(health, currentHealth); // Ensure health doesn't exceed max health
        Debug.Log($"Player healed {amount}. Current health: {currentHealth}");
    }

    public void GainExp(int amount)
    {
        experience += amount;
        Debug.Log($"Player gained {amount} Experience");
        if(experience >= 100 * level)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        health += health / 5;
        attack += attack / 10;
        defense += defense / 10;
        mana += mana / 5;
        level += 1;
        experience = experience % (100 * (level - 1));
        if (experience >= 100 * level)
        {
            LevelUp();
        } else
        {
            Debug.Log($"Player levelled up! current level: {level}");
        }
    }

    public void Defense()
    {
        Debug.Log($"Player defended!");
        temporaryDefense += defense / 5;
    }

    // Fungsi untuk menyimpan health pemain
    public void SaveHealth()
    {
        Debug.Log($"Saving Health: {currentHealth}");
        PlayerPrefs.SetInt(HEALTH_KEY, currentHealth);
        PlayerPrefs.Save();
        Debug.Log($"Health Saved: {currentHealth}");
    }
}
