using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ASyncLoadManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _loadScreen;

    private void Awake()
    {
        if(_loadScreen != null )
        {
            _loadScreen.SetActive( false );
        }
    }
    public void PlayGame()
    {
        if (_loadScreen != null)
        {
            _loadScreen.SetActive(true);
        }
        StartCoroutine(LoadLevelASync(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void GoToLevel(string name)
    {
        if (_loadScreen != null)
        {
            _loadScreen.SetActive(true);
        }
        StartCoroutine(LoadLevelASync(name));
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
