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
}
