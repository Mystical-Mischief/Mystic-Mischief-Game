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

    // Start is called before the first frame update
    void Start()
    {
        //Calls the start from BaseEnemyAi
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(base.Player.transform.position, transform.position);
        //If the player is close enough it chases the player
        if (dist < 10 && Attacked == false)
        {
            base.target = base.Player.transform;
            base.UpdateDestination(base.target.position);
        }
        //If it took an item it escapes
        if (Attacked == true)
        {
            base.target = Escape;
            base.UpdateDestination(base.target.position);
        }
        //This calls the update function from base.
        base.Update();
    }
    private void OnCollisionEnter(Collision other)
    {
        //If it collides with the player and it hasnt yet it will steal an item.
        if (other.gameObject.tag == "Player" && Attacked == false)
        {
            Item = base.Player.GetComponent<Inventory>().PickedUpItems[randomNumber];
            StolenItems.Add(Item);
            base.Player.GetComponent<Inventory>().PickedUpItems.Remove(Item);
            if (Item != null)
            {
                Attacked = true;
            }
        }
        //If it stole an item from the player it tries to escape and if it reaches the escape gameobject it reloads a checkpoint.
        if (other.gameObject.tag == "Escape")
        {
            Attacked = false;
            Player.GetComponent<ThirdPersonController>().LoadCheckpoint();
            Saves.GetComponent<SaveGeneral>().LoadCheckpoint();
        }
    }

    //This will decide which object to steal.
    public virtual void RandomNumber()
    {
        randomNumber = Random.Range(0, base.Player.GetComponent<Inventory>().PickedUpItems.Count);
        if (randomNumber == lastWaitTime)
        {
            randomNumber = Random.Range(0, base.Player.GetComponent<Inventory>().PickedUpItems.Count);
        }
        lastWaitTime = randomNumber;
    }
}
