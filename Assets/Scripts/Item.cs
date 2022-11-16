using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Objective,
        Collectable,
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
    public void SaveItem()
    {
        SaveSystem.SaveItem(this);
    }
    public void LoadItem()
    {
        ItemData data = SaveSystem.LoadItem(this);

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
}
