using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public GameObject LevelSelectUI;
    public EventSystem eventSystem;
    [SerializeField]
    Button first;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            first.Select();
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
            
            first.Select();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            
        }
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
