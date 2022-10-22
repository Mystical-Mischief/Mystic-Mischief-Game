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
    
}
