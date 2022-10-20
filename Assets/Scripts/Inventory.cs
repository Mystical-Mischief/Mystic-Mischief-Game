using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Inventory : MonoBehaviour
{

    private ThirdPersonInputs playerInputs;
    private InputAction move;
    ControlsforPlayer controls;
    private Rigidbody rb;
    private Item item;
    public List<GameObject> PickedUpItems = new List<GameObject>();
    public GameObject g;
    private int index;
    private GameObject Item;
    private Vector3 PlayerPosition;
    public int money;
    public int totalMoney;
    public GameObject Empty;
    private InputAction buttonAction;
    private bool Pickitem;
    private InputAction dropAction;
    private bool PickUp;
    private bool Store;
    private GameObject Socket;
    private Vector3 SocketLocation;
    private bool holdingItem;
    public bool triggered { get; }
    private int holdButton;
    private int tickets;
    public GameObject Gold;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        //buttonAction = controls.FindAction("Fire1");
        //buttonAction.Enable();
        controls = new ControlsforPlayer();

    }

        public void OnEnable()
    {
        //controls.Inv.Fire1.started += PickUp;
        controls.Enable();
        move = controls.Inv.@Fire1;
    }
 public void OnDisable() {
     controls.Disable();
    //controls.Inv.Fire1.started -= PickUp;
    }

    // Start is called before the first frame update
    void Start()
    {
        holdButton = 0;
        controls.Enable();
        PlayerPosition = new Vector3(0,1,0);
        index = PickedUpItems.Count;
        totalMoney = 0;
        buttonAction = new InputAction(
        type: InputActionType.Button,
        binding: "<KeyBoard>/1",
        interactions: "press(behavior=1)");
        buttonAction.Enable();
        Socket = this.gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {   
        Store = controls.Inv.Fire1.ReadValue<float>() > 0.1f;
        PickUp = controls.Inv.PressPick.ReadValue<float>() > 0.1f;
        if (controls.Inv.PressPick.ReadValue<float>() < 0.1f)
        {
            PickUp = false;
        }
        if (controls.Inv.Fire1.ReadValue<float>() < 0.1f)
        {
            Store = false;
        }
        if (holdingItem == true && Store)
        {
            g.SetActive(false);
            holdingItem = false;
        }
        //var move = moveAction.ReadValue<Vector2>(); 

        //Drop the Current Item
        bool Drop = controls.Inv.Drop.ReadValue<float>() > 0.1f;
        if (controls.Inv.Drop.triggered)
        {
            holdingItem = false;
            //Physics.gravity = Physics.gravity - g.GetComponent<Item>().Weight;
            g.transform.position = transform.position + PlayerPosition;
            g.SetActive(true);
            g.GetComponent<SphereCollider>().enabled = true;
            g.GetComponent<Item>().amount = 0;
            g = PickedUpItems[0];
            PickedUpItems.RemoveAt(PickedUpItems.Count - 1);
            money = money - g.GetComponent<Item>().amount;
        }
        if (controls.Inv.HoldItem.triggered)
        {
            holdingItem = true;
            g.GetComponent<MeshRenderer>().enabled = true;
        }

        if (controls.Inv.Drop.ReadValue<float>() < 0.1f)
        {

        }
        if (holdingItem == true)
        {
            g.transform.position = Socket.transform.position; 
        }
        
        if (g == null)
        {
            PickedUpItems.Remove(g);
        }
        if (PickedUpItems.Count == 0)
        {
            g = null;
        }
    }
            
    private void DropItems()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        
         if (other.gameObject.tag == "Gold" && Store && holdingItem == false)
        {
            g = other.gameObject;
            Debug.Log("Pickup");
            index += 1;
            PickedUpItems.Add(g);
            g.SetActive(false);
            if (g.GetComponent<Item>().isTradable == false)
            {
            money = money + g.GetComponent<Item>().amount;
            }
            if (g.GetComponent<Item>().isTradable == true)
            {
                tickets = tickets + g.GetComponent<Item>().amount;
            }
            //Physics.gravity = Physics.gravity + g.GetComponent<Item>().Weight;
        }
        if (other.gameObject.tag == "Gold" && PickUp && holdingItem == false)
        {
            g = other.gameObject;
            g.GetComponent<SphereCollider>().enabled = false;
            index += 1;
            PickedUpItems.Add(g);
            //money = money + g.GetComponent<Item>().amount;
            holdingItem = true;
        }
        if (other.gameObject.tag == "Goal")
        {
            totalMoney = totalMoney + money;
             for (var i = 0; i < PickedUpItems.Count; i++)
            {
                PickedUpItems.RemoveAt(i);
            }
            money = 0;
            g = null;
            for (var i = 0; i < tickets; i++)
            {
                float offset = i * 0.1f;
                Vector3 position = transform.position + Vector3.right * offset;
                var troop = Instantiate(Gold, position, transform.rotation);
            }
            totalMoney = totalMoney + tickets;
            tickets = 0; 

        }
    }
    //private void PickUp(InputAction.CallbackContext obj)
    //{

    //}
}
