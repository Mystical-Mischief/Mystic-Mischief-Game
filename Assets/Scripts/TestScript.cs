using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestScript : MonoBehaviour
{

    private ThirdPersonInputs playerInputs;
    private InputAction move;

    private Item item;
    public List<GameObject> PickedUpItems = new List<GameObject>();
    public GameObject g;
    private int index;
    private GameObject Item;
    private Vector3 PlayerPosition;
    public int money;
    public int totalMoney;
    public GameObject Empty;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPosition = new Vector3(0,1,0);
        index = 0;
        totalMoney = 0;
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.E))
        {
            g.transform.position = transform.position + PlayerPosition;
            g.SetActive(true);
            g.GetComponent<Item>().amount = 0;
            PickedUpItems.Remove(g);
            g = PickedUpItems[0];
            money = money - g.GetComponent<Item>().amount;

        }
        if (g != null)
        {

        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.tag == "Gold" && Input.GetKey(KeyCode.G))
        {
            index += 1;
            g = other.gameObject;
            PickedUpItems.Add(g);
            g.SetActive(false);
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
            g = Empty;
        }
    }
}
