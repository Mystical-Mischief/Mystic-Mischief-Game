using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDialogue : MonoBehaviour
{
    public GameObject dialogueCanvas;
    public Dialogue dialogueScript;
    private GameObject player;
    private ControlsforPlayer controls;
    bool inArea;

    private void Start()
    {
        controls = new ControlsforPlayer();
        controls.Enable();
    }
    private void Update()
    {
        if(inArea && controls.Actions.Interact.WasPerformedThisFrame())
        {
            dialogueCanvas.SetActive(true);
            dialogueScript.enabled = true;
            player.GetComponent<ThirdPersonController>().canMove = false;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {  
        //if the player is in the range and they press the interact buttion activate the dialouge part of the canvas and enable the script.
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            inArea = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = null;
            inArea = false;
        }
    }
}

