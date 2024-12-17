using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBox : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool canMove = false;         // Tracks if the box can move
    private bool playerNearby = false;    // Checks if the player is near the box
    private bool isMoving = false;        // Prevents multiple moves at once

    public float moveSpeed = 5f;          // Speed to move the box
    public Vector2 boxSize = new Vector2(0.9f, 0.9f); // Size of the box collider for overlap check
    public LayerMask obstacleLayer;       // Layer mask to define which layers block the box

    private Vector2 targetPosition;       // The position the box will move to

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // Box starts static
        targetPosition = rb.position; // Set initial target position
    }

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.Return))
        {
            ToggleMovement();
        }

        if (canMove && !isMoving)
        {
            CheckMoveInput();
        }

        // Smoothly move the box to the target position
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerNearby = false;
            canMove = false;
            rb.bodyType = RigidbodyType2D.Kinematic; // Reset to static
        }
    }

    private void ToggleMovement()
    {
        canMove = !canMove;
        rb.bodyType = canMove ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;
    }

    private void CheckMoveInput()
    {
        // Check for directional input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveX != 0 || moveY != 0)
        {
            Vector2 direction = new Vector2(moveX, moveY).normalized;

            // Calculate the target position (one tile away)
            Vector2 nextPosition = rb.position + direction;

            // Check if the next position has an obstacle
            if (!Physics2D.OverlapBox(nextPosition, boxSize, 0, obstacleLayer))
            {
                targetPosition = nextPosition; // Set target position if no obstacle
                isMoving = true;               // Start movement
            }
            else
            {
                Debug.Log("Cannot move: Tile is occupied!");
            }
        }
    }

    private void MoveToTarget()
    {
        // Move towards the target position
        rb.position = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.deltaTime);

        // Stop movement when the target is reached
        if (Vector2.Distance(rb.position, targetPosition) < 0.01f)
        {
            rb.position = targetPosition; // Snap to position
            isMoving = false;             // Stop moving
        }
    }
}
