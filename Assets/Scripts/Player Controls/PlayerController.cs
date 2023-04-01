using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //public variables -CC
    public int maxHealth = 4;
    public bool canMove;
    public bool godMode;
    public SaveGeneral save;
    public float maxStamina;

    //public but doesnt need to be seen in the inspector -CC
    [HideInInspector] public bool onGround;
    [HideInInspector] public float stamina;
    [HideInInspector] public bool damaged;
    [HideInInspector] public int currentHealth;
    [HideInInspector] public bool inWater;
    [HideInInspector] public bool jumpInAir;

    //private but can see in editor -CC
    [SerializeField] float moveForce;
    public float pushForce;
    [SerializeField] float jumpForce;
    [SerializeField] float maxSpeed;
    [SerializeField] Camera playerCam;
    public float powerValue;
    [SerializeField] private Vector3 diveSpeed;


    //Input actions -CC
    private InputAction move;
    private InputAction jump;
    private InputAction flying;

    //Private variables -CC
    ControlsforPlayer controls;
    [HideInInspector] public Rigidbody rb;
    float diveTim;
    private float originalMoveForce;
    private float originalMaxSpeed;
    private float oldMaxStamina;
    Vector3 forceDirection = Vector3.zero;
    bool diving;
    bool Saved;
    private bool hasJumped; //A check for whether the player has already jumped once. - Emilie

    float easyMoveForce;
    float easyMaxSpeed;


    public event EventHandler GotHurt;

    private void Start()
    {
        oldMaxStamina = maxStamina;
        if (DifficultySettings.StaminaDiff == true)
        {
            maxStamina = maxStamina * 2;
        }
        else
        {
            maxStamina = 6;
        }
        originalMaxSpeed = maxSpeed;
        originalMoveForce = moveForce;
        easyMaxSpeed = maxSpeed * 2;
        easyMoveForce = moveForce * 2;
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
        //Sets up controls for jump, caw and godmode
        controls = new ControlsforPlayer();
        controls.Enable();
        controls.Actions.Jump.performed += DoJump;
        controls.Actions.GodMode.started += GodMode;
        move = controls.Actions.Movement;
    }
    private void OnDisable()
    {
        //takes away controls
        controls.Actions.Jump.started -= DoJump;
        controls.Actions.GodMode.started -= GodMode;
        controls.Disable();
    }
    public void UpdateControls()
    {
        var rebinds = PlayerPrefs.GetString("rebinds");
        print(rebinds);
        controls.asset.LoadBindingOverridesFromJson(rebinds);
    }
    //Variables only used in this function
    Vector3 horizontalVelocity;
    private void FixedUpdate()
    {
        if (DifficultySettings.StaminaDiff == true)
        {
            maxStamina = oldMaxStamina * 2;
        }
        else
        {
            maxStamina = 6;
        }
        if (DifficultySettings.PlayerSpeedDiff == true)
        {
            maxSpeed = easyMaxSpeed;
            moveForce = easyMoveForce;
        }
        else
        {
            SetSpeedToNormal();
        }
        //basic playermovement like walking -CC
        if (canMove)
        {
            forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCam) * moveForce;
            forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCam) * moveForce;
        }
        //adds force to move player -CC
        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }

        //Ground Logic -CC
        IsGrounded();
        //Camera Looking Logic -CC
        LookAt();
        //Diving Logic- CC
        DivingLogic();
        //Gliding Logic - CC
        GlidingLogic();
    }

    float bufferDistance = 0.1f;
    float groundCheckDistance;
    RaycastHit hit;
    private void IsGrounded()
    {
        //gets the distance for the ground check based off the players collider -CC
        groundCheckDistance = (GetComponent<CapsuleCollider>().height / 2) + bufferDistance;
        if (Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance))
        {
            //if the raycast hits the ground, you are on the ground and regen stamina faster -CC
            onGround = true;
            jumpInAir = false;

            hasJumped = false;

            stamina += Time.fixedDeltaTime;
            if (stamina >= maxStamina)
            {
                stamina = maxStamina;
            }
        }
        //otherwise you are not on the ground -CC
        else
        {
            onGround = false;
            hasJumped = true;
        }
    }
    //Look at adjusts the players direction so they move forward and glide forwad based on where the camera is facing -CC
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
        //if you can move - CC
        if (canMove)
        {
            //checks to see if you are diving - CC
            diving = controls.Actions.Dive.ReadValue<float>() > 0.1f;
            if (diving && onGround == false)
            {
                //if so dive and keep track of how long you are diving -CC
                diveTim += Time.fixedDeltaTime;
                GetComponent<ConstantForce>().force = diveSpeed;
            }
            else
            {
                //afterwards apply speed based on how long you have been diving for -CC
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

            if (stamina >= maxStamina)
            {
                stamina = maxStamina;
            }

            GetComponent<ConstantForce>().relativeForce = glideSpeed;
        }
        //This makes the flying stop. 
        else
        {
            GetComponent<ConstantForce>().relativeForce = new Vector3(0, 0, 0);
        }
    }
    //gets the camera's right so you can determine the right direction for the player to move - CC
    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }
    //gets the camera's forward so you can determine the forward direction for the player to move - CC
    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    public void SetFloat(string KeyName, float Value)
    {
        PlayerPrefs.SetFloat(KeyName, Value);
    }
    //logic for taking damage -CC
    [SerializeField] private InteractionpromptUI Interactionprompt;
    public void TakeDamage(int damage)
    {
        //if you arent in godmode
        if (!godMode)
        {
            //take damage
            if (damaged != true) 
            {
                currentHealth -= damage;
                Interactionprompt.Setup("Ouch! That Hurt");
                if (currentHealth < 1)
                {
                    Scene scene = SceneManager.GetActiveScene();
                    if (scene.name == "Level 1")
                    {
                        SetFloat("LastLevel", 1);
                    }
                    if (scene.name == "Level 2")
                    {
                        SetFloat("LastLevel", 2);
                    }
                    if (scene.name == "Level 3")
                    {
                        SetFloat("LastLevel", 3);
                    }
                    SceneManager.LoadScene("Lose Screen");
                }

                damaged = true;
                StartCoroutine(tookDamage());
            }
        }

    }
    //timer so you dont take damage constantly -CC
    [SerializeField] float IFrames;
    IEnumerator tookDamage()
    {
        yield return new WaitForSeconds(IFrames); //This represents time spent invincible -Emilie 
        damaged = false;
    }
    //jump function that only triggers when you use jump -CC

    private void DoJump(InputAction.CallbackContext obj)
    {
        if (!PauseMenu.GameIsPaused)
        {
            if (stamina > 0)
            {
                //if you have stamina - CC
                if (!hasJumped)
                {
                    //jump
                    forceDirection += Vector3.up * jumpForce;
                    hasJumped = true;
                }
                else
                {
                    //jump for when you jump the 2nd time and start gliding - CC
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
    }
    
    public void updatePlayerMove(bool move)
    {
        canMove = move;
    }
    //enables god mode which makes the player faster and invincible - CC
    private void GodMode(InputAction.CallbackContext obj)
    {
        if (!godMode)
        {
            godMode = true;
            if(DifficultySettings.PlayerSpeedDiff == false)
            {
                maxSpeed *= 2;
                moveForce *= 2;
            }
        }
        else
        {
            maxSpeed /= 2;
            if (DifficultySettings.PlayerSpeedDiff == false)
            {
                maxSpeed /= 2;
                moveForce /= 2;
            }
        }
    }
    void OnCollisionEnter(Collision other)
    {
        //This stops the player from moving when they are in the water
        if (other.gameObject.CompareTag("Water"))
        {
            moveForce = 0f;
            canMove = false;
            onGround = false;
            inWater = true;
            Debug.Log("Inwater");
        }
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
            rb.AddForce((-transform.forward) * pushForce);
        }
        //This makes the player take damage when they run into the Attackpos gameobject of the dragon.
        if (other.gameObject.CompareTag("attackpos"))
        {
            other.gameObject.SetActive(false);
            other.gameObject.GetComponentInParent<WaterDragonAi>().ResetBite();
            if (DifficultySettings.DragonDMG == true)
            {
                TakeDamage(1);
            }
            else
            {
                TakeDamage(2);
            }
        }
        if (other.gameObject.CompareTag("Dragon"))
        {
            if(DifficultySettings.DragonDMG == true)
            {
                TakeDamage(1);
            }
            else
            {
                TakeDamage(2);
            }
        }
        //This makes the player take damage when they are hit by a projectile.
        if (other.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(1);
        }
    }
    void OnCollisionStay(Collision other)
    {

    }
    void OnCollisionExit(Collision other)
    {
        //This lets the player move again when they jump out of water.
        if (other.gameObject.CompareTag("Water"))
        {
            inWater = false;
            moveForce = moveForce * 2f;
            canMove = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //This saves a checkpoint.
        if (other.gameObject.tag == "Checkpoint")
        {
            Checkpoint();
        }
        //In the fire level this lets the dragon know what room the player is in.
        if (other.gameObject.CompareTag("RoomEnter"))
        {
            other.GetComponent<FireDragonPerch>().playerInRoom = true;
        }
        if (other.gameObject.CompareTag("Wind Gust"))
        {
            // glideSpeed.y = other.GetComponent<WindGust>().force + glideSpeed.y;
            GetComponent<ConstantForce>().force = new Vector3(0, other.GetComponent<WindGust>().force * 10, 0);
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
    //not sure what this is for, this was set up by someone else I just translated it over - CC
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
    //Checkpoints and Loads
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
        Saved = true;
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        currentHealth = maxHealth;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
        stamina = data.Stamina;
        jumpInAir = data.jumpInAir;
        godMode = data.godMode;
        //staminaBar.GetComponent<StaminaBar>().UpdateStamina(Stamina);
        //healthBar?.GetComponent<HealthBar>().SetHealth(currentHealth);
    }

    public void Checkpoint()
    {
        save.SaveEnemy();
        SaveSystem.SavePlayer(this);
        // SaveSystem.Checkpoint(this);
        // Saved = true;
        Debug.Log("Saved");
    }
    public void LoadCheckpoint()
    {
        PlayerData data = SaveSystem.LoadCheckpoint();
        currentHealth = maxHealth;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
        stamina = data.Stamina;
        jumpInAir = data.jumpInAir;
        godMode = data.godMode;
        //staminaBar.GetComponent<StaminaBar>().UpdateStamina(Stamina);
        //healthBar?.GetComponent<HealthBar>().SetHealth(currentHealth);
    }
}
