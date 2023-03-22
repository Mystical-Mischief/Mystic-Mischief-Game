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

    private void Start()
    {
        int x = 0;
        foreach(GameObject button in ControllerButtons)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = InputControlPath.ToHumanReadableString(
            controls[x].action.bindings[1].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
            x++;
        }
        x = 0;
        foreach (GameObject button2 in KeyboardButtons)
        {
            button2.GetComponentInChildren<TextMeshProUGUI>().text = InputControlPath.ToHumanReadableString(
            controls[x].action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
            x++;
        }

    }
    public void ButtonKeyB(int buttonNumber)
    {
        KeyboardButtons[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = "...";

        controls[buttonNumber].action.Disable();

        rebindingOperation = controls[buttonNumber].action.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>/press")
            .WithControlsExcluding("<Pointer>/position")
            .WithControlsExcluding("<Gamepad>")
            .WithTargetBinding(0)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => rebindComplete(buttonNumber, 0))
            .Start();
    }
    public void ButtonCtrl(int buttonNumber)
    {
        ControllerButtons[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = "...";

        controls[buttonNumber].action.Disable();

        rebindingOperation = controls[buttonNumber].action.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>/press")
            .WithControlsExcluding("<Pointer>/position")
            .WithControlsExcluding("<Keyboard>")
            .WithTargetBinding(1)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => rebindComplete(buttonNumber, 1))
            .Start();
    }
    private void rebindComplete(int buttonNumber, int binding)
    {
        rebindingOperation.Dispose();
        controls[buttonNumber].action.Enable();
        print("this is working!!!");

        ControllerButtons[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = InputControlPath.ToHumanReadableString(
            controls[buttonNumber].action.bindings[binding].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);

    }
}
