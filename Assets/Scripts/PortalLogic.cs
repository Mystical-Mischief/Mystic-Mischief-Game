using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalLogic : MonoBehaviour
{
    public string SceneName;
    private void Start()
    {
        if(PlayerPrefs.GetInt("Completed") == 1)
        {
            SceneName = "Wiz Hub Variant";
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (FindObjectOfType<ActivateQuest>())
            {
                FindObjectOfType<ActivateQuest>().ResetActivateQuests();
            }
            SceneManager.LoadScene(SceneName);
        }
    }
}
