using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneLeave : MonoBehaviour
{
    public string sceneToLoad;
    public ASyncLoadManager asyncLoadManager;

    private void OnEnable()
    {
        transform.position = FindObjectOfType<PlayerController>().gameObject.transform.position;
    }
    private void OnTriggerExit(Collider other)
    {
        asyncLoadManager.GoToLevel(sceneToLoad);
    }
}
