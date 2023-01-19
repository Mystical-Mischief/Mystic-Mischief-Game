using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdInteraction : MonoBehaviour
{
    public bool inRange;
    private ControlsforPlayer control;
    public Animator anim;
    public GameObject Hat;
    public Inventory inv;
    public float ticketCount;
    public float TicketAmount;
    public CleverBirdFaceChange faceChange;

    private void Start()
    {
        control = new ControlsforPlayer();
        control.Enable();
    }
    void Update()
    {
        // if(inRange == true && control.Actions.Interact.WasPressedThisFrame())
        // {
        //     BirdAction();
        // }
        if (inRange == true)
        {
            BirdAction();
        }
        if (ticketCount >= TicketAmount)
        {
            Hat.SetActive(true);
        }
    }

    void BirdAction()
    {
        faceChange.talking = true;
        anim.SetBool("Talking", true);
        foreach (GameObject Inv in inv.PickedUpItems)
        {
            if (Inv.gameObject.GetComponent<Item>().itemType == Item.ItemType.Objective)
            {
                ticketCount = ticketCount + 1;
            }
        }
        // if (inv.Objective.Count >= 1)
        // {
            
        // }
        // SceneManager.LoadScene("MainMenu");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
            anim.SetBool("Talking", false);
        }
        faceChange.talking = false;
        ticketCount = 0;
    }
}
