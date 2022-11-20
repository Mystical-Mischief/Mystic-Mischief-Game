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
        //if the player is in the range and they press the interact buttion activate the dialouge part of the canvas and enable the script.
        if (collision.gameObject.tag == "Player" && controls.Actions.Interact.WasPressedThisFrame())
        {
            dialogueCanvas.SetActive(true);
            dialogueScript.enabled = true;
        }
    }
    private void OnTriggerStay(Collider collision)
    {
        //if the player is in the range and they press the interact buttion activate the dialouge part of the canvas and enable the script.
        if (collision.gameObject.tag == "Player" && controls.Actions.Interact.WasPressedThisFrame())
        {
            dialogueCanvas.SetActive(true);
            dialogueScript.enabled = true;
        }
    }
}

