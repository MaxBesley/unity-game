using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform orientation;

    // jump key
    private readonly KeyCode JUMP_KEY = KeyCode.Space;

    private Vector3 _moveDirection;
    private float _horizontalInput;
    private float _verticalInput;
    private bool _readyToJump;
    private bool _onGround;
    private Rigidbody _rb;

    // jumping variables
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;

    [SerializeField] private LayerMask ground;
    [SerializeField] private float playerHeight;
    [SerializeField] private float groundDrag;


    // This is called once when the script object is loaded and initialised
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _readyToJump = true;
    }

    // Called at a consistent (framrate-independent) rate and is in-sync with
    // the Unity physics engine. Generally used for physics-based code.
    private void FixedUpdate()
    {
        MovePlayer();
    }

    // Move the player via user input
    private void MovePlayer()
    {
        // compute as a unit vector
        _moveDirection = (orientation.forward * _verticalInput + orientation.right * _horizontalInput).normalized;
        _moveDirection.y = 0;
        
        // ground movement
        if (_onGround)
        {
            
            _rb.AddForce(_moveDirection * moveSpeed * 10f, ForceMode.Force);
        }
        // air movement
        else
        {

            _rb.AddForce(_moveDirection * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            //                                         ^ MAGIC NUMBER?
        }
    }

    public void groundPlayer()
    {
        _onGround = true;
    }

    // Called on every frame
    void Update()
    {
        // we use `GetAxis()` so both WASD and arrow keys can be used
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
        // determine if the player is on the ground
        _onGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
        
        // make player jump
        if (_readyToJump && _onGround && Input.GetKey(JUMP_KEY))
        {
            _readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
            
        }

        // bound the player's speed
        SpeedControl();

        // remove slipping
        if (_onGround)
        {
            _rb.drag = groundDrag;
        }
        else
        {
            _rb.drag = 0.0f;
        }
//           ^^ DOESN'T ACTUALLY REMOVE SLIPPING!
    }

    // Limit the speed of the player
    private void SpeedControl()
    {
        // compute the player's velocity in the flat x-z plane
        Vector3 xzVelocity = new Vector3(_rb.velocity.x, 0.0f, _rb.velocity.z);

        if (xzVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVel = xzVelocity.normalized * moveSpeed;
            _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
        }
    }

    // Causes the player to jump up when called
    private void Jump()
    {
        // set y velocity to zero
        _rb.velocity = new Vector3(_rb.velocity.x, 0.0f, _rb.velocity.z);

        // execute a jump via an upward force
        _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }

    private void ResetJump() {_readyToJump = true;}
}
