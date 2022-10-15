using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Gold,
        Shoe,
        Ticket,
    }

    public ItemType itemType;
    public int amount;
    public Vector3 Weight;
    public bool isKey;
    public bool isTradable;
}
