using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public List<GameObject> StoredItems = new List<GameObject>();
    public MonoBehaviour dialouge;
    public GameObject ticketText;

    private void Start()
    {
        control = new ControlsforPlayer();
        control.Enable();
        if (ticketCount >= TicketAmount)
        {
            Destroy(GetComponent<Chest>());
            Destroy(this);
        }
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
                dialouge.enabled = true;
                Hat.SetActive(true);
                Destroy(GetComponent<Chest>());
                ticketText.SetActive(false);
                Destroy(this);
            }
            if (ticketCount > 0 && ticketCount < TicketAmount)
            {
                if (!ticketText.activeInHierarchy)
                {
                    ticketText.SetActive(true);
                }
                ticketText.GetComponent<TextMeshProUGUI>().text = $"{ticketCount} / {TicketAmount} Tickets";
            }
            foreach (GameObject items in StoredItems)
            {
                if (items.gameObject.GetComponent<Item>().itemType == Item.ItemType.Objective)
                {
                    // ticketCount = ticketCount - 1;
                    items.SetActive(false);
                }
            }
        }

        void BirdAction()
        {
            faceChange.talking = true;
            anim.SetBool("Talking", true);
            // foreach (GameObject Inv in inv.PickedUpItems)
            // {
            //     if (Inv.gameObject.GetComponent<Item>().itemType == Item.ItemType.Objective)
            //     {
            //         ticketCount = ticketCount + 1;
            //     }
            // }
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
            if (other.gameObject.tag == "Player")
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
            // ticketCount = 0;
        }
        //     void OnCollisionEnter(Collision other)
        // {
        //     if (other.gameObject.CompareTag("PickUp"))
        //     {
        //         other.gameObject.SetActive(false);
        //         // StoredItems.Add(other.gameObject);
        //         ticketCount = ticketCount + 1;

        //     }
        // }
    }
