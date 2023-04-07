using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Reputation : MonoBehaviour
{
    public string reputation;

    [SerializeField]
    private string[] reputationTitles;

    [SerializeField]
    private TextMeshProUGUI reputationText;

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateReputation((int)Treasure.treasureValue/reputationTitles.Length);
    }
    public void UpdateReputation(int index)
    {
        reputation = reputationTitles[index];
        reputationText.text = reputation;
    }
}
