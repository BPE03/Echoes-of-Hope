using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [SerializeField] 
    public Vector3 playerBattlePosition = new Vector3(-4, 2.5f, 0); // Desired position in the battle scene

    public List<GameObject> activeEnemies = new List<GameObject>();
    private GameObject selectedEnemy;

    public GameObject[] enemyPrefabs; // Array of enemy prefabs
    public Transform[] spawnPoints;  // Array of spawn points

    public PlayerStats playerStats;

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

    private void RandomizeEnemyEncounter()
    {
        activeEnemies.Clear(); // Clear previous enemies, if any
        // Randomize the number of enemies (e.g., between 1 and 3)
        int enemyCount = Random.Range(1, spawnPoints.Length + 1);

        // Spawn enemies
        for (int i = 0; i < enemyCount; i++)
        {
            // Randomly pick an enemy prefab
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // Instantiate the enemy at a spawn point
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoints[i].position, Quaternion.identity);

            activeEnemies.Add(newEnemy); // Add to active enemies list
        }
    }

    IEnumerator SetupBattle()
    {
        // Placeholder: Add logic to initialize the battle scene, like positioning characters
        // Check if the player GameObject exists
        playerStats = FindObjectOfType<PlayerStats>();
        if (playerStats != null)
        {
            // Move the player to the battle position
            playerStats.transform.position = playerBattlePosition;
        }
        else
        {
            Debug.LogError("Player GameObject not found in the scene!");
        }

        RandomizeEnemyEncounter();
        Debug.Log("Battle is starting!");

        yield return new WaitForSeconds(2);

        // Transition to the player's turn after setup
        state = BattleState.PLAYER_TURN;
        selectedEnemy = null;

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
        // Make each active enemy attack the player
        foreach (GameObject enemy in activeEnemies)
        {
            EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
            Debug.Log(enemy.name + " attacks!");
            playerStats.TakeDamage(enemyStats.attack);

            yield return new WaitForSeconds(1); // Wait between attacks
        }

        if (playerStats.currentHealth <= 0)
        {
            state = BattleState.LOST;
            Debug.Log("You lost the battle!");
        }
        else
        {
            state = BattleState.PLAYER_TURN;
            PlayerTurn();
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

        if (selectedEnemy == null)
        {
            Debug.Log("No enemy selected! Select an enemy first.");
            return;
        }

        // Deal damage to the selected enemy
        EnemyStats enemyStats = selectedEnemy.GetComponent<EnemyStats>();
        Debug.Log("Player attacks!");
        bool isCritical = Random.value < playerStats.criticalChance; // Random.value gives a value between 0.0 and 1.0
        int finalDamage = playerStats.attack;
        if (isCritical)
        {
            finalDamage = Mathf.RoundToInt(playerStats.attack * playerStats.criticalDamage);
            Debug.Log("CRITICAL HIT!");
        }
        enemyStats.TakeDamage(finalDamage);

        if (enemyStats.currentHealth <= 0)
        {
            Debug.Log("Enemy defeated!");
            playerStats.GainExp(enemyStats.experience);
            activeEnemies.Remove(selectedEnemy); // Remove from active enemies
            Destroy(selectedEnemy); // Destroy the GameObject

            if (activeEnemies.Count == 0)
            {
                state = BattleState.WON;
                Debug.Log("You won the battle!");
                SceneManager.LoadScene("SampleScene"); // Exit to overworld
                return;
            }
        }
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
        PlayerPrefs.SetInt("PlayerHealth", playerStats.currentHealth);
        PlayerPrefs.Save();
        SceneManager.LoadScene("SampleScene"); // Exit to overworld
        return;
    }

    void EndPlayerTurn()
    {
        state = BattleState.ENEMY_TURN;
        StartCoroutine(EnemyTurn());
    }

    public void SelectEnemy(GameObject enemy)
    {
        if (state != BattleState.PLAYER_TURN) return;

        // Deselect previous enemy
        foreach (GameObject e in activeEnemies)
        {
            e.GetComponent<EnemySelection>().HighlightEnemy(false);
        }

        // Select the new enemy
        selectedEnemy = enemy;
        enemy.GetComponent<EnemySelection>().HighlightEnemy(true);

        Debug.Log("Selected Enemy: " + enemy.name);
    }

}
