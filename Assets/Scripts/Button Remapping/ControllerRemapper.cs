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
    private PlayerController pc;

    [SerializeField] private InputActionAsset actions;
    [SerializeField] private InputActionReference[] controls;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    private void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        if(PlayerPrefs.GetString("rebinds") == null)
            PlayerPrefs.SetString("rebinds", string.Empty);
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
            .OnComplete(operation => rebindCompleteKeyB(buttonNumber, 0))
            .Start();
        pc.UpdateControls();
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
            .OnComplete(operation => rebindCompleteCtrl(buttonNumber, 1))
            .Start();
        pc.UpdateControls();
    }
    private void rebindCompleteCtrl(int buttonNumber, int binding)
    {
        rebindingOperation.Dispose();
        controls[buttonNumber].action.Enable();
        InputBinding inputBinding = controls[buttonNumber].action.bindings[binding];
        actions.FindAction(controls[buttonNumber].action.name).ApplyBindingOverride(binding, inputBinding);
        var rebinds = actions.SaveBindingOverridesAsJson();

        PlayerPrefs.SetString("rebinds", rebinds);

        ControllerButtons[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = InputControlPath.ToHumanReadableString(
            controls[buttonNumber].action.bindings[binding].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        FindObjectOfType<PlayerController>().UpdateControls();
    }
    private void rebindCompleteKeyB(int buttonNumber, int binding)
    {
        rebindingOperation.Dispose();
        controls[buttonNumber].action.Enable();
        InputBinding inputBinding = controls[buttonNumber].action.bindings[binding];
        actions.FindAction(controls[buttonNumber].action.name).ApplyBindingOverride(binding, inputBinding);
        var rebinds = actions.SaveBindingOverridesAsJson();

        PlayerPrefs.SetString("rebinds", rebinds);

        KeyboardButtons[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = InputControlPath.ToHumanReadableString(
            controls[buttonNumber].action.bindings[binding].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        FindObjectOfType<PlayerController>().UpdateControls();
    }
}
