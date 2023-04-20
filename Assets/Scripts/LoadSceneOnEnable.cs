using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnEnable : MonoBehaviour
{
    public string SceneToLoad;

    private void OnEnable()
    {
        if (FindObjectOfType<ActivateQuest>())
        {
            FindObjectOfType<ActivateQuest>().ResetActivateQuests();
        }
        SceneManager.LoadScene(SceneToLoad);
    }
}
