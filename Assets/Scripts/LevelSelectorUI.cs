using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectorUI : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        PauseMenu.GameIsPaused = true;
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void OnDisable()
    {
        PauseMenu.GameIsPaused = false;
        Time.timeScale = 1f;
    }
}
