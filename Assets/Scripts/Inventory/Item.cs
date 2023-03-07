using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Objective,
        Collectable,
        WhipOnly
    }

    public ItemType itemType;
    public float Weight;
    public bool inInventory;
    private GameObject Player;
    public Sprite sprite;
    public Color spriteColor;
    public bool canDrop;
    public bool dropped;
    private int value = 1;
    public ParticleSystem ps;
    private Rigidbody rb;

    [SerializeField] private GameObject itemEffect;

    private static InvisibilityHat _invisibilityHat;

    public bool Invisible;

    void Start()
    {
        Player = GameObject.Find("Player");
        dropped = false;
        rb = GetComponent<Rigidbody>();
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


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Clever Bird")
        {
            if (itemType == ItemType.Objective)
            {
                other.gameObject.GetComponent<BirdInteraction>().ticketCount = other.gameObject.GetComponent<BirdInteraction>().ticketCount + value;
                value = 0;
                // other.gameObject.GetComponent<BirdInteraction>().ticketCount = other.gameObject.GetComponent<BirdInteraction>().ticketCount - 1;
                other.gameObject.GetComponent<BirdInteraction>().StoredItems.Add(this.gameObject);
            }
        }
    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "enemy" && rb.velocity.magnitude >= 5)
        {
            other.gameObject.GetComponent<BaseEnemyAI>().stunned = true;
        }
    }

    public static void FindInvisibilityHat(InvisibilityHat hat)
    {
        _invisibilityHat = hat;
    }

    private void Update()
    {
        if (_invisibilityHat != null && _invisibilityHat.IsInvisible())
        {
            Invisible = true;
        }
        else
        {
            Invisible = false;
        }
    }
}
