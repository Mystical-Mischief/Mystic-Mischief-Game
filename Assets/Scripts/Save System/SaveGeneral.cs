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

    // Start is called before the first frame update
    void Start()
    {
        controls = new ControlsforPlayer();
        controls.Enable();
    }
    public void OnEnable()
    {
        controls.Enable();
    }
    public void OnDisable()
    {
        controls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        PickedUpItems = Player.GetComponent<Inventory>().PickedUpItems;
        bool Load = controls.MenuActions.Load.ReadValue<float>() > 0.1f;
        bool Save = controls.MenuActions.Save.ReadValue<float>() > 0.1f;
        if (Save)
        {
            SaveEnemy();
        }
        if (Load)
        {
            LoadEnemy();
        }
        if (LoadMenu == true)
        {
            LoadEnemy();
            LoadMenu = false;
        }
    }

    public void SaveEnemy ()
    {
        foreach (GameObject enemy in Enemies)
        {
            enemy.GetComponent<BaseEnemyAI>().SaveEnemy();
        }
        foreach (GameObject item in PickedUpItems)
        {
            item.GetComponent<Item>().inInventory = true;
        }
        Player.GetComponent<ThirdPersonController>().SavePlayer();
    }
    public void LoadEnemy ()
    {
        foreach (GameObject enemy in Enemies)
        {
            enemy.GetComponent<BaseEnemyAI>().LoadEnemy();
        }
        foreach (GameObject item in PickedUpItems)
        {
            item.SetActive(false);
        }
        Player.GetComponent<ThirdPersonController>().LoadPlayer();
    }
    public virtual void Loadmenu()
    {
        LoadMenu = true;
    }
}
