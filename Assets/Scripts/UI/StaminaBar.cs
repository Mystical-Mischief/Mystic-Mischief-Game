using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StaminaBar : MonoBehaviour
{
    private Image Stamina;
    public Image[] staminaPoints;

    private float CurrentStamina;
    private float MaxStamina;

    PlayerController Player;

    private void Start()
    {
        Player = FindObjectOfType<PlayerController>();
        CurrentStamina = MaxStamina;
    }

    private void Update()
    {
        CurrentStamina = Player.stamina;

        if(MaxStamina != Player.maxStamina)
        {
            MaxStamina = Player.maxStamina;
        }
        //Stamina.fillAmount = CurrentStamina/MaxStamina; //updates the amount of stamina the player has in the UI

        for (int i = 0; i < staminaPoints.Length; i++)
        {
            staminaPoints[i].enabled = !DisplayStaminaPoint(CurrentStamina, i * (MaxStamina / staminaPoints.Length));
        }

    }
    bool DisplayStaminaPoint(float stamina, float pointNumber)
    {
        return (pointNumber >= stamina);
    }
}
