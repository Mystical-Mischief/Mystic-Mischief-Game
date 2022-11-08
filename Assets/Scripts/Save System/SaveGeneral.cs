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

    // Start is called before the first frame update
    void Start()
    {
        SaveEnemyCheckPoint();
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
        if (Save && Player.GetComponent<ThirdPersonController>().Targeted == false)
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
        foreach (GameObject items in Items)
        {
            items.GetComponent<Item>().SaveItem();
        }
        foreach (GameObject enemy in Enemies)
        {
            enemy.GetComponent<BaseEnemyAI>().SaveEnemy();
        }
        foreach (GameObject Inv in PickedUpItems)
        {
            Inv.GetComponent<Item>().inInventory = true;
        }
        Player.GetComponent<ThirdPersonController>().SavePlayer();
    }
    public void SaveEnemyCheckPoint()
    {
        foreach (GameObject enemy in Enemies)
        {
            enemy.GetComponent<BaseEnemyAI>().SaveEnemy();
        }
        foreach (GameObject Inv in PickedUpItems)
        {
            Inv.GetComponent<Item>().inInventory = true;
        }
        Player.GetComponent<ThirdPersonController>().SavePlayer();
    }
    public void LoadEnemy ()
    {
        foreach (GameObject enemy in Enemies)
        {
            enemy.GetComponent<BaseEnemyAI>().LoadEnemy();
        }
        foreach (GameObject Inv in PickedUpItems)
        {
            Inv.SetActive(false);
        }
        foreach (GameObject items in Items)
        {
            items.GetComponent<Item>().LoadItem();
            items.SetActive()
        }
        Player.GetComponent<ThirdPersonController>().LoadPlayer();
    }
    public virtual void Loadmenu()
    {
        LoadMenu = true;
    }
}

    //     public void SavePlayer ()
    // {
    //     SaveSystem.SavePlayer(this);
    //     Saved = true;
    // }
    // public void LoadPlayer ()
    // {
    //     PlayerData data = SaveSystem.LoadPlayer();
    //     currentHealth = data.health;
    //     Vector3 position;
    //     position.x = data.position[0];
    //     position.y = data.position[1];
    //     position.z = data.position[2];
    //     transform.position = position;
    //     Stamina = data.Stamina;
    // }

    //         public void Checkpoint ()
    // {
    //     SaveSystem.Checkpoint(this);
    //     Saved = true;
    // }
    // public void LoadCheckpoint ()
    // {
    //     PlayerData data = SaveSystem.LoadCheckpoint();
    //     currentHealth = data.health;
    //     Vector3 position;
    //     position.x = data.position[0];
    //     position.y = data.position[1];
    //     position.z = data.position[2];
    //     transform.position = position;
    //     Stamina = data.Stamina;
    // }

    //     void OnCollisionEnter(Collision other)
    // {
    //     if(other.gameObject.CompareTag("wall")){
    //         Vector3 direction = other.contacts[0].point - transform.position;
    //         direction = -direction.normalized;
    //         rb.AddForce((-transform.forward * 1000) * powerValue);
    //     }
    //         if(other.gameObject.CompareTag("Water")){
    //             moveForce = 1f;
    //     }
    // }
    // void OnCollisionExit(Collision other)
    // {
    //         if(other.gameObject.CompareTag("Water")){
    //             moveForce = 5f;
    //     } 
    // }

    //     private void OnTriggerStay(Collider other)
    // {
    //     if (other.gameObject.tag == "Checkpoint")
    //     {
    //         Checkpoint();
    //     }
    // }

//SAVE ENEMY
    //         public void SaveEnemy ()
    // {
    //     SaveSystem.SaveEnemy(this);
    //     Debug.Log("Saved");
    // }
    // public void LoadEnemy ()
    // {
    //     EnemyData data = SaveSystem.LoadEnemy(this);
    //     patrolNum = data.patrolNum;
    //     target = PatrolPoints[patrolNum];
    //     UpdateDestination(target.position);

    //     Vector3 position;
    //     position.x = data.position[0];
    //     position.y = data.position[1];
    //     position.z = data.position[2];
    //     transform.position = position;
    //     spottedPlayer = data.spottedPlayer;
    // }