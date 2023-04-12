using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Reputation : MonoBehaviour
{
    public string reputation;
    public ParticleSystem ps;
    public bool levelUp;
    public float nextLevel;
    public float currentEXP;
    public int level;
    public float EXPraise;
    public int LastLevel;

    [SerializeField]
    private string[] reputationTitles;

    [SerializeField]
    private TextMeshProUGUI reputationText;
    public void Start()
    {
        // ps = GetComponentInChildren<ReputationLevelUp>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // UpdateReputation((int)Treasure.treasureValue/reputationTitles.Length);
        if (reputation != reputationTitles[LastLevel])
        {
        reputation = reputationTitles[level + 1];
        }
        currentEXP = Treasure.treasureValue;
        if (currentEXP >= nextLevel)
        {
            ps.Play(true);
            UpdateReputation();
        }

    }
    public void UpdateReputation()
    {
        level = level + 1;
        reputationText.text = reputation;
        nextLevel = nextLevel + EXPraise;
    }
}
