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
    public bool dissapear;

    public string InteractionPrompt => _prompt; 

    public virtual bool Interact(Interactor interactor)
    {

        if(quest != null && quest.activeQuest.questName == questToFinish)
        {
            questToFinish = null;
            quest.NextQuest();
        }
        if (quest != null && questToActivate != string.Empty && !questAccepted)
        {
            print(questToActivate);
            quest.ActivateQuest(questToActivate);
            quest.UpdateQuest();
            questAccepted = true;
        }
        if (dissapear)
        {
            gameObject.SetActive(false);
        }
        return true;
    }
}
