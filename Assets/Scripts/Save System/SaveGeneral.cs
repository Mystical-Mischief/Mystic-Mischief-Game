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
        SaveEnemyCheckPoint();
        controls = new ControlsforPlayer();
        controls.Enable();
    }

    // Update is called once per frame
    void Update()
    {
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
            LoadEnemy();
        }
        //If the player selects Load from the menu it loads the save.
        if (LoadMenu == true)
        {
            LoadEnemy();
            LoadMenu = false;
        }
    }

    //Saves everything.
    public void SaveEnemy ()
    {
        //Saves the items.
        foreach (GameObject items in Items)
        {
            items.GetComponent<Item>().SaveItem();
        }
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
        Camera.GetComponent<MainCameraData>().SaveCamera();
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
        Camera.GetComponent<MainCameraData>().SaveCamera();
    }
    //Loads everything from the savve file (not the checkpoint save).
    public void LoadEnemy ()
    {
        //Loads all of the enemies.
        foreach (GameObject enemy in Enemies)
        {
            enemy.GetComponent<BaseEnemyAI>().LoadEnemy();
        }
        //Loads the items
        foreach (GameObject Inv in PickedUpItems)
        {
            Inv.SetActive(false);
        }
        //Loads the players inventory.
        foreach (GameObject items in Items)
        {
            items.GetComponent<Item>().LoadItem();
            if (items.GetComponent<Item>().inInventory == false)
            {
                    items.SetActive(true);
            }
        }
        // Loads the player usings the players load function
        Player.GetComponent<ThirdPersonController>().LoadPlayer();
        Camera.GetComponent<MainCameraData>().LoadCamera();
    }
    //Loads the last checkpoint.
        public void LoadCheckpoint ()
    {
        //Loads the enemies.
        foreach (GameObject enemy in Enemies)
        {
            enemy.GetComponent<BaseEnemyAI>().LoadEnemy();
        }
        // loads the players inventory.
        foreach (GameObject Inv in PickedUpItems)
        {
            Inv.SetActive(false);
        }
        //Loads the items.
        foreach (GameObject items in Items)
        {
            items.GetComponent<Item>().LoadItem();
            if (items.GetComponent<Item>().inInventory == false)
            {
                    items.SetActive(true);
            }
        }
        //Loads the player from the players load function.
        Player.GetComponent<ThirdPersonController>().LoadCheckpoint();
    }
    public virtual void Loadmenu()
    {
        LoadMenu = true;
    }
}