using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    public Vector3 playerBattlePosition = new Vector3(-4, 2.5f, 0); // Desired position in the battle scene
    public GameObject[] enemyPrefabs; // Array of enemy prefabs
    public Transform[] spawnPoints;  // Array of spawn points

    void Start()
    {
        // Check if the player GameObject exists
        PlayerStats player = FindObjectOfType<PlayerStats>();
        if (player != null)
        {
            // Move the player to the battle position
            player.transform.position = playerBattlePosition;
        }
        else
        {
            Debug.LogError("Player GameObject not found in the scene!");
        }

        RandomizeEnemyEncounter();
    }

    private void RandomizeEnemyEncounter()
    {
        // Randomize the number of enemies (e.g., between 1 and 3)
        int enemyCount = Random.Range(1, spawnPoints.Length + 1);

        // Spawn enemies
        for (int i = 0; i < enemyCount; i++)
        {
            // Randomly pick an enemy prefab
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // Instantiate the enemy at a spawn point
            Instantiate(enemyPrefab, spawnPoints[i].position, Quaternion.identity);
        }
    }
}
