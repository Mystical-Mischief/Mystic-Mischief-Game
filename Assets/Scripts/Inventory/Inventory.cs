using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using System;


public class Inventory : MonoBehaviour
{
    public float MassText;
    public float weightFloat;
    [HideInInspector]
    public ControlsforPlayer controls;

    [HideInInspector]
    public Rigidbody rb;

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
    
    public GameObject InventoryUI;
    public GameObject InventoryImages;
    
    public float HoldTime;
    private float Holding;

    [SerializeField]
    private GameObject _itemThrowPos;

    private LineRenderer _trajectoryLine;
    private DrawPotionProjection _trajectory;
    private Transform _trajectoryTransform;

    private float _heldItemWeight;
    

    private float _itemSpeed;
    public IntegerControl tapCount { get; set; }
   
    void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
        controls = new ControlsforPlayer();
        startMass = rb.mass;

        _trajectory = _itemThrowPos.GetComponent<DrawPotionProjection>();
        _trajectoryTransform = _itemThrowPos.transform;
        _trajectoryLine = _itemThrowPos.GetComponent<LineRenderer>();
        _trajectoryLine.enabled = false;

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
        controls = new ControlsforPlayer();
        controls.Enable();
    }

    void Update()
    {
        if(currentHeldItem != null)
        {
            
            _heldItemWeight = currentHeldItem.GetComponent<Item>().Weight;
            _itemSpeed = rb.velocity.magnitude * 0.7f;
            if (_itemSpeed > 0.1)
            {
                _trajectory.ShowTrajectoryLine(_trajectoryTransform.position, (_trajectoryTransform.forward).normalized * _itemSpeed);
                _trajectoryLine.enabled = true;
            }
            else
            {
                _trajectoryLine.enabled = false;
            }
        }
        else
        {
            _trajectoryLine.enabled = false;
        }
        
        weightFloat = rb.mass;
        bool Load = controls.MenuActions.Load.ReadValue<float>() > 0.1f;
        bool Save = controls.MenuActions.Save.ReadValue<float>() > 0.1f;
        
        TicketAmount = PickedUpItems.Count;
        Store = controls.Inv.Store.IsPressed();
        PickUp = controls.Inv.PressPick.IsPressed();
       
        //Drop the Current Item
        
        bool Drop = controls.Inv.Drop.ReadValue<float>() > 1f;
        

        if (holdingItem && PickUp && controls.Inv.PressPick.WasPressedThisFrame())
        {
            DropItem(currentHeldItem);
        }
        
    }
    private void DoDrop(InputAction.CallbackContext obj)
    {
        Holding = obj.ReadValue<float>();
    }

    public void StoreItem(GameObject item)
    {
        Debug.Log("Item Stored");

        item.SetActive(false);
        holdingItem = false;
        
        PickedUpItems.Add(item);
        
        MassText = MassText + item.GetComponent<Item>().Weight;

    }

    public void StoreObjective(GameObject item)
    {
        Debug.Log("Item Stored");
        item.SetActive(false);
        holdingItem = false;
        
        Objective.Add(item);
        rb.mass = rb.mass + (item.GetComponent<Item>().Weight * 0.2f);
        MassText = MassText + item.GetComponent<Item>().Weight;

    }

    private GameObject Whip;
    public void HoldItem(GameObject Item)
    {
        if (Item.GetComponent<Item>().ps != null)
        {
            Item.GetComponent<Item>().ps.Stop();
        }
        if (hat2.activeInHierarchy)
        {
        Whip = GameObject.FindGameObjectWithTag("Whip");
        Whip.GetComponent<CowboyHat>().allObjects.Remove(Item);
        Whip.GetComponent<CowboyHat>().findCloseItem = true;
        Whip.GetComponent<CowboyHat>().closestItem = null;
        }
        if(Item.GetComponent<Item>().itemType == global::Item.ItemType.WhipOnly)
        {
            Item.GetComponent<Item>().itemType = global::Item.ItemType.Collectable;
        }
        Item.GetComponent<SphereCollider>().enabled = false;
        Item.GetComponent<BoxCollider>().enabled = false;
        Item.GetComponent<Item>().inInventory = true;
        MassText = MassText + Item.GetComponent<Item>().Weight;
        rb.mass = rb.mass + (Item.GetComponent<Item>().Weight * 0.2f);
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
        if (Item.GetComponent<Item>().ps != null)
        {
            Item.GetComponent<Item>().ps.Play();
        }
        
        Item.GetComponent<SphereCollider>().enabled = true;
        Item.GetComponent<BoxCollider>().enabled = true;
        if (hat2.activeInHierarchy)
        {
        Whip.GetComponent<CowboyHat>().allObjects.Add(Item);
        Whip.GetComponent<CowboyHat>().findCloseItem = true;
        Whip.GetComponent<CowboyHat>().closestItem = null;
        }
        Item.GetComponent<Item>().inInventory = false;
        Item.GetComponent<Item>().Dropped();
       
        MassText = MassText - Item.GetComponent<Item>().Weight;
        rb.mass = rb.mass - (Item.GetComponent<Item>().Weight * 0.2f);
        Item.transform.parent = null;
        Item.GetComponent<Rigidbody>().isKinematic = false;
        Item.GetComponent<Rigidbody>().useGravity = true;
        currentHeldItem = null;
        heldItem.dropped = true;
        StartCoroutine(dropTimer(0.5f, false));
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
            if(other.gameObject.GetComponent<Item>().itemType != Item.ItemType.WhipOnly)
            {
                HoldItem(other.gameObject);
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