using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    // ATTRIBUTES

    public float speed = 12f; // Walk speed
    public float jumpPower = 12f; // Amount of force when jumping
    public float groundCheckDist = 8f; // Jump ray length

    public Vector3 goalDir; // direction to move to
    public float dirX; // x move dir

    // OBJECTS

    public LayerMask ground; // What layer the player can jump on
    public SpriteRenderer playerModelRenderer;
    public Rigidbody2D rb;
    public Animator animator;

    void Move()
    {
        dirX = Input.GetAxis("Horizontal"); // Horizontal move dir
        goalDir = new Vector3(dirX * speed, 0, 0) * Time.deltaTime; // Goal direction

        transform.Translate(goalDir, Space.Self); // Translate the position to the goal.
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Vector3 trajectory = transform.up * jumpPower; // Where the player will jump to
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDist, ground); // Ground check

            if (hit)
                rb.AddForce(trajectory); // Jump to goal position
        }
    }

    void UpdateAnimations()
    {
        playerModelRenderer.flipX = dirX < 0f; // Set rotation.

        if (goalDir.x != 0)
        {
            animator.Play("running");
        }
        else
        {
            animator.Play("idle");
        }
    }

    void Retry()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void Update()
    {
        Move();
        Jump();
        UpdateAnimations();
        Retry();
    }
}
