using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public GameObject[] itemsToCollect;
    public int numOfItems;
    private void Start()
    {
        GetComponent<Chest>().dissapear = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "PickUp" && other.GetComponent<Item>().itemType == Item.ItemType.Objective)
        {
            foreach(GameObject go in itemsToCollect)
            {
                if(go == other.gameObject && FindObjectOfType<CollectionQuest>().enabled)
                {
                    FindObjectOfType<CollectionQuest>().CollectedItem(other.gameObject);
                    numOfItems++;
                }
            }
        }
        if(numOfItems == itemsToCollect.Length)
        {
            GetComponent<Chest>().dissapear = true;
        }
    }
}
