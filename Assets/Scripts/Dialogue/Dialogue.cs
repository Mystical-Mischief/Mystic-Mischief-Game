using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//set this up for all the characters who talk
[Serializable]
public class Character
{
    public string CharacterName;
    public GameObject CharacterObject;
}
//set this up for all the lines of dialogue the character will say
[Serializable]
public class DialogueLines
{
    public string Line;
    public int TalkingCharacter;
    public Vector3 CameraPosition;
}
//this holds all the dialogue interactions between the characters
[Serializable]
public class AllDialogue
{
    public DialogueLines[] Interaction;
}
public class Dialogue : MonoBehaviour
{
    public AudioSource audioSource;
    public Camera mainCamera;
    public TextMeshProUGUI textComponent;
    public bool DialogueIsRandom;
    public GameObject reward;
    public GameObject deactivate;
    public Character[] Characters;
    public AllDialogue[] AllDialogues;
    [Header("Speed of text for each letter goes here. Use decimals")]
    public float textSpeed;
    
    private int index;
    private int convoNumber;
    public MonoBehaviour ratAI;
    ControlsforPlayer controls;

    void Start()
    {
        controls = new ControlsforPlayer();
        controls.Enable();
    }
    void OnEnable()
    {
        textComponent.text = string.Empty;
        StartDialogue();
        if (ratAI != null)
            ratAI.enabled = false;
    }
    private void OnDisable()
    {
        if (ratAI != null)
            ratAI.enabled = true;
    }

    void Update()
    {
        //if you press the interact button run the dialogue 
        if (controls.Actions.Interact.WasPerformedThisFrame())
        {
           
            DialogueLines[] currentConversation = AllDialogues[convoNumber-1].Interaction;
            //shows the talking character sprite as well as the line they are saying
            string currentDialogue = $"{Characters[currentConversation[index].TalkingCharacter-1].CharacterName}: {currentConversation[index].Line}";
            if (textComponent.text == currentDialogue)
            {
                audioSource.Play();
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
        audioSource.Play();
        index = 0;
        GetComponent<SphereCollider>().enabled = false;
        mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
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
        updateCamera(currentConversation);
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
    void updateCamera(DialogueLines[] currentConversation)
    {
        mainCamera.transform.position = Characters[currentConversation[index].TalkingCharacter - 1].CharacterObject.transform.position + currentConversation[index].CameraPosition;
        mainCamera.transform.LookAt(Characters[currentConversation[index].TalkingCharacter - 1].CharacterObject.transform.position);
    }
    //goes to the next line by clearing the current line and typing the next line
    void NextLine(DialogueLines[] currentConversation)
    {
        if (index < currentConversation.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            updateCamera(currentConversation);
            StartCoroutine(TypeLine(currentConversation));
        }
        else
        {
            mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = true;
            GetComponent<ActivateDialogue>().dialogueCanvas.SetActive(false);
            StartCoroutine(delayDialogueBox());
            this.enabled = false;
            if (convoNumber == AllDialogues.Length)
            {
                if(reward != null)
                {
                    reward.SetActive(true);
                }
                if(deactivate != null)
                {
                    deactivate.SetActive(false);
                }
            }
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().canMove = true;
        }
    }
    //delays the dialogue so you can walk away from it without activating it again by accident. 
    IEnumerator delayDialogueBox()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<SphereCollider>().enabled = true;
    }
}
