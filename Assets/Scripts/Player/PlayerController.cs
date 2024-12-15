using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;

    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        // Disable movement if in the Battle Scene
        if (SceneManager.GetActiveScene().name == "BattleScene1")
        {
            // Stop movement when entering the Battle Scene
            StopMovement();
            SetIdleAnimation();
            return; // Prevent movement
        }
        PlayerInput();
    }

    private void FixedUpdate()
    {
        // Stop movement if in the Battle Scene
        if (SceneManager.GetActiveScene().name == "BattleScene1")
        {
            StopMovement();
            return;
        }
        AdjustPlayerFacingDirections();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirections()
    {
        // Check for horizontal movement input
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput < 0)
        {
            // Moving left
            mySpriteRenderer.flipX = true;
        }
        else if (horizontalInput > 0)
        {
            // Moving right
            mySpriteRenderer.flipX = false;
        }
    }

    // Helper function to stop movement
    private void StopMovement()
    {
        // Optional: Add logic if you're using Rigidbody2D for movement
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Stop Rigidbody movement
        }
    }

    private void SetIdleAnimation()
    {
        myAnimator = GetComponent<Animator>();
        myAnimator.SetFloat("moveX", 0f); // Assuming "Speed" controls transition between idle and movement
        myAnimator.SetFloat("moveY", 0f);
        mySpriteRenderer.flipX = false;
    }
}
