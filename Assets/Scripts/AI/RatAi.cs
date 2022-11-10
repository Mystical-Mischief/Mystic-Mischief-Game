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
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(base.Player.transform.position, transform.position);
        if (dist < 10 && Attacked == false)
        {
            base.target = base.Player.transform;
            base.UpdateDestination(base.target.position);
        }
        if (Attacked == true)
        {
            base.target = Escape;
            base.UpdateDestination(base.target.position);
        }
        base.Update();
    }
    private void OnCollisionEnter(Collision other)
    {
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
        if (other.gameObject.tag == "Escape")
        {
            Attacked = false;
            Player.GetComponent<ThirdPersonController>().LoadCheckpoint();
            Saves.GetComponent<SaveGeneral>().LoadEnemy();
        }
    }

    public virtual void RandomNumber()
    {
        randomNumber = Random.Range(0, base.Player.GetComponent<Inventory>().PickedUpItems.Count);
        if (randomNumber == lastWaitTime)
        {
            randomNumber = Random.Range(1, 3);
        }
        lastWaitTime = randomNumber;
    }
}
