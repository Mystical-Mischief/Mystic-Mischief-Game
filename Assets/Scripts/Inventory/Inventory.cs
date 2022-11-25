using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using System;

public class Inventory : MonoBehaviour
{
    ControlsforPlayer controls;
    private Rigidbody rb;
    public List<GameObject> PickedUpItems = new List<GameObject>();
    public GameObject currentHeldItem;
    public Transform HoldItemPosition;

    private bool PickUp;
    private bool Store;
    public bool holdingItem;
    private int TicketAmount;
    public GameObject Ticket;
    private float startMass;
    public GameObject hat1;
    public GameObject hat2;
    public GameObject hat3;
    public Text Weight;
    public GameObject InventorySlot;
    public GameObject InventoryUI;
    public bool showinventory;
    public List<GameObject> InventorySlots = new List<GameObject>();

    void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
        controls = new ControlsforPlayer();
        startMass = rb.mass;

    }
    public void OnEnable()
    {
        controls.Enable();
    }
    public void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        controls.Enable();
    }

    void Update()
    {
        if (showinventory == true)
        {
            InventoryUI.SetActive(true);
            foreach (GameObject item in PickedUpItems)
            {
                GameObject InventorySlot = Instantiate(InventorySlots[0]);
            }
        }
        // float Mass = rb.mass.ToString();
        Weight.text = rb.mass.ToString("F1");
        bool Load = controls.MenuActions.Load.ReadValue<float>() > 0.1f;
        bool Save = controls.MenuActions.Save.ReadValue<float>() > 0.1f;
        // if (Save)
        // {
        //     SaveInventory();
        // }
        // if (Load)
        // {
        //     LoadInventory();
        // }
        TicketAmount = PickedUpItems.Count;
        Store = controls.Inv.Store.IsPressed();
        PickUp = controls.Inv.PressPick.IsPressed();
        // Save = controls.MenuActions.Save.IsPressed();
        // Load = controls.MenuActions.Load.IsPressed();

        /* if (controls.Inv.PressPick.WasPerformedThisFrame())
         {
             PickUp = true;
         }
         if (controls.Inv.Store.WasPerformedThisFrame())
         {
             Store = true;
         }*/

        //Drop the Current Item
        bool Drop = controls.Inv.Drop.ReadValue<float>() > 0.1f;
        if (PickedUpItems.Count > 0 && controls.Inv.Drop.triggered)
        {
            QuickDropStoredItem(PickedUpItems[PickedUpItems.Count - 1]);
        }
        if (holdingItem && PickUp && controls.Inv.PressPick.WasPressedThisFrame())
        {
            DropItem(currentHeldItem);
        }
        if (holdingItem && Store && controls.Inv.Store.WasPressedThisFrame())
        {
            StoreItem(currentHeldItem);
        }
        // if (controls.MenuActions.Save.WasPressedThisFrame())
        // {
        //     SaveManager.SaveJsonData();
        // }
    }

    public void QuickDropStoredItem(GameObject Item)
    {
        holdingItem = false;
        rb.mass = rb.mass - Item.GetComponent<Item>().Weight;
        Item.transform.position = transform.position;
        Item.transform.parent = null;
        Item.GetComponent<Rigidbody>().isKinematic = false;
        Item.GetComponent<Item>().inInventory = false;
        PickedUpItems.RemoveAt(PickedUpItems.Count - 1); ;
        Item.SetActive(true);
        Item.GetComponent<SphereCollider>().enabled = true;
        Item.GetComponent<BoxCollider>().enabled = true;
    }
    public void StoreItem(GameObject item)
    {
        Debug.Log("Item Stored");
        item.SetActive(false);
        holdingItem = false;
        PickedUpItems.Add(item);
        rb.mass = rb.mass + item.GetComponent<Item>().Weight;

    }
    public void HoldItem(GameObject Item)
    {
        Item.GetComponent<SphereCollider>().enabled = false;
        Item.GetComponent<BoxCollider>().enabled = false;
        Item.GetComponent<Item>().inInventory = true;
        Item.transform.parent = gameObject.transform;
        Item.transform.position = HoldItemPosition.position;
        Item.GetComponent<Rigidbody>().isKinematic = true;
        holdingItem = true;
        currentHeldItem = Item;
    }
    public void DropItem(GameObject Item)
    {
        Item.GetComponent<SphereCollider>().enabled = true;
        Item.GetComponent<BoxCollider>().enabled = true;
        Item.transform.parent = null;
        Item.GetComponent<Rigidbody>().isKinematic = false;
        currentHeldItem = null;
        Item.GetComponent<Item>().inInventory = false;

        StartCoroutine(dropTimer(0.5f, false));
    }
    IEnumerator dropTimer(float time, bool value)
    {
        yield return new WaitForSeconds(time);
        holdingItem = value;
    }
    public PlayerHatLogic playerHatLogic;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PickUp" && Store && holdingItem == false)
        {
            StoreItem(other.gameObject);
        }
        if (other.gameObject.tag == "Hat" && Store && holdingItem == false)
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
        if (other.gameObject.tag == "PickUp" && PickUp && holdingItem == false)
        {
            HoldItem(other.gameObject);
        }
        if (other.gameObject.tag == "Goal")
        {
            for (var i = 0; i < PickedUpItems.Count; i++)
            {
                PickedUpItems.RemoveAt(i);
                rb.mass = startMass;
            }
        }
                if (other.gameObject.tag == "GoldToTickets")
        {
            for (var i = 0; i < PickedUpItems.Count; i++)
            {
                PickedUpItems.RemoveAt(i);
                Instantiate(Ticket, transform.position, Quaternion.identity);
                rb.mass = startMass;
            }
        }
    }

            public void SaveInventory ()
    {
        SaveSystem.SaveInventory(this);
    }
    public void LoadInventory ()
    {
        InventoryData data = SaveSystem.LoadInventory();
        PickedUpItems = data.PickedUpItems;
    }
}