using UnityEngine;
using UnityEngine.InputSystem;

public class PoopMechanic : MonoBehaviour
{
    public Rigidbody poop;
    public Transform poopPosition;
    //public float resetPoopTime;
    public static bool isPooping;
    ControlsforPlayer controls;
    //bool Pooped;
    public AudioSource poopSound;
    public AudioClip PoopClip;
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        poopSound = GetComponent<AudioSource>();
        controls = new ControlsforPlayer();
        controls.Enable();
        controls.Actions.Poop.canceled += Poop;
    }
 
    public void Poop(InputAction.CallbackContext obj)
    {
        if(playerController.onGround ==false)
        {
            PlaySound(PoopClip);
            Rigidbody clone;
            clone = Instantiate(poop, poopPosition.position, transform.rotation);
        }
        
    }


    public void PlaySound(AudioClip clip)
    {
        poopSound.PlayOneShot(clip);
    }
}
