using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Rigidbody rb;
    Animator animator;
    private ThirdPersonControl playerInputs;
    ControlsforPlayer controls;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        playerInputs = new ThirdPersonControl();
        animator = GetComponent<Animator>();
        controls = new ControlsforPlayer();
        controls.Enable();
        playerInputs.Enable();
    }
    public void OnEnable()
    {
        if(playerInputs != null)
        {
            playerInputs.Enable();
        }
        if (controls != null)
        {
            controls.Enable();
        }
    }
    public void OnDisable()
    {
        playerInputs.Disable();
        controls.Enable();
    }


    // Update is called once per frame
    void Update()
    {
        if (playerInputs.PlayerOnGround.Jump.WasPressedThisFrame() && Player.GetComponent<ThirdPersonController>().Stamina > 0)
        {
            animator.SetTrigger("Jump");
        }
        if (rb.velocity.magnitude >= 8 && Player.GetComponent<ThirdPersonController>().isGrounded == true)
        {
            animator.SetFloat("RunSpeed", 2f);
        // animator.SetTrigger("Launch");
        }
                if (rb.velocity.magnitude >= 6 && rb.velocity.magnitude < 8 && Player.GetComponent<ThirdPersonController>().isGrounded == true)
        {
            animator.SetFloat("RunSpeed", 1f);
        // animator.SetTrigger("Launch");
        }
            if (rb.velocity.magnitude < 6 && Player.GetComponent<ThirdPersonController>().isGrounded == true)
        {
            animator.SetFloat("RunSpeed", 0f);
        // animator.SetTrigger("Launch");
        }
            if (Player.GetComponent<ThirdPersonController>().isGrounded == false)
        {
            animator.SetFloat("RunSpeed", 0f);
        // animator.SetTrigger("Launch");
        }
        if (controls.Actions.Dive.WasPressedThisFrame())
        {
            Debug.Log("Diving");
        }
        if (controls.Actions.Snatch.WasPressedThisFrame())
        {
            animator.SetTrigger("Pick");
        }
        if (controls.Inv.PressPick.WasPressedThisFrame())
        {
            animator.SetTrigger("Pick");
        }

        if (controls.Inv.Drop.WasPressedThisFrame())
        {
            animator.SetTrigger("Drop");
        }
        if (controls.Inv.Store.WasPerformedThisFrame())
        {
            animator.SetTrigger("Store");
        }
    }
}
