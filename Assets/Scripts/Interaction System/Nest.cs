using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Nest : MonoBehaviour
{
    public float treasure;
   

    [SerializeField] ParticleSystem storeVFX;
    Item item;
    bool CanDropItem;

    private void Start()
    {
       storeVFX.Stop();
       
        CanDropItem = false;
    }


    private void Update()
    {
        treasure = Treasure.treasureValue;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="PickUp")
        {
            item = other.gameObject.GetComponent<Item>();
            //if(item.dropped)
            // {

            //storeVFX.Play();
            CanDropItem = true;
            dropItem();
                //item = null;
                //Loot.AddToLoot(value);
                Destroy(other.gameObject);
            //}
        }
    }

    private void dropItem()
    {
        
        if(CanDropItem)
        {
            float value = item.Weight;
            storeVFX.Play();
            item = null;
            CanDropItem = false;
            Treasure.AddValue(value);
            //CanDropItem=false;
            
        }
        
    }

}
