using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopMechanic : MonoBehaviour
{
    public Rigidbody poop;
    public Transform poopPosition;
    public float resetPoopTime;
    public bool Pooped;
    private bool isPooping;
    ControlsforPlayer controls;

    public AudioSource poopSound;
    public AudioClip PoopClip;

    // Start is called before the first frame update
    void Start()
    {
        poopSound = GetComponent<AudioSource>();
        controls = new ControlsforPlayer();
        controls.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        isPooping = controls.Actions.Poop.IsPressed();

        if (isPooping && Pooped == false)
        {
            Poop();
        }
        
    }

    public void Poop()
    {
        PlaySound(PoopClip);
        Rigidbody clone;
        clone = Instantiate(poop, poopPosition.position, transform.rotation);
        Invoke(nameof(ResetPoop), resetPoopTime);
        Pooped = true;
    }

    public virtual void ResetPoop()
    {
        Pooped = false;
    }

    public void PlaySound(AudioClip clip)
    {
        poopSound.PlayOneShot(clip);
    }
}
