using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StaminaBar : MonoBehaviour
{
    private Image Stamina;
    public Image[] staminaPoints;

    public float CurrentStamina = 4;
    private float MaxStamina = 4;

    PlayerController Player;

    private void Start()
    {
        Stamina = GetComponent<Image>();
        Player = FindObjectOfType<PlayerController>();
        CurrentStamina = MaxStamina;
    }

    private void Update()
    {
        CurrentStamina = Player.stamina;
        Stamina.fillAmount = CurrentStamina/MaxStamina; //updates the amount of stamina the player has in the UI

        for(int i = 0; i < staminaPoints.Length; i++)
        {
            staminaPoints[i].enabled = !DisplayStaminaPoint(CurrentStamina, i);
        }

    }

    bool DisplayStaminaPoint(float stamina, int pointNumber)
    {
        return (pointNumber >= stamina);
    }
}
