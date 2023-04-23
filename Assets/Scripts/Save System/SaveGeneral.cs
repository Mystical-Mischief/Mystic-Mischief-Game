using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class SaveGeneral : MonoBehaviour
{
    ControlsforPlayer controls;
    public List<GameObject> Enemies = new List<GameObject>();
    public GameObject Player;
    public List<GameObject> PickedUpItems = new List<GameObject>();
    public static bool LoadMenu;
    public List<GameObject> Items = new List<GameObject>();
    public GameObject Camera;
    // public GameObject Dragon;
    // public string DragonType;
    public Reload reload;
    private float questNum;
    public GameObject q;
    public static List<QuestInfo> currentQuests = new List<QuestInfo>();
    public List<QuestInfo> Quests = new List<QuestInfo>();
    public PlayerHatLogic Hats;
    public List<GameObject> TurnsOn = new List<GameObject>();
    public static bool SavedManualLast;
    public static bool SavedCheckpointLast;
    public bool CheckpointSave;
    public bool ManualSave;
       
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
        q = GameObject.Find("QuestTracker");
        Hats = GameObject.Find("Hats").GetComponent<PlayerHatLogic>();
        foreach (GameObject gO in GameObject.FindGameObjectsWithTag("enemy"))
        {
            if (gO.activeSelf == true)
            {
                Enemies.Add(gO);
            }
        }

        foreach (GameObject item in GameObject.FindGameObjectsWithTag("PickUp"))
        {
            if (item.activeSelf == true && item.gameObject.GetComponent<Item>().itemType != Item.ItemType.Objective)
            {
                Items.Add(item);
            }
        }
        // SaveEnemyCheckPoint();
        // if (reload != null)
        // {
            // if (reload.retrying == true)
            // {
            //     //Saves the checkpoint when the level starts.
            //     // SaveEnemyCheckPoint();
            //     reload.reloaded = true;
            // }
            if (reload.Loading == true)
            {
                if (SavedCheckpointLast == true)
                {
                    LoadCheckpoint();
                }
                if (SavedManualLast == true)
                {
                LoadEnemy();
                }
            }
        // }
        controls.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        ManualSave = SavedManualLast;
        CheckpointSave = SavedCheckpointLast;
        Quests = currentQuests;
        // if (reload.retrying == true)
        // {
        //     LoadEnemy();
        //     reload.reloaded = true;
        // }
        if (reload.Loading == true)
        {
            LoadEnemy();
            reload.Loaded = true;
        }
        PickedUpItems = Player.GetComponent<Inventory>().PickedUpItems;
        // bool Load = controls.MenuActions.Load.ReadValue<float>() > 0.1f;
        // bool Save = controls.MenuActions.Save.ReadValue<float>() > 0.1f;
        //Saves everything on button press.
        // if (controls.MenuActions.Save.triggered)
        // {
        //     Debug.Log("Saved");
        //     SaveEnemy();
        // }
        // //Loads everything on button press.
        // if (controls.MenuActions.Load.triggered)
        // {
        //     LoadCheckpoint();
        // }
        //If the player selects Load from the menu it loads the save.
        if (LoadMenu == true)
        {
            LoadCheckpoint();
            LoadMenu = false;
        }
    }
    public void Load()
    {
        if (SavedCheckpointLast == true)
        {
            LoadCheckpoint();
        }
        if (SavedManualLast == true)
        {
            LoadEnemy();
        }
    }

    //Saves everything.
    public void SaveEnemy ()
    {
        currentQuests.Clear();
        //Saves the enemies.
        foreach (GameObject enemy in Enemies)
        {
            if (enemy != null)
            {
            enemy.GetComponent<BaseEnemyAI>().SaveEnemy();
            }
        }
        //Saves the players inventory.
        foreach (GameObject Item in PickedUpItems)
        {
            Item.GetComponent<Item>().inInventory = true;
        }

        //Saves the items.
        SaveLotsOfItems(); //Run an asyc function so that items can save in the background instead of freezing the game to do it all at once. 

        //Saves the player usings the players save function.
        Player.GetComponent<PlayerController>().SavePlayer();
        Camera.GetComponent<CameraLogic>().SaveCamera();
        foreach(QuestInfo quests in q.GetComponent<Quest>().currentQuests)
        {
            currentQuests.Add(quests);
        }
        Hats.SaveHats();
        SavedCheckpointLast = false;
        SavedManualLast = true;
    }
    //Saves everything when the player reaches a checkpoint.
    public void SaveEnemyCheckPoint()
    {
        currentQuests.Clear();
        //Saves the enemies.
        foreach (GameObject enemy in Enemies)
        {
            if (enemy != null)
            {
            enemy.GetComponent<BaseEnemyAI>().SaveEnemy();
            }
        }
        //Saves the players inventory.
        foreach (GameObject Inv in PickedUpItems)
        {
            Inv.GetComponent<Item>().inInventory = true;
        }
        //Saves the player usings the players save function.
        Player.GetComponent<PlayerController>().SavePlayer();
        Camera.GetComponent<CameraLogic>().SaveCamera();
        // if (DragonType == "Water Dragon")
        // {
        // Dragon.GetComponent<WaterDragonAi>().SaveDragon();
        // }
        foreach(QuestInfo quests in q.GetComponent<Quest>().currentQuests)
        {
            currentQuests.Add(quests);
        }
        foreach (GameObject items in Items)
        {
            if (items != null)
            {
                if (items.GetComponent<Item>() != null)
                {
                    items.GetComponent<Item>().SaveItem();
                }
            }
        }
        // Hats.SaveHats();
        SavedCheckpointLast = true;
        SavedManualLast = false;
    }
    //Loads everything from the savve file (not the checkpoint save).
    public void LoadEnemy ()
    {
        foreach (GameObject turnson in TurnsOn)
        {
                if (turnson.GetComponent<Turnsnn>().isActivated == true)
                {
                    turnson.SetActive(true);
                }
        }
        //Loads all of the enemies.
        foreach (GameObject enemy in Enemies)
        {
            if (enemy != null)
            {
            enemy.GetComponent<BaseEnemyAI>().LoadEnemy();
            }
        }
        //Loads the players inventory.
        foreach (GameObject items in Items)
        {
            if (items != null)
            {
                if (items.GetComponent<Item>() != null)
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
            }
        }
        // // Loads the player usings the players load function
        Player.GetComponent<PlayerController>().LoadPlayer();
        Camera.GetComponent<CameraLogic>().LoadCamera();
        // if (DragonType == "Water Dragon")
        // {
        // Dragon.GetComponent<WaterDragonAi>().LoadDragon();
        // }
        // q.GetComponent<ActivateQuest>().startQuests.Clear();
        q.GetComponent<Quest>().currentQuests.Clear();
        q.GetComponent<ActivateQuest>().activateQuest(currentQuests);
        Hats.LoadHats();
        reload.Loaded = true;
    }
    //Loads the last checkpoint.
        public void LoadCheckpoint ()
    {
        foreach (GameObject turnson in TurnsOn)
        {
                    turnson.SetActive(true);
                    if (turnson.name == "CleverBird")
                    {
                    turnson.GetComponent<ActivateDialogue>().enabled = true;
                    }
        }
        //Loads all of the enemies.
        foreach (GameObject enemy in Enemies)
        {
            if (enemy != null)
            {
            enemy.GetComponent<BaseEnemyAI>().LoadEnemy();
            }
        }
        //Loads the players inventory.
        foreach (GameObject items in Items)
        {
            if (items != null)
            {
                if (items.GetComponent<Item>() != null)
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
            }
        }
        // Loads the player usings the players load function
        Player.GetComponent<PlayerController>().LoadPlayer();
        Camera.GetComponent<CameraLogic>().LoadCamera();
        // if (DragonType == "Water Dragon")
        // {
        // Dragon.GetComponent<WaterDragonAi>().LoadDragon();
        // }
        q.GetComponent<Quest>().currentQuests.Clear();
        q.GetComponent<ActivateQuest>().activateQuest(currentQuests);
        // Hats.LoadHats();
        foreach (GameObject turnson in TurnsOn)
        {
            if (turnson.GetComponent<Turnsnn>() != null)
            {
                if (turnson.GetComponent<Turnsnn>().isActivated != true)
                {
                    turnson.SetActive(false);
                }
            }
        }
    }
    public virtual void Loadmenu()
    {
        LoadMenu = true;
    }
   async void SaveLotsOfItems()
    {
        foreach (GameObject items in Items)
        {
            if (items != null)
            {
                if (items.GetComponent<Item>() != null)
                {
                    items.GetComponent<Item>().SaveItem();
                }
            }
            await Task.Yield();
        }
    }
}