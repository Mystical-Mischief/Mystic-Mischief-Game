using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHatLogic : MonoBehaviour
{
    public GameObject[] hats;
    public GameObject[] godModeHats;
    public Sprite[] hatImages;
    public Image hatUIImage;
    GameObject[] playerHats;

    ControlsforPlayer controls;
    private bool switchHat;
    private int currentHatNum;
    public GameObject currentHatObject;
    private bool canSwitchHat = true;
    public bool dontAutoSwitch;

    private PlayerController player;

    void Start()
    {
        //sets up all the boring stuff like controls what hat the player is using and disables all but the first hat
        controls = new ControlsforPlayer();
        controls.Enable();
        currentHatNum = 0;
        player = FindObjectOfType<PlayerController>();
        foreach(GameObject hat in hats)
        {
            hat.SetActive(false);
        }
        hats[currentHatNum].SetActive(true);
        currentHatObject = hats[currentHatNum];
        for (int i = 0; i < hats.Length; i++)
        {
            if (currentHatObject == hats[i])
            {
                hatUIImage.sprite = hatImages[i];
            }
        }
        playerHats = hats;
    }

    void Update()
    {
        if(player.godMode)
        {
            hats = godModeHats;
        }
        else
        {
            hats = playerHats;
        }
        if (hats[currentHatNum].name == "Empty" && dontAutoSwitch == false)
        {
            ChangeHat();
        }
        //if the player switches their hat run the hat change corotine so the players cant spam it
        switchHat = controls.Actions.SwitchHat.IsPressed();
        if (switchHat && canSwitchHat)
        {
            canSwitchHat = false;
            StartCoroutine(ChangeHatCooldown());
        }
    }
    //changes the hat to the next listed hat. if its the last hat in the list it goes to the first hat. 
    void ChangeHat()
    {
        hats[currentHatNum].SetActive(false);
        currentHatNum++;
        if(currentHatNum == hats.Length)
        {
            currentHatNum = 0;
        }
        hats[currentHatNum].SetActive(true);
        currentHatObject = hats[currentHatNum];
        for (int i = 0; i < hats.Length; i++)
        {
            if (currentHatObject == hats[i])
            {
                hatUIImage.sprite = hatImages[i];
            }
        }
    }
    //changes hat and starts a short cooldown so hat changing is easier to use
    IEnumerator ChangeHatCooldown()
    {
        ChangeHat();
        yield return new WaitForSeconds(0.5f);
        canSwitchHat = true;
    }

    public void SaveHats()
    {
        SaveSystem.SaveHats(this);
    }
    public void LoadHats()
    {
        HatData data = SaveSystem.LoadHats(this);

        // hats = data.hats;
    }
}
