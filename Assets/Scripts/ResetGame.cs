using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    public void resetGame()
    {
        PlayerPrefs.SetInt("Completed", 0);
    }
    public void completeGame()
    {
        PlayerPrefs.SetInt("Completed", 1);
    }
}
