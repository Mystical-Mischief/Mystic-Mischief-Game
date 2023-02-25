using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Rigidbody rb;
    public AudioClip DropClip;
    public AudioClip PlayerCaw;
    public GameObject Player;
    [SerializeField] private GameObject flyingEffets;
    Animator animator;
    private ThirdPersonControl playerInputs;
    ControlsforPlayer controls;
    AudioSource audioSource;


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
    void Update()
    {
        Animations();

        SoundFXs();
    }
    void Animations()
    {
        if(animator.GetBool("Grounded") != Player.GetComponent<PlayerController>().onGround)
        {
            animator.SetBool("Grounded", Player.GetComponent<PlayerController>().onGround);
            if (Player.GetComponent<PlayerController>().onGround)
            {
                flyingEffets.SetActive(false);
            }
            else
            {
                flyingEffets.SetActive(true);
            }
        }
        if (playerInputs.PlayerOnGround.Jump.WasPressedThisFrame() && Player.GetComponent<PlayerController>().stamina > 0)
        {
            animator.SetTrigger("Jump");
        }
        if (rb.velocity.magnitude >= 1 && Player.GetComponent<PlayerController>().onGround == true)
        {
            animator.SetFloat("RunSpeed", 1f);
        }
        if (rb.velocity.magnitude < 6 && Player.GetComponent<PlayerController>().onGround == true)
        {
            animator.SetFloat("RunSpeed", 0f);
        }
        if (Player.GetComponent<PlayerController>().onGround == false)
        {
            animator.SetFloat("RunSpeed", 0f);
        }
        if (controls.Actions.Dive.IsPressed())
        {
            animator.SetBool("IsDiving", true);
        }
        if (controls.Actions.Dive.WasReleasedThisFrame())
        {
            animator.SetBool("IsDiving", false);
        }
        if (controls.Actions.Snatch.WasPressedThisFrame())
        {
            animator.SetTrigger("Pick");
        }
        if (controls.Inv.PressPick.WasPressedThisFrame())
        {
            animator.SetTrigger("Pick");
        }
    }
    void SoundFXs()
    {
        if (controls.Inv.Drop.WasPerformedThisFrame())
        {
            PlaySound(DropClip);
            animator.SetTrigger("Drop");
        }
        if (controls.Actions.Caw.WasPerformedThisFrame())
        {
            PlaySound(PlayerCaw);
        }
    }
}
