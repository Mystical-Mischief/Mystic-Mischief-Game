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
    public float fallDistance = 1f;
    public bool canKill;

    [SerializeField] private GameObject itemEffect;

    private static InvisibilityHat _invisibilityHat;

    public bool Invisible;
    // ControlsforPlayer controls;

    void Start()
    {
        Player = GameObject.Find("Player");
        dropped = false;
        rb = GetComponent<Rigidbody>();
        // controls = new ControlsforPlayer();
        // controls.Enable();
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
        if (other.gameObject.name == "Interactions")
        {
            if (itemType == ItemType.Objective)
            {
                other.gameObject.GetComponent<BirdInteraction>().ticketCount = other.gameObject.GetComponent<BirdInteraction>().ticketCount + value;
                value = 0;
                // other.gameObject.GetComponent<BirdInteraction>().ticketCount = other.gameObject.GetComponent<BirdInteraction>().ticketCount - 1;
                other.gameObject.GetComponent<BirdInteraction>().StoredItems.Add(this.gameObject);
            }
        }

        if (other.gameObject.tag == "PlayerOnly" && rb.velocity.magnitude >= fallDistance && dropped == true)
        {
            if (canKill == false)
            {
            other.gameObject.GetComponentInParent<BaseEnemyAI>().stunned = true;
            }
            if (canKill == true)
            {
                other.gameObject.GetComponentInParent<BaseEnemyAI>().Die = true;
                Destroy(other.gameObject.transform.parent.gameObject, 1f);
            }
        }
    }
    public void OnCollisionEnter(Collision other)
    {

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
    public void Dropped()
    {
        dropped = true;
        Invoke(nameof(ResetDropped), 2f);
    }
    public void ResetDropped()
    {
        dropped = false;
    }
}
