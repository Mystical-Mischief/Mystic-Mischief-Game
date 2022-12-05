using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public Quest quest;
    public string questToFinish;
    public string questToActivate;
    private bool questAccepted;
    public bool finishQuest;
    [SerializeField] private string _prompt;

    public string InteractionPrompt => _prompt; 

    public bool Interact(Interactor interactor)
    {
        Debug.Log( "opening Chest!");
        if(quest != null && (questAccepted || questToActivate == string.Empty))
        {
            quest.UpdateQuest();
        }
        if (quest != null && questToActivate != string.Empty && !questAccepted)
        {
            quest.ActivateQuest(questToActivate);
            quest.UpdateQuest();
            questAccepted = true;
            gameObject.SetActive(false);
        }
        return true;
    }
}
