using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillTreeLeveling : MonoBehaviour
{
    

    public GameObject SkillTree;

    public GameObject Text;

    private ControlsforPlayer playerControls;
    bool _canOpenSkillTree;
    public static bool OpenSkilltree;

    private void Start()
    {
        playerControls = new ControlsforPlayer();
        if(Text !=null)
            Text.SetActive(false);
        playerControls.Enable();
        OpenSkilltree = false;
    }
    private void OnTriggerEnter(Collider other) // The skill tree ui pops up
    {
        if(other.gameObject.tag=="LevelUp")
        {
            Text.SetActive(true);
            _canOpenSkillTree=true;
            
        }
    }
   
    private void Update()
    {
        if(_canOpenSkillTree && playerControls.Actions.Interact.IsPressed())
            OpenSkillTree();
            
    }
    private void OpenSkillTree()
    {
        if(PauseMenu.GameIsPaused == false)
        {
            OpenSkilltree = true;
            Text.SetActive(false);
            SkillTree.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    private void OnTriggerExit(Collider other) // The skill tree ui disappears
    {
        if (other.gameObject.tag == "LevelUp")
        {
            Text.SetActive(false);
            _canOpenSkillTree = false;
            
        }
    }
    public void BackButton()
    {
        SkillTree.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        OpenSkilltree = false;
    }

}   
