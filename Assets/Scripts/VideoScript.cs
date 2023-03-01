using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour
{
    VideoPlayer video;

    void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += CheckOver;
        
    }


    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        //the scene that you want to load after the video has ended.
        StartCoroutine(LoadLevelASync(SceneManager.GetActiveScene().buildIndex + 1));
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
