using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class ThirdPersonController : MonoBehaviour
{
    public bool canMove;
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
    [HideInInspector]
    public bool Saved;
    [HideInInspector]
    public bool Loaded;
    public float powerValue;
    public bool Targeted;
    public bool inWater;

    public bool isGrounded{get; set;}
    [SerializeField] private GameObject camGround;
    [SerializeField] private GameObject camFly;


    //[HideInInspector]
    public float rbSpeed;
    public int maxHealth = 4;
    public int currentHealth;

    public GameObject healthBar;
    public GameObject staminaBar;

    private AudioSource caw;
    


    // Start is called before the first frame update
    private void Awake()
    {
        Checkpoint();
        rb = this.GetComponent<Rigidbody>();
        playerInputs = new ThirdPersonInputs();
        Stamina = 6;
        CapsuleCollider = transform.GetComponent<CapsuleCollider>();
        controls = new ControlsforPlayer();
        isGrounded = true;
        if(healthBar != null)
        {
            healthBar.GetComponent<HealthBar>().SetMaxHealth(4);
        }
        currentHealth = maxHealth;

        caw = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(canMove)
        {
            forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * moveForce;
            forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * moveForce;
        }

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        // if(rb.velocity.y < 0f)
        // {
        //     rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
        // }

        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if(horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }

        IsGrounded();
        LookAt();

        Vector3 Turn = new Vector3(0, 0, 0);
        Vector3 MaxRotation = new Vector3(0, 10, 0);
        bool Left = controls.Actions.GlideLeft.ReadValue<float>() > 0.1f;
        bool Right = controls.Actions.GlideRight.ReadValue<float>() > 0.1f;

        if (Left)
        {
            GetComponent<ConstantForce>().relativeTorque = new Vector3(0, -10, 0);
        }
        else
        {
            GetComponent<ConstantForce>().relativeTorque = new Vector3(0, 0, 0);
        }
        if (Right)
        {
            GetComponent<ConstantForce>().relativeTorque = new Vector3(0, 10, 0);
        }
        Vector3 velocity = rb.velocity;
        Vector3 lastPosition = transform.position;
        bool dive = false;
        if (dive == false)
        {
            oldHVelocity = new Vector3(velocity.x, 0, velocity.z);
        }
        if (isGrounded == true)
        {
            Stamina += Time.fixedDeltaTime;
            if (Stamina >= 6)
            {
                Stamina = 6;
            }
        }

        flying = controls.Actions.Glide.ReadValue<float>() > 0.1f;
        bool diving = controls.Actions.Dive.ReadValue<float>() > 0.1f;
        if (diving && isGrounded == false)
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
            diveTim -= Time.fixedDeltaTime;
            GetComponent<ConstantForce>().force = new Vector3(0, 0, 0);
            Vector3 newHVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z) + oldHVelocity;
        }
        if (diveTim <= 0)
        {
            diveTim = 0;
        }
        if (isGrounded == false && canMove)
        {
            Stamina += (Time.fixedDeltaTime * 0.5f);
            if (Stamina >= 6)
            {
                Stamina = 6;
            }
            GetComponent<ConstantForce>().relativeForce = glideSpeed + Turn;
        }
        else { GetComponent<ConstantForce>().relativeForce = new Vector3(0, 0, 0); }

    }

    private void Update()
    {
        if (controls.Test.HealthTest.WasPerformedThisFrame())
        {
            TakeDamage(1);
            Debug.Log("Taking Damage...");
        }
        if (currentHealth <= 0)
        {
            LoadCheckpoint();
            currentHealth = maxHealth;
        }
        staminaBar?.GetComponent<StaminaBar>().UpdateStamina(Stamina);
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);

        healthBar?.GetComponent<HealthBar>().SetHealth(currentHealth);
        Debug.Log("In TakeDamage");
        
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
        playerInputs.PlayerOnGround.Caw.started += Caw;
        move = playerInputs.PlayerOnGround.Movement;
        playerInputs.PlayerOnGround.Enable();
        controls.Enable();

        camGround.SetActive(true);
        camFly.SetActive(false);

    }
    private void OnDisable()
    {
        playerInputs.PlayerOnGround.Jump.started -= DoJump;
        playerInputs.PlayerOnGround.Caw.started -= Caw;
        playerInputs.PlayerOnGround.Disable();
        controls.Disable();

        camGround.SetActive(false);
        camFly.SetActive(false);
    }

    private void IsGrounded()
    {
        float bufferDistance = 0.1f;
        float groundCheckDistance = (GetComponent<CapsuleCollider>().height/2)+bufferDistance;
        RaycastHit hit;
        if(Physics.Raycast(transform.position,-transform.up, out hit,groundCheckDistance))
        {
            isGrounded=true;
            camGround.SetActive(true);
            camFly.SetActive(false);
        }
        else
        {
            isGrounded = false;
            camGround.SetActive(false);
            camFly.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("wall")){
            Vector3 direction = other.contacts[0].point - transform.position;
            direction = -direction.normalized;
            rb.AddForce((-transform.forward * 1000) * powerValue);
        }
        if(other.gameObject.CompareTag("Water")){
        moveForce = 0.5f;
            isGrounded = false;
            inWater = true;
        }
    }
    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.CompareTag("Water")){
            inWater = false;
            moveForce = 5f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            Checkpoint();
        }
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        
            if (Stamina > 0)
            {
                forceDirection += Vector3.up * jumpForce;
                Stamina -= 1;
                StaminaBar.instance.UseStamina(1);
                Debug.Log("In DoJump Function");
            }
        
    }
    public void SavePlayer ()
    {
        SaveSystem.SavePlayer(this);
        Saved = true;
    }
    public void LoadPlayer ()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        currentHealth = data.health;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
        Stamina = data.Stamina;
    }

    public void Checkpoint ()
    {
        SaveSystem.Checkpoint(this);
        Saved = true;
    }
    public void LoadCheckpoint ()
    {
        PlayerData data = SaveSystem.LoadCheckpoint();
        currentHealth = data.health;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
        Stamina = data.Stamina;
    }
    private void Caw(InputAction.CallbackContext obj)
    {
        caw.Play();
    }

}
