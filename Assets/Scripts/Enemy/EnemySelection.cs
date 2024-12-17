using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelection : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // For highlighting
    private Color originalColor;
    private BattleManager battleSystem;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        // Find the BattleSystem
        battleSystem = FindObjectOfType<BattleManager>();
    }

    private void OnMouseDown() // Triggered when the enemy is clicked
    {
        battleSystem.SelectEnemy(gameObject); // Tell BattleSystem which enemy is selected
    }

    public void HighlightEnemy(bool highlight)
    {
        spriteRenderer.color = highlight ? Color.red : originalColor; // Change color to indicate selection
    }
}
