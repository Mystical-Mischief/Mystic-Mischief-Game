using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipSceneScript : MonoBehaviour
{
    ControlsforPlayer controls;
    public string SceneName;
    [SerializeField] ASyncLoadManager aSyncLoadManager;
    bool loadedLevel;
    private void Start()
    {
        controls = new ControlsforPlayer();
        controls.Enable();
        controls.MenuActions.SkipScene.performed += _ => SkipCutscene();
    }



    void SkipCutscene()
    {
            if (VideoScript.canSkipVideo == true)
            {
                if (controls.MenuActions.SkipScene.WasPerformedThisFrame() && !loadedLevel)
                {
                    loadedLevel = true;
                    //SceneManager.LoadScene(SceneName);
                    aSyncLoadManager.GoToLevel(SceneName);
                    //StartCoroutine(LoadLevelASync(SceneManager.GetActiveScene().buildIndex + 1));
                }
            }

    }

    // private void Update()
    // {
    //     if (controls.MenuActions.SkipScene.WasPerformedThisFrame() && !loadedLevel)
    //     {
    //         loadedLevel = true;
    //         //SceneManager.LoadScene(SceneName);
    //         aSyncLoadManager.GoToLevel(SceneName);
    //         //StartCoroutine(LoadLevelASync(SceneManager.GetActiveScene().buildIndex + 1));
    //     }
    //
    // }
    IEnumerator LoadLevelASync(int levelIndex)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelIndex);

        while (!loadOperation.isDone)
        {
            yield return null;
        }
    }

}
