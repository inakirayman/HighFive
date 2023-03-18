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


    [Header("Slide")]
    public float SlideDuration;
    private float _slideTime;
    public Transform _slidePivot;
    public bool _isSliding;
 


    private Rigidbody _rigidbody;
    

    public Vector3 _movement;
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



        if (_isSliding)
        {
            _slideTime += Time.deltaTime;

            if (_slideTime > SlideDuration)
            {
                _isSliding = false;
                _slideTime = 0;
            }
               

            _slidePivot.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));

        }
        else if (!_isSliding )
        {
            _slidePivot.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        }

        
    }



    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && _isGrounded)  
        _rigidbody.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }

    public void Slide(InputAction.CallbackContext context)
    {
       
        if (context.performed && _isGrounded && !_isSliding)
        {
             Debug.Log("Slide");
            _isSliding = true;
        }
           
    }

    public void HorizontalMovement(InputAction.CallbackContext context)
    {
        if (context.performed && !_isSwapingLanes)
        {
            _isSwapingLanes = true;
            float value = context.ReadValue<float>();

            xPos = transform.position.x + value;
        }
       
    }




}
