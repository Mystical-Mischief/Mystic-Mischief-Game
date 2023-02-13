using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGeneral : MonoBehaviour
{
    ControlsforPlayer controls;
    public List<GameObject> Enemies = new List<GameObject>();
    public GameObject Player;
    public List<GameObject> PickedUpItems = new List<GameObject>();
    public static bool LoadMenu;
    public List<GameObject> Items = new List<GameObject>();
    public GameObject Camera;
    public GameObject Dragon;
    public string DragonType;
    public Reload reload;
       
       void Awake()
       {
            controls = new ControlsforPlayer();
       }
        public void OnEnable()
    {
        controls.Enable();
    }
    public void OnDisable()
    {
        controls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Saves the checkpoint when the level starts.
        // SaveEnemyCheckPoint();
        controls.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (reload.retrying == true)
        {
            LoadCheckpoint();
            reload.reloaded = true;
        }
        PickedUpItems = Player.GetComponent<Inventory>().PickedUpItems;
        // bool Load = controls.MenuActions.Load.ReadValue<float>() > 0.1f;
        // bool Save = controls.MenuActions.Save.ReadValue<float>() > 0.1f;
        //Saves everything on button press.
        if (controls.MenuActions.Save.triggered)
        {
            Debug.Log("Saved");
            SaveEnemy();
        }
        //Loads everything on button press.
        if (controls.MenuActions.Load.triggered)
        {
            LoadCheckpoint();
        }
        //If the player selects Load from the menu it loads the save.
        if (LoadMenu == true)
        {
            LoadCheckpoint();
            LoadMenu = false;
        }
    }

    //Saves everything.
    public void SaveEnemy ()
    {
        //Saves the enemies.
        foreach (GameObject enemy in Enemies)
        {
            enemy.GetComponent<BaseEnemyAI>().SaveEnemy();
        }
        //Saves the players inventory.
        foreach (GameObject Item in PickedUpItems)
        {
            Item.GetComponent<Item>().inInventory = true;
        }
        //Saves the items.
        foreach (GameObject items in Items)
        {
            items.GetComponent<Item>().SaveItem();
        }
        //Saves the player usings the players save function.
        Player.GetComponent<ThirdPersonController>().SavePlayer();
        Camera.GetComponent<CameraLogic>().SaveCamera();
        if (DragonType == "Water Dragon")
        {
        Dragon.GetComponent<WaterDragonAi>().SaveDragon();
        }
    }
    //Saves everything when the player reaches a checkpoint.
    public void SaveEnemyCheckPoint()
    {
        //Saves the enemies.
        foreach (GameObject enemy in Enemies)
        {
            enemy.GetComponent<BaseEnemyAI>().SaveEnemy();
        }
        //Saves the players inventory.
        foreach (GameObject Inv in PickedUpItems)
        {
            Inv.GetComponent<Item>().inInventory = true;
        }
        //Saves the player usings the players save function.
        Player.GetComponent<ThirdPersonController>().SavePlayer();
        Camera.GetComponent<CameraLogic>().SaveCamera();
        if (DragonType == "Water Dragon")
        {
        Dragon.GetComponent<WaterDragonAi>().SaveDragon();
        }
    }
    //Loads everything from the savve file (not the checkpoint save).
    public void LoadEnemy ()
    {
        // Player.GetComponent<Inventory>().PickedUpItems = null;
        //Loads all of the enemies.
        foreach (GameObject enemy in Enemies)
        {
            enemy.GetComponent<BaseEnemyAI>().LoadEnemy();
        }
        //Loads the items
        // foreach (GameObject Item in PickedUpItems)
        // {
        //     // if (Item.GetComponent<Item>().inInventory == false)
        //     // {
        //     //     Player.GetComponent<Inventory>().PickedUpItems.Remove(Item);
        //     // }
            
        //     // Player.GetComponent<Inventory>().PickedUpItems.Add(Item);
        //     // Item.GetComponent<Item>().inInventory = false;
        // }
        //Loads the players inventory.
        foreach (GameObject items in Items)
        {
            items.GetComponent<Item>().LoadItem();
            if (items.GetComponent<Item>().inInventory == true)
            {
                if (!PickedUpItems.Contains(items))
                {
                    Player.GetComponent<Inventory>().PickedUpItems.Add(items);
                    items.GetComponent<Item>().inInventory = false;
                }

            }
            // if (items.GetComponent<Item>().inInventory == false)
            // {
            //         items.SetActive(true);
            // }
        }
        // Loads the player usings the players load function
        Player.GetComponent<ThirdPersonController>().LoadPlayer();
        Camera.GetComponent<CameraLogic>().LoadCamera();
        if (DragonType == "Water Dragon")
        {
        Dragon.GetComponent<WaterDragonAi>().LoadDragon();
        }
    }
    //Loads the last checkpoint.
        public void LoadCheckpoint ()
    {
        // Player.GetComponent<Inventory>().PickedUpItems = null;
        //Loads all of the enemies.
        foreach (GameObject enemy in Enemies)
        {
            enemy.GetComponent<BaseEnemyAI>().LoadEnemy();
        }
        //Loads the players inventory.
        foreach (GameObject items in Items)
        {
            items.GetComponent<Item>().LoadItem();
            if (items.GetComponent<Item>().inInventory == true)
            {
                if (!PickedUpItems.Contains(items))
                {
                    Player.GetComponent<Inventory>().PickedUpItems.Add(items);
                    items.GetComponent<Item>().inInventory = false;
                }

            }
        }
        // Loads the player usings the players load function
        Player.GetComponent<ThirdPersonController>().LoadPlayer();
        Camera.GetComponent<CameraLogic>().LoadCamera();
        if (DragonType == "Water Dragon")
        {
        Dragon.GetComponent<WaterDragonAi>().LoadDragon();
        }
    }
    public virtual void Loadmenu()
    {
        LoadMenu = true;
    }
}