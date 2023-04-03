using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image Health;
    public Image[] healthPoints;
    public Image[] faces;

    public float CurrentHealth;
    private float MaxHealth;

    PlayerController Player;

    private void Start()
    {
        Health = GetComponent<Image>();
        Player = FindObjectOfType<PlayerController>();
        MaxHealth = Player.maxHealth;
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        CurrentHealth = Player.currentHealth; 
        Health.fillAmount = CurrentHealth / MaxHealth; //updates the amount of health the player has in the UI

        for (int i = 0; i < healthPoints.Length; i++)
        {
            healthPoints[i].enabled = !DisplayHealthPoint(CurrentHealth, i);
        }

        if(CurrentHealth == 4)
        {
            faces[3].gameObject.SetActive(false);
            faces[0].gameObject.SetActive(true);
        }
        if (CurrentHealth == 3)
        {
            faces[0].gameObject.SetActive(false);
            faces[1].gameObject.SetActive(true);
        }
        if (CurrentHealth == 2)
        {
            faces[1].gameObject.SetActive(false);
            faces[2].gameObject.SetActive(true);
        }
        if (CurrentHealth == 1)
        {
            faces[2].gameObject.SetActive(false);
            faces[3].gameObject.SetActive(true);
        }
    }

    bool DisplayHealthPoint(float health, int pointNumber)
    {
        return (pointNumber >= health);
    }
}
