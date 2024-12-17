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
    public int currentHealth;

    private const string HEALTH_KEY = "PlayerHealth"; // Key untuk menyimpan health di PlayerPrefs

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
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth); // Ensure health doesn't go below 0
        Debug.Log($"Player took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
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
