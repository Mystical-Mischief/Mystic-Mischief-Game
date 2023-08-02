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

    VideoScript videoScript; 
    private void Start()
    {
        controls = new ControlsforPlayer();
        controls.Enable();

        controls.MenuActions.SkipScene.performed += _ => SkipCutscene();
        videoScript = new VideoScript();
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
 //     if (videoScript.canSkipVideo == true)
 //     {
 //         if (controls.MenuActions.SkipScene.WasPerformedThisFrame() && !loadedLevel)
 //         {
 //             loadedLevel = true;
 //             //SceneManager.LoadScene(SceneName);
 //             aSyncLoadManager.GoToLevel(SceneName);
 //             //StartCoroutine(LoadLevelASync(SceneManager.GetActiveScene().buildIndex + 1));
 //             Debug.Log("sKIPPING");
 //         }
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
