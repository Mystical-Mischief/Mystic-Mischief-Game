using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[Serializable]
public class Character
{
    public string CharacterName;
    public Vector3 CameraPosition;
    public Vector3 CameraRotation;
}
[Serializable]
public class DialogueLines
{
    public string Line;
    public int TalkingCharacter;
}
[Serializable]
public class AllDialogue
{
    public DialogueLines[] Interaction;
}
public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public bool DialogueIsRandom;
    public Character[] Characters;
    public AllDialogue[] AllDialogues;
    [Header("Speed of text for each letter goes here. Use decimals")]
    public float textSpeed;
    
    private int index;
    private int convoNumber;

    // Start is called before the first frame update
    void OnEnable()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        //if you press the interact button run the dialogue 
        if (Input.GetMouseButtonDown(0))
        {
            DialogueLines[] currentConversation = AllDialogues[convoNumber-1].Interaction;
            //shows the talking character sprite as well as the line they are saying
            string currentDialogue = $"{Characters[currentConversation[index].TalkingCharacter-1].CharacterName}: {currentConversation[index].Line}";
            if (textComponent.text == currentDialogue)
            {
                NextLine(currentConversation);
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = currentDialogue;
            }
        }
    }
    //choses the proper dialogue option out of the list. if its random it will choose a random option. if its not then it will go to the next conversation listed
    void StartDialogue()
    {
        index = 0;
        if (DialogueIsRandom)
        {
            System.Random rnd = new System.Random();
            convoNumber = rnd.Next(1, AllDialogues.Length+1);
        }
        else
        {
            convoNumber++;
            if(convoNumber > AllDialogues.Length)
            {
                convoNumber = 1;
            }
        }
        DialogueLines[] currentConversation = AllDialogues[convoNumber - 1].Interaction;
        //characterImage.sprite = Characters[currentConversation[index].TalkingCharacter - 1].CharacterSprite;
        StartCoroutine(TypeLine(currentConversation));
    }
    //types the lines letter by letter to look nice
    IEnumerator TypeLine(DialogueLines[] currentConversation)
    {
        textComponent.text = $"{Characters[currentConversation[index].TalkingCharacter - 1].CharacterName}: ";
        //Type each character 1 by 1
        foreach (char c in currentConversation[index].Line)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    //goes to the next line by clearing the current line and typing the next line
    void NextLine(DialogueLines[] currentConversation)
    {
        if (index < currentConversation.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            //characterImage.sprite = Characters[currentConversation[index].TalkingCharacter - 1].CharacterSprite;
            StartCoroutine(TypeLine(currentConversation));
        }
        else
        {
            GetComponent<ActivateDialogue>().dialogueCanvas.SetActive(false);
            StartCoroutine(delayDialogueBox());
            this.enabled = false;
        }
    }
    //delays the dialogue so you can walk away from it without activating it again by accident. 
    IEnumerator delayDialogueBox()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
