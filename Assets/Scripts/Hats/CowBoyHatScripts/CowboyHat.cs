using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowboyHat : BaseHatScript
{
    [SerializeField]
    private float maxWhipDistance;
    private float _increasedWhipDistance;
    public bool canShoot;
    [SerializeField]
    private float whipStrength;
    [SerializeField]
    private GameObject circleObject;
    [HideInInspector]
    public List<GameObject> allObjects = new List<GameObject>();
    public GameObject closestItem;
    GameObject nextClosestItem;
    [HideInInspector]
    public bool findCloseItem = true;
    Vector3 originalLocalPosition;
    Vector3 originalWorldPosition;
    Rigidbody rb;
    public bool isGrounded;
    public float l1HatUseTime;
    private GameObject Player;
    public bool gunSlinger;
    public bool pullEnemy;
    public Transform HoldItemPosition;
    private Item heldItem;
    public bool holdingItem;
    public GameObject currentHeldItem;
    Inventory playerInv;
    public Rigidbody bullet;
    public float projectileSpeed;
    public float speed;

    new void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerInv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        GetComponent<SphereCollider>().enabled = false;
        base.Start();
        originalLocalPosition = transform.localPosition;
        rb = GetComponent<Rigidbody>();
        //adds all objects the player can use the whip on in a list for later
        _increasedWhipDistance = maxWhipDistance * 2;

    }
    //enables and disables the circle object tool when the object is activated or disabled
    new void OnEnable()
    {
        base.OnEnable();
        foreach (GameObject gO in GameObject.FindGameObjectsWithTag("PickUp"))
        {
            if (gO.GetComponent<Item>() && (gO.GetComponent<Item>().itemType == Item.ItemType.Collectable || gO.GetComponent<Item>().itemType == Item.ItemType.WhipOnly))
            {
                allObjects.Add(gO);
            }
            if (gO.GetComponent<WhippableObject>())
            {
                allObjects.Add(gO);
            }
        }
        if (pullEnemy == true)
        {
            foreach (GameObject gO in GameObject.FindGameObjectsWithTag("enemy"))
        {
                allObjects.Add(gO);
        }
        }

        
    }
    void OnDisable()
    {
        circleObject.SetActive(false);
        allObjects.Clear();
    }
    new void Update()
    {

        if (pullEnemy == true)
        {
            foreach (GameObject gO in GameObject.FindGameObjectsWithTag("enemy"))
            {
                allObjects.Add(gO);
            }
        }
        // foreach (GameObject gO in GameObject.FindGameObjectsWithTag("PickUp"))
        // {
        //     if (gO.GetComponent<Item>().inInventory == true)
        //     {
        //         allObjects.Remove(gO);
        //     }
        // }
        if (SkillLevel <= 1 && isGrounded == false)
        {
            canUseHat = false;
        }
        if (SkillLevel <= 1 && isGrounded == true)
        {
            canUseHat = true;
        }
        if (Player.GetComponent<PlayerController>().onGround == true)
        {
            isGrounded = true;
        }
        if (Player.GetComponent<PlayerController>().onGround == false)
        {
            isGrounded = false;
        }

        //if you can use the hat use the hat and start the cooldown
        if (controls.Actions.ActivateHat.IsPressed() && canUseHat && gunSlinger == false)
        {
            Debug.Log("CowBoy");
            canUseHat = false;
            if (SkillLevel <= 1 || pullEnemy == true)
            {
                Invoke(nameof(HatAbility), l1HatUseTime);
            }
            if (SkillLevel >= 2 && holdingItem == false && pullEnemy == false)
            {
                HatAbility();
            }
            if (SkillLevel >= 2 && holdingItem == true)
            {
                DropItem(currentHeldItem);
                holdingItem = false;
            }
            // if (SkillLevel == 3 && Player.GetComponent<Inventory>().holdingItem == false)
            // {
            //     HatAbility();
            // }
            // if (SkillLevel == 3 && Player.GetComponent<Inventory>().holdingItem == true)
            // {
            //     HatAbilityHoldingItem();
            // }
            // if (SkillLevel == 4)
            // {
            //     HatAbility();
            // }
        }
        if (gunSlinger == true && controls.Actions.CowBoyHatUse.WasPerformedThisFrame() && playerInv.holdingItem == false)
        {
            HatAbility();
        }
        if (controls.Actions.CowBoyHatShot.WasPerformedThisFrame() && gunSlinger == true && canShoot == true)
        {
            Shoot();
        }
        if (SkillLevel > 1)
        {
            maxWhipDistance = _increasedWhipDistance; // increase max whip distance when the level in skill tree is greater than 1
        }
        //checks to see if the hat has moved too far for the whip distance. if it did reset the hat
        if (Vector3.Distance(originalWorldPosition, transform.position) > maxWhipDistance)
        {
            ResetHat();
        }
        //finds the closest item to whip that the hat can whip
        if (findCloseItem)
        {
            foreach (GameObject gO in allObjects)
            {
                if (!gO.activeInHierarchy)
                {
                    continue;
                }
                if (closestItem == null)
                {
                    closestItem = gO;
                    continue;
                }
                if (Vector3.Distance(gO.transform.position, transform.position) < Vector3.Distance(closestItem.transform.position, transform.position))
                {
                    closestItem = gO;
                }
            }
            if (closestItem != null)
            {
                findCloseItem = false;
            }
        }
        circleObjectLogic();

        
        //after finding the closest item it will find the next closest item.
        if (!findCloseItem)
        {
            detectNextClosestItem();
        }
        //if the closest item has been grabbed or stored in the inventory clear the closest item
        if (closestItem != null && !closestItem.activeInHierarchy)
        {
            findCloseItem = true;
            closestItem = null;
        }
        //if there is a closest item make the whip face the closest item
        if (closestItem != null)
        {
            transform.forward = closestItem.transform.position - transform.position;
        }
        base.Update();
    }
    void circleObjectLogic()
    {
        if(Vector3.Distance(closestItem.transform.position, transform.position) <= maxWhipDistance)
        {
            circleObject.SetActive(true);
        }
        else
        {
            circleObject.SetActive(false);
        }
    }
        public override void LevelUp()
        {
            SkillLevel += 1;
        }
    //If the player has the gunSlinger skill it shoots something from the gun.
    void Shoot()
    {
        canShoot = false;
        Rigidbody clone;
        clone = Instantiate(bullet, HoldItemPosition.position, closestItem.transform.rotation);
        var step =  speed * Time.deltaTime; // calculate distance to move
        clone.velocity = (closestItem.transform.position - clone.position).normalized * projectileSpeed;
        Invoke(nameof(ResetShoot), 2f);
    }
    void ResetShoot()
    {
        canShoot = true;
    }
    //finds the 2nd closest item out of the list and updates it while the player moves around
    void detectNextClosestItem()
    {
        foreach (GameObject gO in allObjects)
        {
            if (!gO.activeInHierarchy)
            {
                continue;
            }
            if (nextClosestItem == null)
            {
                nextClosestItem = gO;
                continue;
            }
            if (gO == closestItem)
            {
                continue;
            }
            if (Vector3.Distance(gO.transform.position, transform.position) < Vector3.Distance(nextClosestItem.transform.position, transform.position))
            {
                nextClosestItem = gO;
            }
        }
        if (Vector3.Distance(nextClosestItem.transform.position, transform.position) < Vector3.Distance(closestItem.transform.position, transform.position))
        {
            findCloseItem = true;
        }
    }
    //makes whip able to move and moves it forward based off whip strength
    public override void HatAbility()
    {
        if(closestItem != null)
        {
            GetComponent<SphereCollider>().enabled = true;
            originalWorldPosition = transform.position;
            rb.isKinematic = false;
            rb.AddForce(transform.forward * whipStrength, ForceMode.Impulse);
        }
        base.HatAbility();
        if (gunSlinger == true)
        {

        }
    }
    //sets it to where it cant move and moves it back to the original position
    void ResetHat()
    {
        GetComponent<SphereCollider>().enabled = false;
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        transform.localPosition = originalLocalPosition;
    }
    //trigger logic for when it hits an object to pick up the item
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PickUp")
        {
            if (other.gameObject.GetComponent<Item>() && other.gameObject.GetComponent<Item>().inInventory == false)
            {
                if (!playerInv.holdingItem)
                {
                playerInv.HoldItem(other.gameObject);
                ResetHat();
                allObjects.Remove(other.gameObject);
                }
                else
                {
                    if (SkillLevel > 2 && gunSlinger == false)
                    {
                        HoldItem(other.gameObject);
                        ResetHat();
                    }
                }
            }
            // else if (playerInv.holdingItem == true && SkillLevel > 2 && gunSlinger == false && holdingItem == false)
            // {
                
            // }
            if (other.gameObject.GetComponent<WhippableObject>())
            {
                other.GetComponent<WhippableObject>().runProperties = true;
            }
        }
        if (other.gameObject.tag == "enemy")
        {
            // other.gameObject.GetComponent<Rigidbody>().AddForce(Player.transform.position * whipStrength, ForceMode.Impulse);
            ResetHat();
            // other.gameObject.GetComponent<BaseEnemyAI>().stunned = true;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "enemy")
        {
            // other.gameObject.GetComponent<Rigidbody>().AddForce(Player.transform.position * whipStrength, ForceMode.Impulse);
            // other.gameObject.GetComponent<BaseEnemyAI>().stunned = true;
        }
    }
    public float MaxWhipDis()
    {
        return maxWhipDistance;
    }

    public float IncreasedWhipDis()
    {
        float addedDis = maxWhipDistance * 2;
        return addedDis;
    }


        public void HoldItem(GameObject Item)
    {
        if (Item.GetComponent<Item>().ps != null)
        {
            Item.GetComponent<Item>().ps.Stop();
        }
        
        Item.GetComponent<SphereCollider>().enabled = false;
        Item.GetComponent<BoxCollider>().enabled = false;
        allObjects.Remove(Item);
        findCloseItem = true;
        closestItem = null;
        // circleObject.SetActive(false);
        // Item.GetComponent<Item>().inInventory = true;
        // Player.GetComponent<Inventory>().MassText = Player.GetComponent<Inventory>().MassText + Item.GetComponent<Item>().Weight;
        if (pullEnemy == true)
        {
        Player.GetComponent<Inventory>().rb.mass = Player.GetComponent<Inventory>().rb.mass + (Item.GetComponent<Item>().Weight * 0.2f);
        }
        Item.transform.parent = gameObject.transform;
        Item.transform.position = HoldItemPosition.position;
        Item.transform.rotation = HoldItemPosition.rotation;
        Item.GetComponent<Rigidbody>().isKinematic = true;
        holdingItem = true;
        currentHeldItem = Item;
        heldItem = Item.GetComponent<Item>();
    }

    public void DropItem(GameObject Item)
    {
        if (Item.GetComponent<Item>().ps != null)
        {
            Item.GetComponent<Item>().ps.Play();
        }
        if (pullEnemy == true)
        {
        Player.GetComponent<Inventory>().rb.mass = Player.GetComponent<Inventory>().rb.mass - (Item.GetComponent<Item>().Weight * 0.2f);
        }
        // Item.GetComponent<Item>().inInventory = false;
        Item.GetComponent<SphereCollider>().enabled = true;
        Item.GetComponent<BoxCollider>().enabled = true;
        // circleObject.SetActive(true);
        int LayerPickUp = LayerMask.NameToLayer("PickUp");
        Item.gameObject.layer = LayerPickUp;
        Item.gameObject.tag="PickUp";
        // MassText = MassText - Item.GetComponent<Item>().Weight;
        // rb.mass = rb.mass - (Item.GetComponent<Item>().Weight * 0.2f);
        Item.transform.parent = null;
        Item.GetComponent<Rigidbody>().isKinematic = false;
        Item.GetComponent<Rigidbody>().useGravity = true;
        currentHeldItem = null;
        heldItem.dropped = true;
        holdingItem = false;
        base.HatAbility();
        // StartCoroutine(dropTimer(0.5f, false));

        // if (storeParticles != null)
        // {
        //     Instantiate(storeParticles, transform.position, transform.rotation);
        // }
    }
}
