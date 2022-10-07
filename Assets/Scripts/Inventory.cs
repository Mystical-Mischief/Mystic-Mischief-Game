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
        controls.Enable();
        PlayerPosition = new Vector3(0,1,0);
        index = 0;
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
        //var move = moveAction.ReadValue<Vector2>(); 
        bool Drop = controls.Inv.Drop.ReadValue<float>() > 0.1f;
        if (Drop)
        {
            Debug.Log("Actiyhgf");
            g.transform.position = transform.position + PlayerPosition;
            g.SetActive(true);
            g.GetComponent<Item>().amount = 0;
            PickedUpItems.Remove(g);
            g = PickedUpItems[0];
            money = money - g.GetComponent<Item>().amount;
            if (g = PickedUpItems[0])
            {
            g.transform.position = transform.position + PlayerPosition;
            g.SetActive(true);
            g.GetComponent<Item>().amount = 0;
            PickedUpItems.Remove(g);
            g = null;
            money = money - g.GetComponent<Item>().amount;
                g = null;
            }

        }
        g.transform.position = Socket.transform.position; 
        
        if (g == null)
        {
            PickedUpItems.Remove(g);
        }
    }
            
    
    private void OnTriggerStay(Collider other)
    {
        
         if (other.gameObject.tag == "Gold" && Store)
        {
            g = other.gameObject;
            Debug.Log("Pickup");
            index += 1;
            PickedUpItems.Add(g);
            g.SetActive(false);
            money = money + g.GetComponent<Item>().amount;
        }
        else if (other.gameObject.tag == "Gold" && PickUp)
        {
            g = other.gameObject;
            index += 1;
            PickedUpItems.Add(g);
            money = money + g.GetComponent<Item>().amount;
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
        }
    }
    //private void PickUp(InputAction.CallbackContext obj)
    //{

    //}
}
