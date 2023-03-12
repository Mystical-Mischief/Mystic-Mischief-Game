using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class ControllerRemapper : MonoBehaviour
{
    public GameObject[] ControllerButtons;
    public GameObject[] KeyboardButtons;
    public InputActionAsset controlForPlayer;

    [SerializeField] private InputActionReference[] controls;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    public void ButtonCtrl()
    {
        ControllerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Waiting for input...";

        rebindingOperation = controls[0].action.PerformInteractiveRebinding()
            .WithExpectedControlType("Mouse").OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => rebindComplete())
            .Start();
    }
    private void rebindComplete() 
    {
        ControllerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = InputControlPath.ToHumanReadableString(controls[0].action.bindings[0].effectivePath,InputControlPath.HumanReadableStringOptions.OmitDevice);
        rebindingOperation.Dispose();
    }
}
