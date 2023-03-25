using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneLeave : MonoBehaviour
{
    public string sceneToLoad;
    private void OnTriggerExit(Collider other)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
