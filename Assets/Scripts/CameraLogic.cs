using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    public Vector2 turn;
    public bool isFlying;
    public float sensitivity;
    public float groundMaxYRotation, groundMinYRotation, airMaxXRotation, airMinXRotation;
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
        if (isFlying)
        {
            turn.x = player.transform.rotation.y * 100 + (inputs.PlayerOnGround.Look.ReadValue<Vector2>().x / sensitivity);
            turn.y = 0;
            if (!isFlying)
            {
                turn.y = player.transform.rotation.x * 100;
            }
        }
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
        }
        
        Quaternion newRotation = Quaternion.Euler(turn.y, -turn.x, 0);
        transform.localRotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * 2);
        if (inputs.Test.UnlockMouse.WasPerformedThisFrame())
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
