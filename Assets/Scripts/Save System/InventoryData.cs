using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData {

    public List<GameObject> PickedUpItems;
    public bool InInventory;
    
    public InventoryData (Inventory inv)
    {
        PickedUpItems = inv.PickedUpItems;
    }
}
