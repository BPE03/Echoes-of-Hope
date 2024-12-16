using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    public GameObject enemy; // Drag and drop the Enemy GameObject in the Inspector
    public GameObject player;
    // Enum to define different phases of the battle
    public enum BattleState { START, PLAYER_TURN, ENEMY_TURN, WON, LOST }

    // Variable to track the current state
    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the battle state to START
        state = BattleState.START;

        // Start the battle sequence
        StartCoroutine(SetupBattle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SetupBattle()
    {
        // Placeholder: Add logic to initialize the battle scene, like positioning characters
        Debug.Log("Battle is starting!");

        yield return new WaitForSeconds(2);

        // Transition to the player's turn after setup
        state = BattleState.PLAYER_TURN;

        // Call a function to handle the player's turn
        PlayerTurn();
    }

    void PlayerTurn()
    {
        Debug.Log("Player's Turn! Choose an action.");
    }
    IEnumerator EnemyTurn()
    {
        Debug.Log("Enemy's Turn!");

        yield return new WaitForSeconds(1);

        player.TakeDamage(15); // Example damage value
        Debug.Log("Enemy attacks!");

        if (player.currentHealth > 0)
        {
            state = BattleState.PLAYER_TURN;
            PlayerTurn();
        }
        else
        {
            state = BattleState.LOST;
            Debug.Log("You lost the battle!");
        }
    }
    public void OnPlayerActionComplete()
    {
        // After the player's action, transition to the enemy's turn
        state = BattleState.ENEMY_TURN;

        // Start the enemy's turn
        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYER_TURN) return;

        Debug.Log("Player attacks!");
        enemy.TakeDamage(20); // Example damage value
        EndPlayerTurn();
    }

    public void OnDefendButton()
    {
        if (state != BattleState.PLAYER_TURN) return;

        Debug.Log("Player defends!");
        // Implement defense logic here (e.g., reduce damage from the next enemy attack)
        EndPlayerTurn();
    }

    public void OnItemButton()
    {
        if (state != BattleState.PLAYER_TURN) return;

        Debug.Log("Player uses an item!");
        // Implement item usage logic here (e.g., healing or buffs)
        EndPlayerTurn();
    }

    public void OnFleeButton()
    {
        if (state != BattleState.PLAYER_TURN) return;

        Debug.Log("Player fled!");
        // Implement item usage logic here (e.g., healing or buffs)
        EndPlayerTurn();
    }

    void EndPlayerTurn()
    {
        state = BattleState.ENEMY_TURN;
        StartCoroutine(EnemyTurn());
    }

}
