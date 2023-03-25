using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Rigidbody rb;
    public AudioClip DropClip;
    public AudioClip PlayerCaw;
    public AudioClip HurtClip;
    public PlayerController Player;
    [SerializeField] private GameObject flyingEffets;
    [SerializeField] private ParticleSystem damageVFX;
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


        Player.GotHurt += PlayDamageVFX; //Subscribing to the GotHurt Event -Emilie
    }

    private void PlayDamageVFX(object sender, EventArgs e)
    {
        damageVFX.Play();
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
        if (Player.canMove && !PauseMenu.GameIsPaused)
        {
            Animations();
        }
        SoundFXs();
    }
    void Animations()
    {
        if(animator.GetBool("Grounded") != Player.GetComponent<PlayerController>().onGround)
        {
            animator.SetBool("Grounded", Player.GetComponent<PlayerController>().onGround);
            if (Player.onGround)
            {
                flyingEffets.SetActive(false);
            }
            else
            {
                flyingEffets.SetActive(true);
            }
        }
        if (playerInputs.PlayerOnGround.Jump.WasPressedThisFrame() && Player.stamina > 0)
        {
            animator.SetTrigger("Jump");
        }
        if (rb.velocity.magnitude >= 1 && Player.onGround == true)
        {
            animator.SetFloat("RunSpeed", 1f);
        }
        if (rb.velocity.magnitude < 6 && Player.onGround == true)
        {
            animator.SetFloat("RunSpeed", 0f);
        }
        if (Player.onGround == false)
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
    bool waitOnSFX;
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
        if (waitOnSFX || !Player.damaged)
        {
            waitOnSFX = true;
            if(Player.damaged == true)
            {
                PlaySound(HurtClip);
                waitOnSFX = false;
            }
        }
    }
} //my butt hurts
//the struggles of fat assery
