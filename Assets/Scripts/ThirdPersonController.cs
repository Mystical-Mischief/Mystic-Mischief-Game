using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{

    private ThirdPersonInputs playerInputs;
    private InputAction move;
    ControlsforPlayer controls;
    private CapsuleCollider CapsuleCollider;

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
    public float Stamina;
    public float runSpeed;
    public float applySpeed;
    private bool flying;
    private Vector3 oldHVelocity;
    public Vector3 glideSpeed;
    public Vector3 diveSpeed;
    public float diveTim;
    private bool canFly;

    public bool isGrounded = false;
    public float groundCheckDisttance;
    private float bufferCheckDistance = 0.1f;
    public bool triggered { get; }
    


    // Start is called before the first frame update
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        playerInputs = new ThirdPersonInputs();
        Stamina = 6;
        CapsuleCollider = transform.GetComponent<CapsuleCollider>();
        controls = new ControlsforPlayer();
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
        if (isGrounded == true)
        {
           moveForce = 5f;
        }
        if (isGrounded == false)
        {
            moveForce = 0f;
        }
        // Gliding and Diving
        Vector3 Turn = new Vector3(0,0,0);
        Vector3 MaxRotation = new Vector3 (0,10,0);
        bool Left = controls.Actions.GlideLeft.ReadValue<float>() > 0.1f;
        bool Right = controls.Actions.GlideRight.ReadValue<float>() > 0.1f;

        if (Left && canFly == true)
        {
            GetComponent<ConstantForce>().relativeTorque = new Vector3 (0,-10,0);
        }
        else
        {
            GetComponent<ConstantForce>().relativeTorque = new Vector3 (0,0,0);
        }
        if (Right && canFly == true)
        {
           GetComponent<ConstantForce>().relativeTorque = new Vector3 (0,10,0);
        }
                Vector3 velocity = rb.velocity;
        Vector3 lastPosition = transform.position;
        bool dive = false;
        if (dive == false)
        {
        oldHVelocity = new Vector3(velocity.x, 0, velocity.z);
        }

                flying = controls.Actions.Glide.ReadValue<float>() > 0.1f;
                bool diving = controls.Actions.Dive.ReadValue<float>() > 0.1f;
                if(diving && canFly == true)
        {
            diveTim += Time.fixedDeltaTime;
            dive = true;
            Vector3 newHVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            GetComponent<ConstantForce>().force = diveSpeed;
        }
        else 
        {
            glideSpeed.z = glideSpeed.z + diveTim - Time.fixedDeltaTime;
            glideSpeed.y = glideSpeed.y + diveTim - (Time.fixedDeltaTime * 2);
            if (diveTim > 10)
            {
                diveTim = 0;
                glideSpeed.z = 100;
            }
            if (glideSpeed.z <=100)
            {
                glideSpeed.z = 100;
            }
            if (glideSpeed.y <=8)
            {
                glideSpeed.y = 8;
            }
            if (glideSpeed.z >=130)
            {
                glideSpeed.z = 100;
            }
            if (glideSpeed.y >=25)
            {
                glideSpeed.y = 8;
            }
            diveTim -= Time.fixedDeltaTime;
            GetComponent<ConstantForce>().force = new Vector3(0, 0, 0);
            Vector3 newHVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z) + oldHVelocity;
        }
        if (diveTim <= 0)
        {
            diveTim = 0;
        }
                if (flying && canFly == true)
        {
            GetComponent<ConstantForce>().relativeForce = glideSpeed + Turn;
        }
        else {GetComponent<ConstantForce>().relativeForce = new Vector3(0, 0, 0);}

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
        if (isGrounded == true)
        {
            Stamina += (Time.fixedDeltaTime * 2);
            if (Stamina >= 6)
            {
                Stamina = 6;
            }
            canFly = false;
        }
        else 
        {
            canFly = true;
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
        controls.Enable();

    }
    private void OnDisable()
    {
        playerInputs.PlayerOnGround.Jump.started -= DoJump;
        playerInputs.PlayerOnGround.Disable();
        controls.Disable();
    }
    private void IsGroundeds()
    {
        //float extraHeight = 0.01f;
        //Physics.Raycast(CapsuleCollider.bounds.center, Vector2.down, CapsuleCollider.bounds.extents.y + extraHeight);
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        if (isGrounded == true)
        {
            if (Stamina > 0)
            {
            forceDirection += new Vector3(0,2,0) * jumpForce;
            Stamina -= 1;
            }
        }
        else{
            if (Stamina > 0)
            {
                forceDirection += Vector3.up * jumpForce;
                Stamina -= 1;
            }
        }
    }
   
}
