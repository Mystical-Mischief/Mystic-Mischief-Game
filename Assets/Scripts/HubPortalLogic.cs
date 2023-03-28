using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubPortalLogic : MonoBehaviour
{
    static string levelName;
    public GameObject levelSelector;
    private void Start()
    {
        if(levelName != null)
            GetComponent<PortalLogic>().SceneName = levelName;
        if(PlayerPrefs.GetInt("Completed") == 1)
        {
            addLevelSelector();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            switch (GetComponent<PortalLogic>().SceneName)
            {
                case "Level 2":
                    levelName = "Intro Level 3";
                    break;
                case "Intro Level 3":
                    break;
            }
            print(levelName);
        }
    }
    void addLevelSelector()
    {
        levelSelector.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
