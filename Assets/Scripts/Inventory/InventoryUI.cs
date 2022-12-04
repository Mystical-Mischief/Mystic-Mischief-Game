using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public List<GameObject> InventorySlots = new List<GameObject>();
    public GameObject InventorySlot;
    public List<GameObject> PickedUpItems = new List<GameObject>();
    public Inventory inv;
    private int InvNumber;

    // Update is called once per frame
    void Update()
    {
        PickedUpItems = inv.PickedUpItems;
        if (PickedUpItems != null)
        {
        InventorySlot = InventorySlots[PickedUpItems.Count - 1];
        }
    }
    public void DropLastItemUI()
    {
        InventorySlot.GetComponent<InventorySlot>().EmptyInventorySlot();
    }
}
