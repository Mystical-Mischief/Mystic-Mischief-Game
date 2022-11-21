using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image Health;
    public float CurrentHealth;
    private float MaxHealth;
    ThirdPersonController Player;

    private void Start()
    {
        Health = GetComponent<Image>();
        Player = FindObjectOfType<ThirdPersonController>();
        MaxHealth = Player.maxHealth;
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        CurrentHealth = Player.currentHealth; 
        Health.fillAmount = CurrentHealth / MaxHealth; //updates the amount of health the player has in the UI

    }
}
