using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public GameObject LevelSelectUI;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            LockCamera(false);
            LevelSelectUI.SetActive(true);
            other.gameObject.GetComponent<PlayerController>().canMove = false;
        }
    }
    public void LockCamera(bool _lock)
    {
        if (_lock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
