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
        if(Vector3.Distance(originalWorldPosition, transform.position) > maxWhipDistance)
        {
            ResetHat();
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

        }
    }
}
