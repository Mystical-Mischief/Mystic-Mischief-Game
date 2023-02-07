using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using System;
using TMPro; 

public class Inventory : MonoBehaviour
{
    public TextMeshProUGUI Weight;
    public float MassText;
    public float weightFloat;
    [HideInInspector]
    public ControlsforPlayer controls;
    private Rigidbody rb;
    public List<GameObject> PickedUpItems = new List<GameObject>();
    public List<GameObject> Objective = new List<GameObject>();
    public GameObject currentHeldItem;
    public GameObject currentObject;
    public Transform HoldItemPosition;
    public bool canGrabItem;
    private Item heldItem;
    private bool PickUp;
    private bool Store;
    public bool holdingItem;
    private int TicketAmount;
    public GameObject Ticket;
    private float startMass;
    public GameObject hat1;
    public GameObject hat2;
    public GameObject hat3;
    public GameObject hat4;
    // public Text Weight;
    // public GameObject InventorySlot;
    public GameObject InventoryUI;
    public GameObject InventoryImages;
    public bool UIOpen;
    private float UITime = 3f;
    public float HoldTime;
    private float Holding;
    public IntegerControl tapCount { get; set; }
    // public bool showinventory;
    // public List<GameObject> InventorySlots = new List<GameObject>();


    [SerializeField] private ParticleSystem storeParticles;

    void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
        controls = new ControlsforPlayer();
        startMass = rb.mass;

    }
    public void OnEnable()
    {
        controls.Enable();

        controls.Inv.Drop.started += DoDrop;
    }
    public void OnDisable()
    {
        controls.Disable();

        controls.Inv.Drop.started -= DoDrop;
    }

    void Start()
    {
        controls.Enable();
    }

    void Update()
    {
        // if (showinventory == true)
        // {
        //     InventoryUI.SetActive(true);
        //     foreach (GameObject item in PickedUpItems)
        //     {
        //         GameObject InventorySlot = Instantiate(InventorySlots[0]);
        //     }
        // }
        // float Mass = rb.mass.ToString();
        //Weight.text = ("Weight: " + MassText.ToString("F2")+"lb");
        weightFloat = rb.mass;
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
        // currentObject = PickedUpItems[PickedUpItems.Count - 1];
        bool Drop = controls.Inv.Drop.ReadValue<float>() > 1f;
        // HoldTime = controls.Inv.Drop.tapCount();
        if (controls.Inv.OpenInv.WasPerformedThisFrame())
        {
            if (UIOpen == false){
                OpenUI();
            }
            else {CloseUI();}
        }
        // if (UIOpen == true && controls.Inv.OpenInv.WasPerformedThisFrame())
        // {
        //     CloseUI();
        // }
            // if ((controls.Inv.Drop.ReadValue<Hold>()))
            // {

            // }
            if (controls.Inv.Drop.WasPerformedThisFrame()){
            QuickDropStoredItem(PickedUpItems[PickedUpItems.Count - 1]);
            }
        // else if (controls.Inv.Drop.ReadValue<float>() < 1f && controls.Inv.Drop.ReadValue<float>() > 0.1f)
        // {
        //     
        // }

        if (holdingItem && PickUp && controls.Inv.PressPick.WasPressedThisFrame())
        {
            DropItem(currentHeldItem);
        }
        // if (holdingItem && Store && controls.Inv.Store.WasPressedThisFrame())
        // {
        //     StoreItem(currentHeldItem);
        // }
        // if (controls.MenuActions.Save.WasPressedThisFrame())
        // {
        //     SaveManager.SaveJsonData();
        // }
    }
        private void DoDrop(InputAction.CallbackContext obj)
    {
        Holding = obj.ReadValue<float>();

        // HoldTime += Time.deltaTime;
    }

    public void OpenUI()
    {
        // Debug.Log("Pressed");
        // InventoryImages.SetActive(true);
        // UIOpen = true;
    }
    public void CloseUI()
    {
        // InventoryImages.SetActive(false);
        // UIOpen = false;
    }

    public void QuickDropStoredItem(GameObject Item)
    {
        // var ItemDropping = 0;
        // var itemIndexPos = 0;
        // if (Item.GetComponent<Item>().canDrop == true)
        // {
        //     InventoryUI.GetComponent<InventoryUI>().DropLastItemUI();
        //     // Item.GetComponent<Item>().inInventory = false;
        //     holdingItem = false;
        //     MassText = MassText - Item.GetComponent<Item>().Weight;
        //     rb.mass = rb.mass - (Item.GetComponent<Item>().Weight * 0.2f);
        //     Item.transform.position = transform.position;
        //     Item.transform.parent = null;
        //     Item.GetComponent<Rigidbody>().isKinematic = false;
        //     PickedUpItems.RemoveAt(PickedUpItems.Count - 1); ;
        //     Item.SetActive(true);
        //     Item.GetComponent<SphereCollider>().enabled = true;
        //     Item.GetComponent<BoxCollider>().enabled = true;

        //     if (storeParticles != null)
        //     {
        //         Instantiate(storeParticles, transform.position, transform.rotation);
        //     }
        // }
        // else if (PickedUpItems[PickedUpItems.Count - 1].gameObject.GetComponent<Item>().canDrop == false) { 
            
        //                         foreach (GameObject i in PickedUpItems)
        //             {
        //                 if (i.gameObject.GetComponent<Item>().canDrop == true)
        //                 {
        //                     Item = i;
        //                     break;
        //                 }
        //                 InventoryUI.GetComponent<InventoryUI>().DropLastItemUI();
        //             }

        //             if (Item.GetComponent<Item>().canDrop == true){
        //     holdingItem = false;
        // MassText = MassText - Item.GetComponent<Item>().Weight;
        // rb.mass = rb.mass - (Item.GetComponent<Item>().Weight * 0.2f);
        // Item.transform.position = transform.position;
        // Item.transform.parent = null;
        // Item.GetComponent<Rigidbody>().isKinematic = false;
        // PickedUpItems.Remove(Item);
        // Item.SetActive(true);
        // Item.GetComponent<SphereCollider>().enabled = true;
        // Item.GetComponent<BoxCollider>().enabled = true;
        //                 GameObject temp = PickedUpItems[ItemDropping];
        //             PickedUpItems[itemIndexPos] = PickedUpItems[PickedUpItems.Count - 1];
        //             PickedUpItems[PickedUpItems.Count - 1] = temp; 
        //             }
        //         }
        }
    public void StoreItem(GameObject item)
    {
        Debug.Log("Item Stored");

        item.SetActive(false);
        holdingItem = false;
        // item.GetComponent<Item>().inInventory = true;
        PickedUpItems.Add(item);
        // rb.mass = rb.mass + (item.GetComponent<Item>().Weight * 0.2f);
        MassText = MassText + item.GetComponent<Item>().Weight;

        if (storeParticles != null)
        {
            Instantiate(storeParticles, transform.position, transform.rotation);
        }

    }

    public void StoreObjective(GameObject item)
    {
        Debug.Log("Item Stored");
        item.SetActive(false);
        holdingItem = false;
        // item.GetComponent<Item>().inInventory = true;
        Objective.Add(item);
        rb.mass = rb.mass + (item.GetComponent<Item>().Weight * 0.2f);
        MassText = MassText + item.GetComponent<Item>().Weight;

        if (storeParticles != null)
        {
            Instantiate(storeParticles, transform.position, transform.rotation);
        }

    }
    public void HoldItem(GameObject Item)
    {
        if (Item.GetComponent<Item>().ps != null)
        {
            Item.GetComponent<Item>().ps.Play(true);
        }
        
        Item.GetComponent<SphereCollider>().enabled = false;
        Item.GetComponent<BoxCollider>().enabled = false;
        // Item.GetComponent<Item>().inInventory = true;
        Item.transform.parent = gameObject.transform;
        Item.transform.position = HoldItemPosition.position;
        Item.transform.rotation = HoldItemPosition.rotation;
        Item.GetComponent<Rigidbody>().isKinematic = true;
        holdingItem = true;
        currentHeldItem = Item;
        heldItem = Item.GetComponent<Item>();
    }
    public void DropItem(GameObject Item)
    {
        // Item.GetComponent<Item>().inInventory = false;
        Item.GetComponent<SphereCollider>().enabled = true;
        Item.GetComponent<BoxCollider>().enabled = true;
        Item.transform.parent = null;
        Item.GetComponent<Rigidbody>().isKinematic = false;
        currentHeldItem = null;
        heldItem.dropped = true;
        StartCoroutine(dropTimer(0.5f, false));

        if (storeParticles != null)
        {
            Instantiate(storeParticles, transform.position, transform.rotation);
        }
    }
    IEnumerator dropTimer(float time, bool value)
    {
        yield return new WaitForSeconds(time);
        holdingItem = value;
    }
    public PlayerHatLogic playerHatLogic;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PickUp")
        {
            canGrabItem = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            canGrabItem = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PickUp" && Store && holdingItem == false)
        {
            // if (other.gameObject.GetComponent<Item>().itemType == Item.ItemType.Objective)
            // {
            //     StoreObjective(other.gameObject);
            // }
            if (other.gameObject.GetComponent<Item>().itemType == Item.ItemType.Objective)
            {
            StoreItem(other.gameObject); 
            }
        }
        if (other.gameObject.tag == "Hat" && PickUp && holdingItem == false)
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
            if (other.gameObject.GetComponent<HatPickup>().hatType == HatPickup.HatType.forth)
            {
                playerHatLogic.hats[3] = hat4;
                other.gameObject.SetActive(false);
            }
        }
        if (other.gameObject.tag == "PickUp" && PickUp && holdingItem == false && other.gameObject.GetComponent<Item>().enabled == true)
        {
            HoldItem(other.gameObject);
        }
        if (other.gameObject.tag == "Goal")
        {
            // for (var i = 0; i < PickedUpItems.Count; i++)
            // {
            //     other.gameObject.GetComponent<Goal>().StoredItems.Add(PickedUpItems[i]);
            //     InventoryUI.GetComponent<InventoryUI>().DropLastItemUI();
            //     // Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
            //     PickedUpItems.RemoveAt(i);
            //     rb.mass = startMass;
            // }
            // if (PickedUpItems.Count <= 0 && other.gameObject.GetComponent<Goal>().TurnsOff == true){
            // other.gameObject.GetComponent<Goal>().goal.enabled=false;
            // }
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