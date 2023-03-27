using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAi : BaseEnemyAI
{
    public int randomNumber;
    int lastWaitTime;
    public GameObject Item;
    public GameObject Saves;
    public List<GameObject> StolenItems = new List<GameObject>();
    public bool Attacked;
    public Transform Escape;
    private GameObject heldItem;

    // Start is called before the first frame update
    new void Start()
    {
        //Calls the start from BaseEnemyAi
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        float dist = Vector3.Distance(base.Player.transform.position, transform.position);
        //If the player is close enough it chases the player
        if (dist < 10 && Attacked == false)
        {
            target = Player.transform;
            UpdateDestination(target.position);
        }
        //If it took an item it escapes
        if (Attacked == true)
        {
            target = Escape;
            UpdateDestination(target.position);
        }
        else
        {
            Patrol();
        }
        //This calls the update function from base.
        base.Update();
    }
    private void OnCollisionEnter(Collision other)
    {
        //If it collides with the player and it hasnt yet it will steal an item.
        if (other.gameObject.tag == "Player" && Attacked == false)
        {
            heldItem = Player.GetComponent<Inventory>().currentHeldItem;
            Player.GetComponent<Inventory>().DropItem(Player.GetComponent<Inventory>().currentHeldItem);
            heldItem.transform.parent = this.gameObject.transform;
            heldItem.transform.localPosition = Vector3.zero;
            heldItem.GetComponent<Rigidbody>().isKinematic = true;
            if (heldItem != null)
            {
                Attacked = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Escape"))
        {
            Attacked = false;
            LostPlayer();
            target = PatrolPoints[0];

        }
    }
}
