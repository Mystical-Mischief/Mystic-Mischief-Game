using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snatching : MonoBehaviour
{
    public float hixboxTime;
    public GameObject[] groundHitboxes;
    public GameObject airHitbox;

    private bool isSnatching;
    ThirdPersonController TPController;
    ControlsforPlayer controls;
    private bool snatching;
    public GameObject hat1;
    public GameObject hat2;
    public GameObject hat3;
    void Start()
    {
        TPController = GetComponent<ThirdPersonController>();
        controls = new ControlsforPlayer();
        controls.Enable();
    }

    void Update()
    {
        snatching = controls.Actions.Snatch.IsPressed();
        //if the player snatchs and is currently not snatching run the snatching function
        if (snatching && !isSnatching && !GetComponent<Inventory>().holdingItem && !GetComponent<Inventory>().canGrabItem)
        {
            isSnatching = true;
            // if the player is on the ground use the ground hitbox. 
            if (TPController.isGrounded)
            {
                print("snatch em ground");
                StartCoroutine(snatchingHitboxLogic(groundHitboxes));
            }
            else
            {
                //if the player is in the air snatch in the air
                print("snatch in air");
                GameObject[] array = new GameObject[] { airHitbox };
                StartCoroutine(snatchingHitboxLogic(array));
            }
        }
    }
    private int hitboxNumber = 0;
    //runs through the list of hitboxes, activates it, waits for the hitbox time to end then runs the next one until all of them have been ran
    IEnumerator snatchingHitboxLogic(GameObject[] hitboxes)
    {
        foreach (GameObject hitbox in hitboxes)
        {
            StartCoroutine(snatchingAttack(hitbox));
            yield return new WaitForSeconds(hixboxTime);
            hitboxNumber++;
        }
        hitboxNumber = 0;
        isSnatching = false;
    }
    //activates and deactivates each hitbox based on a timer
    IEnumerator snatchingAttack(GameObject hitbox)
    {
        hitbox.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(hixboxTime);
        hitbox.GetComponent<BoxCollider>().enabled = false;
    }
    //for visuals in the scene. draws a red box for ground hitbox and a blue hitbox for air hitboxes
    private void OnDrawGizmos()
    {
        if (isSnatching)
        {
            if (TPController.isGrounded)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(groundHitboxes[hitboxNumber].transform.position, groundHitboxes[hitboxNumber].GetComponent<BoxCollider>().size);
            }
            else
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawCube(airHitbox.transform.position, airHitbox.GetComponent<BoxCollider>().size);
            }
        }
    }

    public PlayerHatLogic playerHatLogic;
    private void OnTriggerStay(Collider other)
    {
            if (other.gameObject.tag == "Hat" && isSnatching == true)
        {
            if (other.gameObject.GetComponent<HatPickup>().hatType == HatPickup.HatType.first)
            {
            playerHatLogic.hats[0] = hat1;
            other.gameObject.SetActive(false);
            }
            if (other.gameObject.GetComponent<HatPickup>().hatType == HatPickup.HatType.second)
            {
            playerHatLogic.hats[1] = hat2;
            other.gameObject.SetActive(false);
            }
            if (other.gameObject.GetComponent<HatPickup>().hatType == HatPickup.HatType.third)
            {
            playerHatLogic.hats[2] = hat3;
            other.gameObject.SetActive(false);
            }
        }
    }
}
