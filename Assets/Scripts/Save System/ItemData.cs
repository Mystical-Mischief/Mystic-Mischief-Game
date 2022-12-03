using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public float[] position;
    public bool Active;
    public bool InInventory;

    public ItemData(Item item)
    {
        Active = item.gameObject.activeSelf;

        InInventory = item.inInventory;
        position = new float[3];
        position[0] = item.transform.position.x;
        position[1] = item.transform.position.y;
        position[2] = item.transform.position.z;
    }
}
