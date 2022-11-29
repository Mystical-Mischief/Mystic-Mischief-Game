using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public Quest quest;
    [SerializeField] private string _prompt;

    public string InteractionPrompt => _prompt; 

    public bool Interact(Interactor interactor)
    {
        Debug.Log( "opening Chest!");
        if(quest != null)
        {
            quest.UpdateQuest();
        } 
        return true;
    }
}
