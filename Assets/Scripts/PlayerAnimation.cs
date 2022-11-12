using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Rigidbody rb;
    Animator animator;
    private ThirdPersonControl playerInputs;
    public GameObject Player;

        public void OnEnable()
    {
        playerInputs.Enable();
    }
    public void OnDisable()
    {
        playerInputs.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInputs = new ThirdPersonControl();
        animator = GetComponent<Animator>();
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
    }
}
