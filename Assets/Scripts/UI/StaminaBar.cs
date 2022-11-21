using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StaminaBar : MonoBehaviour
{
    private Image Stamina;
    public float CurrentStamina;
    private float MaxStamina = 6;
    ThirdPersonController Player;

    private void Start()
    {
        Stamina = GetComponent<Image>();
        Player = FindObjectOfType<ThirdPersonController>();
        CurrentStamina = MaxStamina;
    }

    private void Update()
    {
        CurrentStamina = Player.Stamina;
        Stamina.fillAmount = CurrentStamina/MaxStamina; //updates the amount of stamina the player has in the UI

    }
}
