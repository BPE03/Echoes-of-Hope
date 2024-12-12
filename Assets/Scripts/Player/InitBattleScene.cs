using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitBattleScene : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object collided with is tagged as "Enemy"
        if (collision.gameObject.CompareTag("Enemies"))
        {
            // Save any data if needed before transitioning (e.g., player state)
            // For example: PlayerPrefs.SetInt("HP", playerHealth);

            // Load the battle scene
            SceneManager.LoadScene("BattleScene1"); // Replace with the actual name of your scene
        }
    }
}
