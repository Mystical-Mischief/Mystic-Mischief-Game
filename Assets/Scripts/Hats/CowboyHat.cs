using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowboyHat : BaseHatScript
{
    [SerializeField]
    private float maxWhipDistance;
    [SerializeField]
    private float whipStrength;
    private List<GameObject> allObjects = new List<GameObject>();
    GameObject closestItem;
    GameObject nextClosestItem;
    bool findCloseItem = true;
    Vector3 originalLocalPosition;
    Vector3 originalWorldPosition;
    Rigidbody rb;
    new void Start()
    {
        base.Start();
        originalLocalPosition = transform.localPosition;
        rb = GetComponent<Rigidbody>();
        foreach(GameObject gO in GameObject.FindGameObjectsWithTag("PickUp"))
        {
            if(gO.GetComponent<Item>().itemType == Item.ItemType.Collectable)
            {
                print(gO.name);
                allObjects.Add(gO);
            }
        }
    }
    new void Update()
    {
        base.Update();
        
        if (Vector3.Distance(originalWorldPosition, transform.position) > maxWhipDistance)
        {
            ResetHat();
        }
        if (findCloseItem)
        {
            foreach (GameObject gO in allObjects)
            {
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
        if (!findCloseItem)
        {
            detectNextClosestItem();
        }
        if (closestItem.GetComponent<Item>().inInventory)
        {
            findCloseItem = true;
            closestItem = null;
        }
        transform.forward = closestItem.transform.position - transform.position;
    }
    void detectNextClosestItem()
    {
        foreach (GameObject gO in allObjects)
        {
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
    public override void HatAbility()
    {
        originalWorldPosition = transform.position;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * whipStrength, ForceMode.Impulse);

        base.HatAbility();
    }
    void ResetHat()
    {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        transform.localPosition = originalLocalPosition;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PickUp")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().HoldItem(other.gameObject);
            ResetHat();
        }
    }
}
