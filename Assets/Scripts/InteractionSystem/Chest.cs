using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public Quest quest;
    public string questToActivate;
    private bool questAccepted;
    [SerializeField] private string _prompt;

    public string InteractionPrompt => _prompt; 

    public bool Interact(Interactor interactor)
    {
        Debug.Log( "opening Chest!");
        if(quest != null && questAccepted)
        {
            quest.UpdateQuest();
        }
        if (quest != null && questToActivate != string.Empty && !questAccepted)
        {
            questAccepted = true;
            quest.ActivateQuest(questToActivate);
        }
        return true;
    }
}
