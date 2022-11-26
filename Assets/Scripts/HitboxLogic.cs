using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxLogic : MonoBehaviour
{
    Inventory inventory;
    private void Start()
    {
        inventory = GetComponentInParent<Inventory>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //if you snatch an enemy check to see if it has an item it can snatch (needs to be updated with new kobold AI)
        if(other.gameObject.tag == "Enemy")
        {
            GameObject enemy = other.gameObject;
            //stun the enemy
            //if the enemy is an item holder snatch the item the ai is holding
            if(enemy.GetComponent<BaseEnemyAI>().EnemyType == "ItemHolder")
            {
                if (enemy.GetComponent<ItemHolderAI>().HoldingItem)
                {
                    snatchItem(enemy.GetComponent<ItemHolderAI>().CurrentHeldItem);
                }
            }
        }
    }
    //run the hold item function in the inventory script
    private void snatchItem(GameObject item)
    {
        inventory.HoldItem(item);
    }
}
