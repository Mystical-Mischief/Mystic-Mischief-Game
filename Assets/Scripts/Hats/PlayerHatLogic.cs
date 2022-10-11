using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHatLogic : MonoBehaviour
{
    public GameObject[] hats;

    ControlsforPlayer controls;
    private bool switchHat;
    private int currentHatNum;
    private GameObject currentHatObject;
    private bool canSwitchHat = true;

    void Start()
    {
        controls = new ControlsforPlayer();
        controls.Enable();
        currentHatNum = 0;
        foreach(GameObject hat in hats)
        {
            hat.SetActive(false);
        }
        hats[currentHatNum].SetActive(true);
    }

    void Update()
    {
        switchHat = controls.Actions.SwitchHat.IsPressed();
        if (switchHat && canSwitchHat)
        {
            canSwitchHat = false;
            StartCoroutine(ChangeHatCooldown());
        }
    }
    void ChangeHat()
    {
        hats[currentHatNum].SetActive(false);
        currentHatNum++;
        if(currentHatNum == hats.Length)
        {
            currentHatNum = 0;
        }
        hats[currentHatNum].SetActive(true);
    }
    IEnumerator ChangeHatCooldown()
    {
        ChangeHat();
        yield return new WaitForSeconds(0.5f);
        canSwitchHat = true;
    }
}
