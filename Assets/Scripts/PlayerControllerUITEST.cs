using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class PlayerControllerUITEST : MonoBehaviour
{
    public float MoveSpeed = 5f; // The speed at which the character moves.
    public float JumpForce = 7f; // The force with which the character jumps.
    private Rigidbody _rb; // The rigidbody component attached to the character.
    public bool _isGrounded = false; // Whether or not the character is touching the ground.
    private Vector3 _movement;
    public int CoinValue = 1;
    public int Multiplier = 1;
    public int Launchpad = 30;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        SimpleMovement();
        
        IncreaseScores();
    }

    private void SimpleMovement()
    {
        // Get the horizontal input (left/right arrow keys or A/D keys).
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Get the vertical input (up/down arrow keys or W/S keys).
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Move the character horizontally and vertically.
        _movement = new Vector3(horizontalInput * MoveSpeed, _rb.velocity.y, verticalInput * MoveSpeed);
        _rb.velocity = _movement;

        // Jump if the jump button (space) is pressed and the character is grounded.
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            _isGrounded = false;
        }    
    }

    private void IncreaseScores()
    {
        //UIController.Instance.IncreaseScore((int)_movement.z, Multiplier);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the character is touching the ground.
        

        if (other.gameObject.CompareTag("Coins"))
        {
            
            Destroy(other.gameObject);
            UIController.Instance.IncreaseCoins(CoinValue);
        }

        if (other.gameObject.CompareTag("Obstacles"))
        {
            this.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Launchers"))
        {
            LaunchPlayer();
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void LaunchPlayer()
    {
        _rb.AddForce(new Vector3(0, 1, 0)* Launchpad, ForceMode.Impulse);
    }
}
