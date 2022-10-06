using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{

    private ThirdPersonInputs playerInputs;
    private InputAction move;

    private Rigidbody rb;
    [SerializeField]
    private float moveForce = 5f;
    
    public float jumpForce = 10f;
    [SerializeField]
    private float maxSpeed = 5f;
    [SerializeField]
    private Vector3 forceDirection = Vector3.zero;

    [SerializeField]
    private Camera playerCamera;

    public bool isGrounded = false;
    public float groundCheckDisttance;
    private float bufferCheckDistance = 0.1f;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        playerInputs = new ThirdPersonInputs();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * moveForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * moveForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if(rb.velocity.y < 0f)
        {
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
        }

        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if(horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }

        LookAt();

        groundCheckDisttance = (GetComponent<CapsuleCollider>().height/2)+bufferCheckDistance;

        RaycastHit hit;
        if(Physics.Raycast(transform.position,-transform.up,out hit, groundCheckDisttance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;
        if(move.ReadValue<Vector2>().sqrMagnitude > 0.1f)
        {
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }
    }
    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private void OnEnable()
    {
        playerInputs.PlayerOnGround.Jump.started += DoJump;
        move = playerInputs.PlayerOnGround.Movement;
        playerInputs.PlayerOnGround.Enable();

    }
    private void OnDisable()
    {
        playerInputs.PlayerOnGround.Jump.started -= DoJump;
        playerInputs.PlayerOnGround.Disable();
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        forceDirection += Vector3.up * jumpForce;
    }

   
}
