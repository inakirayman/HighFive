using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float WalkSpeed = 6;
    public float JumpForce = 5;
    public float LaneSwapSpeed = 10;

    [Header("Ground Check")]
    public float PlayerHeight;
    public LayerMask GroundMask;
    private bool _isGrounded;


    private Rigidbody _rigidbody;
    

    public Vector3 _movement;
    private Vector3 _startMovePos;
    private float xPos;
    private bool _isSwapingLanes;

    private void Start()
    {
       
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.2f, GroundMask);

        

        if (_isSwapingLanes & transform.position.x == xPos) _isSwapingLanes = false;
    }

    private void FixedUpdate()
    {
      

        var pos = transform.position;
        _rigidbody.MovePosition(pos + new Vector3(0, 0, WalkSpeed * Time.deltaTime));
        if (_isSwapingLanes)
        _rigidbody.MovePosition(Vector3.MoveTowards(pos, new Vector3(xPos, pos.y, pos.z),Time.deltaTime*LaneSwapSpeed));


       
            //(new Vector3(0, 0, WalkSpeed), ForceMode.Force);
        
    }



    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed & _isGrounded)  
        _rigidbody.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }

    public void HorizontalMovement(InputAction.CallbackContext context)
    {
        if (context.performed & !_isSwapingLanes)
        {
            _isSwapingLanes = true;
            float value = context.ReadValue<float>();

            xPos = transform.position.x + value;
        }
       
    }




}
