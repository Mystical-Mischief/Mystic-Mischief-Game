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
        yield return null;

        transitionMask.SetTrigger("Shrink");

        yield return new WaitForSeconds(transitionDelay);    //Gives time for the trasition animation to fully play. It does artifically increase load time though -Emilie 

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelName);

        loadOperation.allowSceneActivation = false; //Gives control on when to activate the level -Emilie 

        while (!loadOperation.isDone)
        {
            if(loadOperation.progress >= 0.9f)
            {
                loadOperation.allowSceneActivation = true;
                yield return new WaitForSeconds(transitionDelay);

                transitionMask.SetTrigger("Grow");
            }

            yield return null;
        }

        yield return new WaitForSeconds(transitionDelay);  //Resuing delay for destroying objects below -Emilie 
        Destroy(this);                                     //Every scene may end up requiring a load screen, so we might want to consider not deleting these at all in the future. -Emilie
        Destroy(loadCanvas);

    }

    IEnumerator LoadLevelASync(int levelIndex)
    {
        yield return null;

        transitionMask.SetTrigger("Shrink");

        yield return new WaitForSeconds(transitionDelay);    //Gives time for the trasition animation to fully play. It does artifically increase load time though -Emilie 

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

        yield return new WaitForSeconds(transitionDelay);  //Resuing delay for destroying objects below -Emilie 
        Destroy(this);                                     //Every scene may end up requiring a load screen, so we might want to consider not deleting these at all in the future. -Emilie
        Destroy(loadCanvas);
    } //This is identical to function above, consider a way for one function to accept two different parameters: int or string? - Emilie
}
