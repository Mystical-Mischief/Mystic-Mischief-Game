using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompleter : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("Completed", 1);
    }
}
