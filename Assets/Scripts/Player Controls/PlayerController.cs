using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //public variables
    public int maxHealth = 4;
    public bool canMove;
    public bool godMode;

    //private but can see in editor
    [SerializeField] float maxStamina;
    [SerializeField] float moveForce;
    [SerializeField] float jumpForce;
    [SerializeField] float maxSpeed;
    [SerializeField] Camera playerCam;
    [SerializeField] float powerValue;
    [SerializeField] private GameObject camGround;
    [SerializeField] private GameObject camFly;
    [SerializeField] private GameObject dragonCamGround;
    [SerializeField] private GameObject dragonCamFly;
    [SerializeField] private GameObject flyingEffets;
    [SerializeField] private Vector3 diveSpeed;
    PlayerAnimation playerAnimation;

    //Input actions
    private InputAction move;
    private InputAction jump;
    private InputAction flying;

    //Private variables
    ControlsforPlayer controls;
    Rigidbody rb;
    float diveTim;
    float stamina;
    private float originalMoveForce;
    private float originalMaxSpeed;
    Vector3 forceDirection = Vector3.zero;
    bool onGround;
    bool inWater;
    bool jumpInAir;
    bool damaged;
    bool diving;
    int currentHealth;

    //Scripts to make to seperate this shit
    //1) Audio manager & Animations
    //2) Lock on function

    private void Start()
    {
        originalMaxSpeed = maxSpeed;
        originalMoveForce = moveForce;
        rb = GetComponent<Rigidbody>();
        controls = new ControlsforPlayer();
        controls.Enable();
        stamina = maxStamina;
        move = controls.Actions.Movement;
        jump = controls.Actions.Jump;
        flying = controls.Actions.Glide;
        currentHealth = maxHealth;
    }
    //Variables only used in this function
    Vector3 horizontalVelocity;
    private void Update()
    {
        if (canMove)
        {
            forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCam) * moveForce;
            forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCam) * moveForce;

            if (jump.triggered)
            {
                DoJump();
            }
        }
        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }

        IsGrounded();
        LookAt();

        DivingLogic();
    }
    private void FixedUpdate()
    {
        
    }

    float bufferDistance = 0.1f;
    float groundCheckDistance;
    RaycastHit hit;
    private void IsGrounded()
    {
        groundCheckDistance = (GetComponent<CapsuleCollider>().height / 2) + bufferDistance;
        if (Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance))
        {
            onGround = true;
            jumpInAir = false;
            stamina += Time.fixedDeltaTime;
            if (stamina >= 4)
            {
                stamina = 4;
            }
        }
        else
        {
            onGround = false;
        }
    }
    Vector3 direction;
    private void LookAt()
    {
        direction.y = 0f;
        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f)
        {
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }
    }
    private void DoJump()
    {
        if (stamina > 0)
        {
            forceDirection += Vector3.up * jumpForce;
            if (stamina > 0 && onGround == false)
            {
                forceDirection += Vector3.up * jumpForce;
                if (!godMode)
                {
                    stamina -= 1;
                }
                jumpInAir = true;
            }
        }
    }
    private void DivingLogic()
    {
        if (diving && onGround == false)
        {
            diveTim += Time.fixedDeltaTime;
            Vector3 newHVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            GetComponent<ConstantForce>().force = diveSpeed;
            flyingEffets.SetActive(false);

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
    public void TakeDamage(int damage)
    {
        if (!godMode)
        {
            currentHealth -= damage;
            Debug.Log(currentHealth);
            damaged = true;
            StartCoroutine(tookDamage());
        }

    }
    IEnumerator tookDamage()
    {
        yield return new WaitForSeconds(2);
        damaged = false;
    }
}
