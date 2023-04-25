using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipSceneScript : MonoBehaviour
{
    ControlsforPlayer controls;
    public string SceneName;
    [SerializeField] ASyncLoadManager aSyncLoadManager;

    private void Start()
    {
        controls = new ControlsforPlayer();
        controls.Enable();
    }
    private void Update()
    {
        if (controls.MenuActions.SkipScene.WasPerformedThisFrame())
        {
            //SceneManager.LoadScene(SceneName);
            aSyncLoadManager.GoToLevel(SceneName);
            //StartCoroutine(LoadLevelASync(SceneManager.GetActiveScene().buildIndex + 1));
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
