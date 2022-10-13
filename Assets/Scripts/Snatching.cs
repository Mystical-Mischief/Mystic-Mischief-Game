using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snatching : MonoBehaviour
{
    public float hixboxTime;
    public GameObject[] groundHitboxes;
    public GameObject airHitbox;

    ThirdPersonController TPController;
    ControlsforPlayer controls;
    private bool snatching;
    void Start()
    {
        TPController = GetComponent<ThirdPersonController>();
        controls = new ControlsforPlayer();
        controls.Enable();
    }

    void Update()
    {
        snatching = controls.Actions.Snatch.IsPressed();

        if (snatching)
        {
            if (TPController.isGrounded)
            {
                print("snatch em ground");
                StartCoroutine(changeMultipleHitboxes(groundHitboxes));
            }
            else
            {
                print("snatch in air");
                StartCoroutine(snatchingAttack(airHitbox, hixboxTime));
            }
            
        }
    }
    IEnumerator changeMultipleHitboxes(GameObject[] hitboxes)
    {
        foreach(GameObject hitbox in hitboxes)
        {
            StartCoroutine(snatchingAttack(hitbox, hixboxTime));
            yield return new WaitForSeconds(hixboxTime);
        }
    }
    IEnumerator snatchingAttack(GameObject hitbox, float time)
    {
        hitbox.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(time);
        hitbox.GetComponent<BoxCollider>().enabled = false;
    }
}
