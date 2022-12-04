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
    public Sprite sprite;
    public Color spriteColor;
    public bool canDrop;

    [SerializeField] private GameObject itemEffect;

    void Start()
    {
        Player = GameObject.Find("Player");
    }
    
    public void SaveItem()
    {
        SaveSystem.SaveItem(this);
    }
    public void LoadItem()
    {
        ItemData data = SaveSystem.LoadItem(this);

        inInventory = data.InInventory;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }

    public void PlayEffect()
    {
        if(itemEffect != null)
        {
           
            if(itemType == ItemType.Collectable)
            {
                //Instantiate(itemEffect, this.transform.position, Quaternion.identity) as GameObject;
            }
        }
    }
}
