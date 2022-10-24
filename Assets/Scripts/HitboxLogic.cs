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
        if(other.gameObject.tag == "PickUp" && !inventory.holdingItem)
        {
            snatchItem(other.gameObject);
        }
        if(other.gameObject.tag == "Enemy")
        {
            GameObject enemy = other.gameObject;
            //stun the enemy
            if(enemy.GetComponent<BaseEnemyAI>().EnemyType == "ItemHolder")
            {
                if (enemy.GetComponent<ItemHolderAI>().HoldingItem)
                {
                    snatchItem(enemy.GetComponent<ItemHolderAI>().CurrentHeldItem);
                }
            }
        }
    }
    private void snatchItem(GameObject item)
    {
        inventory.HoldItem(item);
    }
}
