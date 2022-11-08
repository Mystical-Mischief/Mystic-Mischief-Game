using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDialogue : MonoBehaviour
{
    public GameObject dialogueCanvas;
    public Dialogue dialogueScript;
    private GameObject player;
    private ControlsforPlayer controls;

    private void Start()
    {
        controls = new ControlsforPlayer();
        controls.Enable();
    }
    private void OnTriggerEnter(Collider collision)
    {  
        if (collision.gameObject.tag == "Player" && controls.Actions.Interact.WasPressedThisFrame())
        {
            dialogueCanvas.SetActive(true);
            dialogueScript.enabled = true;
        }
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && controls.Actions.Interact.WasPressedThisFrame())
        {
            dialogueCanvas.SetActive(true);
            dialogueScript.enabled = true;
        }
    }
}

