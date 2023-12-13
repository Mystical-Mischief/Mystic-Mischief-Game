using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour
{
    VideoPlayer video;
    public string SceneName;
    [SerializeField] ASyncLoadManager aSyncLoadManager; 

    [SerializeField] private float delayTime = 3f;

    private Animator videoAnimator; //Added a fade in animation so the cutscene doesn't jumpcut on screen. 

    public static bool canSkipVideo = true;

    void Awake()
    {
        video = GetComponent<VideoPlayer>();
        videoAnimator = video.GetComponent<Animator>();
        video.Pause();
        canSkipVideo = true;
    }


    private void Start()  //Start seems to allow the Coroutine below to work. -Emilie
    {
        StartCoroutine(DelayedVideo()); //Starts a timer to delay the video allowing the load screen transition to finish. -Emilie
        video.loopPointReached += CheckOver;
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        //the scene that you want to load after the video has ended.
        //SceneManager.LoadScene(SceneName);
        canSkipVideo = false;
        aSyncLoadManager.GoToLevel(SceneName);

        //StartCoroutine(LoadLevelASync(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator LoadLevelASync(int levelIndex)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelIndex);

        while (!loadOperation.isDone)
        {
            yield return null;
        }
    }

    IEnumerator DelayedVideo()
    {
        yield return new WaitForSeconds(delayTime);
        videoAnimator.Play("Intro_Cutscene_Fade_In");

        video.Play();
    }
}



