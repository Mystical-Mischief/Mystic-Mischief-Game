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
    AudioSource audioSource;
    public AudioClip collectClip;
        public AudioClip DropClip;
    public Inventory inv;

    // Start is called before the first frame update
    void Start()
    {
        audioSource= GetComponent<AudioSource>();
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

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInputs.PlayerOnGround.Jump.WasPressedThisFrame() && Player.GetComponent<ThirdPersonController>().Stamina > 0)
        {
            animator.SetTrigger("Jump");
        }
        // if (rb.velocity.magnitude >= 8 && Player.GetComponent<ThirdPersonController>().isGrounded == true)
        // {
        //     animator.SetFloat("RunSpeed", 2f);
        // // animator.SetTrigger("Launch");
        // }
                if (rb.velocity.magnitude >=1 && Player.GetComponent<ThirdPersonController>().isGrounded == true)
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

        if (controls.Inv.Drop.WasPerformedThisFrame())
        {
            PlaySound(DropClip);
            animator.SetTrigger("Drop");
        }
        if (controls.Inv.Store.WasPressedThisFrame())
        {
            PlaySound(collectClip);
            animator.SetTrigger("Store");
        }
    }
}
