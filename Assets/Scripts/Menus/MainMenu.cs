using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    public GameObject Saves;
    [SerializeField] EventSystem eventSystem;

    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(LoadLevelASync(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void GoToLevel1(string name)
    {
        //SceneManager.LoadScene(2);
        StartCoroutine(LoadLevelASync(2));
    }
    
    public void GoToLevel2()
    {
        StartCoroutine(LoadLevelASync("Level 2"));
    }
    
    public void GoToLevel3()
    {
        StartCoroutine(LoadLevelASync("Level 3"));
    }

    public void GoToWizardHub() => SceneManager.LoadScene(8);

    public void LoadGame()
    {
        StartCoroutine(LoadLevelASync(SceneManager.GetActiveScene().buildIndex + 1));
        Saves.GetComponent<SaveGeneral>().Loadmenu();
    }

    public void QuitGame()
    {
        Debug.Log("Quiting Game...");
        Application.Quit();
    }

    IEnumerator LoadLevelASync(string levelName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelName);

        while (!loadOperation.isDone)
        {
            yield return null;
        }
    }

    IEnumerator LoadLevelASync(int levelIndex)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelIndex);

        while (!loadOperation.isDone)
        {
            yield return null;
        }
    }
}
