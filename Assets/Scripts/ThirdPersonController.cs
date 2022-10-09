using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

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
    public int Stamina;
    public float runSpeed;
    public float applySpeed;
    private bool flying;
    private Vector3 oldHVelocity;
    public Vector3 glideSpeed;
    public Vector3 diveSpeed;
    public float diveTim;

    public bool isGrounded{get; set;}
    [SerializeField] private CinemachineFreeLook camGround;
    [SerializeField] private CinemachineFreeLook camFly;
    


    // Start is called before the first frame update
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        playerInputs = new ThirdPersonInputs();
        Stamina = 6;
        CapsuleCollider = transform.GetComponent<CapsuleCollider>();
        controls = new ControlsforPlayer();
        isGrounded = true;
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

        IsGrounded();
        LookAt();

        // Gliding and Diving
        Vector3 Turn = new Vector3(0,0,0);
        Vector3 MaxRotation = new Vector3 (0,10,0);
        bool Left = controls.Actions.GlideLeft.ReadValue<float>() > 0.1f;
        bool Right = controls.Actions.GlideRight.ReadValue<float>() > 0.1f;

        if (Left)
        {
       //    GetComponent<ConstantForce>().relativeTorque = new Vector3 (0,-10,0);
        }
        else
        {
        //    GetComponent<ConstantForce>().relativeTorque = new Vector3 (0,0,0);
        }
        if (Right)
        {
        //   GetComponent<ConstantForce>().relativeTorque = new Vector3 (0,10,0);
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
                if(diving)
        {
            diveTim += Time.fixedDeltaTime;
            dive = true;
            Vector3 newHVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            //GetComponent<ConstantForce>().force = diveSpeed;
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
            //GetComponent<ConstantForce>().force = new Vector3(0, 0, 0);
            Vector3 newHVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z) + oldHVelocity;
        }
        if (diveTim <= 0)
        {
            diveTim = 0;
        }
                if (flying)
        {
            //GetComponent<ConstantForce>().relativeForce = glideSpeed + Turn;
        }
        //else {GetComponent<ConstantForce>().relativeForce = new Vector3(0, 0, 0);}
      
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

        CameraSwitch.Register(camGround);
        CameraSwitch.Register(camFly);
        CameraSwitch.SwitchCamera(camGround);

    }
    private void OnDisable()
    {
        playerInputs.PlayerOnGround.Jump.started -= DoJump;
        playerInputs.PlayerOnGround.Disable();
        controls.Disable();

        CameraSwitch.Unregister(camGround);
        CameraSwitch.Unregister(camFly);
    }

    private void IsGrounded()
    {
        float bufferDistance = 0.1f;
        float groundCheckDistance = (GetComponent<CapsuleCollider>().height/2)+bufferDistance;
        RaycastHit hit;
        if(Physics.Raycast(transform.position,-transform.up, out hit,groundCheckDistance))
        {
            isGrounded=true;
            if(CameraSwitch.IsActiveCamera(camFly))
            {
                CameraSwitch.SwitchCamera(camGround);
                Debug.Log("Ground");
            }
        }
        else
        {
            isGrounded = false;
            if(CameraSwitch.IsActiveCamera(camGround))
            {
                CameraSwitch.SwitchCamera(camFly);
                Debug.Log("Fly");
            }
        }
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        //if()
        {
            if (Stamina > 0)
            {
            forceDirection += Vector3.up * jumpForce;
            //Stamina -= 1;
            }
        }
    }
   
}
