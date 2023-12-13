using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingRat : BaseEnemyAI
{

    public int randomNumber;
    public GameObject Item;
    public GameObject Saves;
    public List<GameObject> StolenItems = new List<GameObject>();
    public bool Attacked;

    private GameObject heldItem;
    public bool isPeaceful;
    public bool Walking;
    public Animator anim;

    private Transform tempTarget;

    // Start is called before the first frame update
    new void Start()
    {
        //Calls the start from BaseEnemyAi
        base.Start();
        anim = GetComponentInChildren<Animator>();
    }
    void OnEnable()
    {
        Walking = true;
    }

    // Update is called once per frame
    new void Update()
    {


        base.Update();

        if (!this.gameObject.GetComponent<Dialogue>().enabled)
        {
            Patrol();
        }
        else
        {
            target = ai_Rb.transform;
            tempTarget = target;
            UpdateDestination(tempTarget.position);
        }
        //If it took an item it escapes
        // if (Attacked == true && !isPeaceful)
        // {
        //    //Escape = target;
        //    //UpdateDestination(Escape.position);
        // }
        // else
        // {
        //     
        // }
        if (Walking == true)
        {
            anim.SetBool("Idle", false);
        }
        if (Walking == false)
        {
            anim.SetBool("Idle", true);
        }

        //This calls the update function from base.

    }

    public override void FoundPlayer()
    {
        
    }
    public override void LostPlayer()
    {

    }

    public override void EnemyDetection()
    {

    }
    //  private void OnCollisionEnter(Collision other)
    //  {
    //      //If it collides with the player and it hasnt yet it will steal an item.
    //      if (other.gameObject.tag == "Player" && Attacked == false && !isPeaceful)
    //      {
    //          if(heldItem == null) 
    //          {
    //              heldItem = Player.GetComponent<Inventory>().currentHeldItem;
    //              Player.GetComponent<Inventory>().DropItem(Player.GetComponent<Inventory>().currentHeldItem);
    //              heldItem.transform.parent = this.gameObject.transform;
    //              heldItem.transform.localPosition = Vector3.zero;
    //              heldItem.GetComponent<Rigidbody>().isKinematic = true;
    //          }
    //
    //          if (heldItem != null)
    //          {
    //              Attacked = true;
    //          }
    //      }
    //  }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Escape"))
    //     {
    //         Attacked = false;
    //         //LostPlayer();
    //         //target = PatrolPoints[0];
    //         Patrol();
    //
    //     }
    //
    // }

    // [SerializeField]
    // private GameObject[] Positions;
    // [SerializeField]
    // private float _speed = 5;
    // private Transform _target;
    // private int _index;
    //
    // // Start is called before the first frame update
    // void Start()
    // {
    //     _index = 0;
    //     _target = Positions[_index].transform;
    //     
    // }
    //
    // // Update is called once per frame
    // void Update()
    // {
    //     
    //     _target = Positions[_index].transform;
    //     transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime); //makes the key towards a specific location
    //     if (Vector3.Distance(transform.position, _target.position) < 0.5) //if the key is close to the targeted location
    //     {
    //         _index++; //increase index
    //         if (_index == Positions.Length) // this helps reset the key flying positions
    //         {
    //             _index = 0;
    //         }
    //     }
    //     
    // }

}
