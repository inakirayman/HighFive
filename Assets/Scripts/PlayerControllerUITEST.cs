using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerUITEST : MonoBehaviour
{
    public float moveSpeed = 5f; // The speed at which the character moves.
    public float jumpForce = 7f; // The force with which the character jumps.
    private Rigidbody rb; // The rigidbody component attached to the character.
    private bool isGrounded = false; // Whether or not the character is touching the ground.

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get the horizontal input (left/right arrow keys or A/D keys).
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Get the vertical input (up/down arrow keys or W/S keys).
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Move the character horizontally and vertically.
        Vector3 movement = new Vector3(horizontalInput * moveSpeed, 0f, verticalInput * moveSpeed);
        rb.velocity = movement;

        // Jump if the jump button (space) is pressed and the character is grounded.
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        // Check if the character is touching the ground.
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
