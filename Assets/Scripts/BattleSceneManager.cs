using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    public Vector3 playerBattlePosition = new Vector3(-4, 2.5f, 0); // Desired position in the battle scene

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
    }
}
