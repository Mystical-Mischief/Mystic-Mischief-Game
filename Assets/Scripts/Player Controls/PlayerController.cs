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
    [HideInInspector] public bool onGround;
    [HideInInspector] public float stamina;
    [HideInInspector] public bool damaged;
    [HideInInspector] public int currentHealth;
    [HideInInspector] public bool inWater;

    //private but can see in editor
    [SerializeField] float maxStamina;
    [SerializeField] float moveForce;
    [SerializeField] float jumpForce;
    [SerializeField] float maxSpeed;
    [SerializeField] Camera playerCam;
    [SerializeField] float powerValue;
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
    private float originalMoveForce;
    private float originalMaxSpeed;
    Vector3 forceDirection = Vector3.zero;
    bool jumpInAir;
    bool diving;

    private void Start()
    {
        originalMaxSpeed = maxSpeed;
        originalMoveForce = moveForce;
        rb = GetComponent<Rigidbody>();
        controls.Enable();
        stamina = maxStamina;
        move = controls.Actions.Movement;
        jump = controls.Actions.Jump;
        flying = controls.Actions.Glide;
        currentHealth = maxHealth;
    }
    private void OnEnable()
    {
        controls = new ControlsforPlayer();
        controls.Enable();
        controls.Actions.Jump.started += DoJump;
        controls.Actions.Caw.started += Caw;
        controls.Actions.GodMode.started += GodMode;
        move = controls.Actions.Movement;
    }
    private void OnDisable()
    {
        controls.Disable();
        controls.Actions.Caw.started -= Caw;
        controls.Actions.Jump.started += DoJump;
        controls.Actions.GodMode.started -= GodMode;
        controls.Disable();
    }
    //Variables only used in this function
    Vector3 horizontalVelocity;
    private void FixedUpdate()
    {
        if (canMove)
        {
            forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCam) * moveForce;
            forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCam) * moveForce;
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
        GlidingLogic();
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
        direction = rb.velocity;
        direction.y = 0f;
        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f)
        {
            rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }
    }
    Vector3 glideSpeed;
    private void DivingLogic()
    {
        if (canMove)
        {
            diving = controls.Actions.Dive.ReadValue<float>() > 0.1f;
            if (diving && onGround == false)
            {
                diveTim += Time.fixedDeltaTime;
                Vector3 newHVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                GetComponent<ConstantForce>().force = diveSpeed;
            }
            else
            {
                glideSpeed.z = glideSpeed.z + diveTim - Time.fixedDeltaTime;
                glideSpeed.y = glideSpeed.y + diveTim - (Time.fixedDeltaTime * 2);
                //The code below makes sure that each part of the glidespeed Vector3 does not add to much momentum after the player stops diving. These are caps for each float so that it does not exceed to much.
                if (diveTim > 10)
                {
                    diveTim = 0;
                    glideSpeed.z = 100;
                }
                if (glideSpeed.z <= 100)
                {
                    glideSpeed.z = 100;
                }
                if (glideSpeed.y <= 9.8f)
                {
                    glideSpeed.y = 9.8f;
                }
                if (glideSpeed.z >= 130)
                {
                    glideSpeed.z = 100;
                }
                if (glideSpeed.y >= 25)
                {
                    glideSpeed.y = 9.8f;
                }
                //This makes the players momentum slowely return to what it normally is when the player flies.
                diveTim -= Time.fixedDeltaTime;
                GetComponent<ConstantForce>().force = new Vector3(0, 0, 0);
            }
            //This lets the float for calculating how long the player dives not go below zero.
            if (diveTim <= 0)
            {
                diveTim = 0;
            }
        }
    }
    void GlidingLogic()
    {
        //This lets the player fly after jumping in the air.
        if (onGround == false && canMove && jumpInAir == true)
        {
            stamina += (Time.fixedDeltaTime * 0.5f);
            if (stamina >= 4)
            {
                stamina = 4;
            }
            GetComponent<ConstantForce>().relativeForce = glideSpeed;
        }
        //This makes the flying stop.
        else
        {
            GetComponent<ConstantForce>().relativeForce = new Vector3(0, 0, 0);
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
    private void DoJump(InputAction.CallbackContext obj)
    {
        if (canMove)
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
    }
    private void Caw(InputAction.CallbackContext obj)
    {
        //caw.Play();
        print("caw");
    }
    private void GodMode(InputAction.CallbackContext obj)
    {
        if (!godMode)
        {
            godMode = true;
            maxSpeed *= 2;
            moveForce *= 2;
        }
        else
        {
            maxSpeed /= 2;
            moveForce /= 2;
            godMode = false;
        }
    }
    void OnCollisionEnter(Collision other)
    {
        //This pushes the player back when they hit a wall.
        if (other.gameObject.CompareTag("wall"))
        {
            Vector3 direction = other.contacts[0].point - transform.position;
            direction = -direction.normalized;
            rb.AddForce((-transform.forward * 1000) * powerValue);
        }
        //This pushes the player back when they hit an enemy.
        if (other.gameObject.CompareTag("enemy"))
        {
            Vector3 direction = other.contacts[0].point - transform.position;
            direction = -direction.normalized;
            rb.AddForce((-transform.forward * 1000) * powerValue);
        }
        //This stops the player from moving when they are in the water
        if (other.gameObject.CompareTag("Water"))
        {
            moveForce = moveForce * 0.5f;
            onGround = false;
            inWater = true;
        }
        //This makes the player take damage when they run into the Attackpos gameobject of the dragon.
        if (other.gameObject.CompareTag("Attackpos"))
        {
            TakeDamage(4);
        }
        //This makes the player take damage when they are hit by a projectile.
        if (other.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(1);
        }
    }
    void OnCollisionExit(Collision other)
    {
        //This lets the player move again when they jump out of water.
        if (other.gameObject.CompareTag("Water"))
        {
            inWater = false;
            moveForce = moveForce * 2f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //This saves a checkpoint.
        if (other.gameObject.tag == "Checkpoint")
        {
            //Checkpoint();
        }
        //In the fire level this lets the dragon know what room the player is in.
        if (other.gameObject.CompareTag("RoomEnter"))
        {
            other.GetComponent<FireDragonPerch>().playerInRoom = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Im the fire level this lets the dragon know when they exited the room they were in.
        if (other.gameObject.CompareTag("RoomEnter"))
        {
            other.GetComponent<FireDragonPerch>().playerInRoom = false;
        }
    }
    public void IncreaseSpeed(float speedBoost)
    {
        maxSpeed *= speedBoost;
        moveForce *= speedBoost;
    }
    public void SetSpeedToNormal()
    {
        maxSpeed = originalMaxSpeed;
        moveForce = originalMoveForce;
    }
}
