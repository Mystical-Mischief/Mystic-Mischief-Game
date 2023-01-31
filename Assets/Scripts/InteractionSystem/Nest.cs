using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Nest : MonoBehaviour
{
    public float treasure;

    private void Update()
    {
        treasure = Treasure.treasureValue;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="PickUp")
        {
            Item item = other.gameObject.GetComponent<Item>();
            float value = item.Weight;
            if(item.dropped)
            {
                item = null;
                Treasure.AddValue(value);
                Destroy(other.gameObject);
            }
        }
    }
}
