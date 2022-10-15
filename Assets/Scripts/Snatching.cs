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
    void Start()
    {
        TPController = GetComponent<ThirdPersonController>();
        controls = new ControlsforPlayer();
        controls.Enable();
    }

    void Update()
    {
        snatching = controls.Actions.Snatch.IsPressed();

        if (snatching && !isSnatching)
        {
            isSnatching = true;
            if (TPController.isGrounded)
            {
                print("snatch em ground");
                StartCoroutine(snatchingHitboxLogic(groundHitboxes));
            }
            else
            {
                print("snatch in air");
                GameObject[] array = new GameObject[] { airHitbox };
                StartCoroutine(snatchingHitboxLogic(array));
            }
        }
    }
    private int hitboxNumber = 0;
    IEnumerator snatchingHitboxLogic(GameObject[] hitboxes)
    {
        foreach(GameObject hitbox in hitboxes)
        {
            StartCoroutine(snatchingAttack(hitbox));
            yield return new WaitForSeconds(hixboxTime);
            hitboxNumber++;
        }
        hitboxNumber = 0;
        isSnatching = false;
    }
    IEnumerator snatchingAttack(GameObject hitbox)
    {
        hitbox.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(hixboxTime);
        hitbox.GetComponent<BoxCollider>().enabled = false;
    }
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
}
