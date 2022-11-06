using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        KeyItem,
        Collectable,
        Currency,
    }

    public ItemType itemType;
    public float Weight;
    public bool inInventory;
    private GameObject Player;

    void Start()
    {
        Player = GameObject.Find("Player");
    }
    
    void Update()
    {
        if (inInventory == true)
        {
            Player.GetComponent<Inventory>().StoreItem(this.gameObject);
        }
    }
}
