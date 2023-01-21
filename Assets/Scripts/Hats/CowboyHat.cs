using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowboyHat : BaseHatScript
{
    [SerializeField]
    private float maxWhipDistance;
    [SerializeField]
    private float whipStrength;
    [SerializeField]
    private GameObject circleObject;
    private List<GameObject> allObjects = new List<GameObject>();
    [HideInInspector]
    public GameObject closestItem;
    GameObject nextClosestItem;
    bool findCloseItem = true;
    Vector3 originalLocalPosition;
    Vector3 originalWorldPosition;
    Rigidbody rb;
    new void Start()
    {
        GetComponent<SphereCollider>().enabled = false;
        base.Start();
        originalLocalPosition = transform.localPosition;
        rb = GetComponent<Rigidbody>();
        //adds all objects the player can use the whip on in a list for later
        foreach(GameObject gO in GameObject.FindGameObjectsWithTag("PickUp"))
        {
            print(gO.name);
            if (gO.GetComponent<Item>().itemType == Item.ItemType.Collectable)
            {

                allObjects.Add(gO);
            }
        }
    }
    //enables and disables the circle object tool when the object is activated or disabled
    new void OnEnable()
    {
        base.OnEnable();
        circleObject.SetActive(true);
    }
    void OnDisable()
    {
        circleObject.SetActive(false);
    }
    new void Update()
    {
        base.Update();
        //checks to see if the hat has moved too far for the whip distance. if it did reset the hat
        if (Vector3.Distance(originalWorldPosition, transform.position) > maxWhipDistance)
        {
            ResetHat();
        }
        //finds the closest item to whip that the hat can whip
        if (findCloseItem)
        {
            foreach (GameObject gO in allObjects)
            {
                if (!gO.activeInHierarchy)
                {
                    continue;
                }
                if (gO.GetComponent<Item>().inInventory)
                {
                    continue;
                }
                if (closestItem == null)
                {
                    closestItem = gO;
                    continue;
                }
                if (Vector3.Distance(gO.transform.position, transform.position) < Vector3.Distance(closestItem.transform.position, transform.position))
                {
                    closestItem = gO;
                }
            }
            findCloseItem = false;
        }
        //after finding the closest item it will find the next closest item.
        if (!findCloseItem)
        {
            detectNextClosestItem();
        }
        //if the closest item has been grabbed or stored in the inventory clear the closest item
        if (closestItem != null && (closestItem.GetComponent<Item>().inInventory || !closestItem.activeInHierarchy))
        {
            findCloseItem = true;
            closestItem = null;
        }
        //if there is a closest item make the whip face the closest item
        if (closestItem != null)
        {
            transform.forward = closestItem.transform.position - transform.position;
        }
        
    }
    //finds the 2nd closest item out of the list and updates it while the player moves around
    void detectNextClosestItem()
    {
        foreach (GameObject gO in allObjects)
        {
            if (!gO.activeInHierarchy)
            {
                continue;
            }
            if (gO.GetComponent<Item>().inInventory)
            {
                continue;
            }
            if (nextClosestItem == closestItem)
            {
                nextClosestItem = null;
            }
            if (nextClosestItem == null)
            {
                nextClosestItem = gO;
                continue;
            }
            if (gO == closestItem)
            {
                continue;
            }
            if (Vector3.Distance(gO.transform.position, transform.position) < Vector3.Distance(nextClosestItem.transform.position, transform.position))
            {
                nextClosestItem = gO;
            }
        }
        if (Vector3.Distance(nextClosestItem.transform.position, transform.position) < Vector3.Distance(closestItem.transform.position, transform.position))
        {
            findCloseItem = true;
        }
    }
    //makes whip able to move and moves it forward based off whip strength
    public override void HatAbility()
    {
        GetComponent<SphereCollider>().enabled = true;
        originalWorldPosition = transform.position;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * whipStrength, ForceMode.Impulse);
        base.HatAbility();
    }
    //sets it to where it cant move and moves it back to the original position
    void ResetHat()
    {
        GetComponent<SphereCollider>().enabled = false;
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        transform.localPosition = originalLocalPosition;
    }
    //trigger logic for when it hits an object to pick up the item
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PickUp" && !GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().holdingItem)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().HoldItem(other.gameObject);
            ResetHat();
        }
    }

    public float MaxWhipDis()
    {
        return maxWhipDistance;
    }
}
