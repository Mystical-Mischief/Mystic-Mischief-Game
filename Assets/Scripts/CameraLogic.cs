using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    public Vector2 turn;
    public float sensitivity;
    private ThirdPersonInputs inputs; 
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        inputs = new ThirdPersonInputs();
        inputs.Enable();
        turn = inputs.PlayerOnGround.Look.ReadValue<Vector2>();
    }
    void Update()
    {
        turn.x += inputs.PlayerOnGround.Look.ReadValue<Vector2>().x / sensitivity;
        turn.y += inputs.PlayerOnGround.Look.ReadValue<Vector2>().y / sensitivity;
        Quaternion newRotation = Quaternion.Euler(turn.y, turn.x, 0);
        transform.localRotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * 2);
        if (inputs.Test.UnlockMouse.WasPerformedThisFrame())
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
