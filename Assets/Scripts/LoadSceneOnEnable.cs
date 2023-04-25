using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnEnable : MonoBehaviour
{
    public string SceneToLoad;
    [SerializeField] ASyncLoadManager aSyncLoadManager;
    private void OnEnable()
    {
        if (FindObjectOfType<ActivateQuest>())
        {
            FindObjectOfType<ActivateQuest>().ResetActivateQuests();
        }
        //SceneManager.LoadScene(SceneToLoad);
        aSyncLoadManager.GoToLevel(SceneToLoad);
    }
}
