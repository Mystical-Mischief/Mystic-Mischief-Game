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

    [SerializeField] private InputActionReference[] controls;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    public void ButtonCtrl(int buttonNumber)
    {
        ControllerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "...";

        controls[buttonNumber].action.Disable();

        rebindingOperation = controls[buttonNumber].action.PerformInteractiveRebinding(0)
            .WithControlsHavingToMatchPath("<Keyboard>")
            .WithBindingGroup("Keyboard")
            .OnComplete(operation => rebindComplete(buttonNumber))
            .Start();
    }
    private void rebindComplete(int buttonNumber) 
    {
        rebindingOperation.Dispose();
        controls[buttonNumber].action.Enable();

        ControllerButtons[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = InputControlPath.ToHumanReadableString(controls[buttonNumber].action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
    }
}
