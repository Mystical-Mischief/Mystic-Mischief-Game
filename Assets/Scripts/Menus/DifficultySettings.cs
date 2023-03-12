using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySettings : MonoBehaviour
{
    [SerializeField]
    private Toggle EnemySpeedToggle;

    public static bool EnemySpeedDiff;

    [SerializeField]
    private Toggle DragonDMGToggle;

    public static bool DragonDMG;

    [SerializeField]
    private Toggle StaminaToggle;

    public static bool StaminaDiff;

    [SerializeField]
    private Toggle PlayerSpeedToggle;

    public static bool PlayerSpeedDiff;
    // Start is called before the first frame update
    void Start()
    {
        //EnemySpeedDiff = false;
        //DragonDMG = false;
        //StaminaDiff = false;
        //PlayerSpeedDiff = false;
        EnemySpeedToggle.isOn = EnemySpeedDiff;
        DragonDMGToggle.isOn = DragonDMG;
        StaminaToggle.isOn = StaminaDiff;
        PlayerSpeedToggle.isOn = PlayerSpeedDiff;
    }

    // Update is called once per frame
    void Update()
    {
        EnemySpeedDiff = EnemySpeedToggle.isOn;
        DragonDMG = DragonDMGToggle.isOn;
        StaminaDiff = StaminaToggle.isOn;
        PlayerSpeedDiff = PlayerSpeedToggle.isOn;
    }
}
