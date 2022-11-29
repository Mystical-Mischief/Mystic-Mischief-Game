using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Inventory inv;
    public List<GameObject> PickedUpItems = new List<GameObject>();
    public int invNumber;
    public GameObject inventoryItem;
    public Image ObjectwithImage;
    public Color newColor;
    public Text nameText;
    private Color noColor;
    public bool inInventory;
    public Transform inventoryTransform;
    public GameObject Empty;
    // Start is called before the first frame update
    void Start()
    {
        EmptyInventorySlot();
    }

    // Update is called once per frame
    void Update()
    {
     PickedUpItems = inv.PickedUpItems;
     inventoryItem = PickedUpItems[invNumber];
     inventoryTransform = inventoryItem.transform;
          if (inventoryItem.GetComponent<Item>().inInventory == true)
     {
        // Debug.Log("InInv");
     }
     if (inventoryItem.GetComponent<Item>().inInventory == false)
     {
        Debug.Log("Dropped");
        inventoryTransform = Empty.transform;
     }
    if (inventoryItem != PickedUpItems[invNumber])
    {
        Debug.Log("Dropped");
        inventoryItem = Empty;
    }
    // Debug.Log(inventoryItem);
    // ChangeSprite();
    // nameText.text = (inventoryItem.GetComponent<Item>().ItemType.itemtype);

    if (PickedUpItems[invNumber] != null)
    {
        inInventory = true;
        ChangeSprite();
    }
    else {inInventory = false; EmptyInventorySlot();}
    // if(PickedUpItems == null)
    // {
    //     EmptyInventorySlot();
    // }
    }

    public void EmptyInventorySlot()
    {
        newColor.a = 0f;
        ObjectwithImage.color = newColor;
        nameText.text = ("");
    }

    public void ChangeSprite()
    {
        ObjectwithImage.sprite = inventoryItem.GetComponent<Item>().sprite;
        newColor = inventoryItem.GetComponent<Item>().spriteColor;
        newColor.a = 255f;
        ObjectwithImage.color = newColor;
        nameText.text = (inventoryItem.name);
    }
}
