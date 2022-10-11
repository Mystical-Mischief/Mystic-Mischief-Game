using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Gold,
        Shoe,
    }

    public ItemType itemType;
    public int amount;
}
