using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    public Vector2 turn;
    public bool isFlying;
    public float sensitivity;
    public float groundMaxYRotation, groundMinYRotation;
    private ThirdPersonControl inputs;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Cursor.lockState = CursorLockMode.Locked;
        inputs = new ThirdPersonControl();
        inputs.Enable();
        turn = inputs.PlayerOnGround.Look.ReadValue<Vector2>();
    }
    void Update()
    {
        isFlying = !player.GetComponent<ThirdPersonController>().isGrounded;
        //if the player is flying use the fly camera
        if (isFlying)
        {
            turn.x += inputs.PlayerOnGround.Look.ReadValue<Vector2>().x / sensitivity;
            turn.y += inputs.PlayerOnGround.Look.ReadValue<Vector2>().y / sensitivity;

            if (turn.x > 360)
            {
                turn.x -= 360;
            }
            if (turn.x < -360)
            {
                turn.x += 360;
            }
            if(turn.y < -45)
            {
                turn.y = -45;
            }
            if (turn.y > 45)
            {
                turn.y = 45;
            }
            Quaternion newRotation = Quaternion.Euler(turn.y, turn.x, 0);
            transform.localRotation = Quaternion.Lerp(transform.rotation, newRotation, 1);
        }
        //if the player is grounded use the ground camera
        else
        {
            turn.x += inputs.PlayerOnGround.Look.ReadValue<Vector2>().x / sensitivity;
            turn.y += inputs.PlayerOnGround.Look.ReadValue<Vector2>().y / sensitivity;
            if (turn.y > groundMaxYRotation)
            {
                turn.y = groundMaxYRotation;
            }
            if (turn.y < groundMinYRotation)
            {
                turn.y = groundMinYRotation;
            }
            if (turn.x > 360)
            {
                turn.x -= 360;
            }
            if (turn.x < -360)
            {
                turn.x += 360;
            }
            Quaternion newRotation = Quaternion.Euler(turn.y, turn.x, 0);
            transform.localRotation = Quaternion.Lerp(transform.rotation, newRotation, 1);
            //}
            //unlocks the mouse when the button is pressed
            if (inputs.Test.UnlockMouse.WasPerformedThisFrame())
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

        public void SaveCamera()
        {
            SaveSystem.SaveCamera(this);
        }
        public void LoadCamera ()
        {
        CameraData data = SaveSystem.LoadCamera();
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
        Vector2 rotation;
        turn.x = data.rotation[0];
        turn.y = data.rotation[1];
        
        // rotation.z = data.rotation[2];
        // Quaternion quaternion = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        float smooth = 5.0f;
        // transform.rotation = Quaternion.Slerp(transform.rotation, quaternion,  Time.deltaTime * smooth);
        //staminaBar.GetComponent<StaminaBar>().UpdateStamina(Stamina);
        //healthBar?.GetComponent<HealthBar>().SetHealth(currentHealth);
    }
}
