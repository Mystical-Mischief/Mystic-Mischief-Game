using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ASyncLoadManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _loadScreen;

    public Animator transitionMask;

    [SerializeField] private GameObject loadCanvas; 

    [SerializeField] private float transitionDelay; 

    private void Awake()
    {
        DontDestroyOnLoad(this);        // Ensures that the load screen is not immediently destoryed when loading the scene. Game objects must manually be deleted. -Emilie 
        DontDestroyOnLoad(loadCanvas);

        if (_loadScreen != null )
        {
            _loadScreen.SetActive( false );
        }
    }
    public void PlayGame()
    {
        PlayerPrefs.SetInt("Completed", 0);
        if (_loadScreen != null)
        {
            _loadScreen.SetActive(true);
        }
        StartCoroutine(LoadLevelASync(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void GoToLevel(string name)
    {
        Debug.Log("Going places...");

        if (_loadScreen != null)
        {
            _loadScreen.SetActive(true);
        }
        StartCoroutine(LoadLevelASync(name));
    }

    IEnumerator LoadLevelASync(string levelName)
    {
        yield return null;

        transitionMask.SetTrigger("Shrink");

        yield return new WaitForSecondsRealtime(transitionDelay);    //Gives time for the trasition animation to fully play -Emilie 

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelName);
       //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
        loadOperation.allowSceneActivation = false; //Gives control on when to activate the level -Emilie 

        while (!loadOperation.isDone)
        {
            if(loadOperation.progress >= 0.9f)
            {
                loadOperation.allowSceneActivation = true;
                yield return new WaitForSeconds(transitionDelay);
                transitionMask.SetTrigger("Grow");
                AudioListener.pause = false;
            }

            yield return null;
        }

        yield return new WaitForSeconds(transitionDelay);  //Reusing delay before destroying objects below -Emilie 
        Destroy(this.gameObject);                                     //Since each level will need a load screen, will we want to consider keeping these objects in the future? 
        Destroy(loadCanvas);

    }

    IEnumerator LoadLevelASync(int levelIndex)
    {
        yield return null;

        transitionMask.SetTrigger("Shrink");

        yield return new WaitForSeconds(transitionDelay);    //Gives time for the trasition animation to fully play -Emilie 

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelIndex);

        loadOperation.allowSceneActivation = false; //Gives control on when to activate the level -Emilie 

        while (!loadOperation.isDone)
        {
            if (loadOperation.progress >= 0.9f)
            {
                loadOperation.allowSceneActivation = true;
                yield return new WaitForSeconds(transitionDelay);
                transitionMask.SetTrigger("Grow");
            }

            yield return null;
        }

        yield return new WaitForSeconds(transitionDelay);  //Reusing delay before destroying objects below -Emilie 
        Destroy(this.gameObject);                                     //Since each level will need a load screen, will we want to consider keeping these objects in the future? 
        Destroy(loadCanvas);
    }
}
