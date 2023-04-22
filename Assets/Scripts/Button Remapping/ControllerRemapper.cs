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
    public Sprite[] controllerImages;
    public Sprite defaultImage;

    [SerializeField] private InputActionAsset actions;
    [SerializeField] private InputActionReference[] controls;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    private void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        if(PlayerPrefs.GetString("rebinds") == null)
        {
            PlayerPrefs.SetString("rebinds", string.Empty);
        }   
        else
        {
            if(pc != null)
                pc.UpdateControls();
            foreach(InputActionReference IAR in controls)
            {
                IAR.asset.LoadBindingOverridesFromJson(PlayerPrefs.GetString("rebinds"));
            }
        }
        int x = 0;
        foreach(GameObject button in ControllerButtons)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = InputControlPath.ToHumanReadableString(
            controls[x].action.bindings[1].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
            updateButtonIcons(button, x);
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
        if(pc != null)
            pc.UpdateControls();
    }
    public void ButtonCtrl(int buttonNumber)
    {
        ControllerButtons[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = "...";
        ControllerButtons[buttonNumber].GetComponent<Image>().sprite = null;

        controls[buttonNumber].action.Disable();

        rebindingOperation = controls[buttonNumber].action.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>/press")
            .WithControlsExcluding("<Pointer>/position")
            .WithControlsExcluding("<Keyboard>")
            .WithTargetBinding(1)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => rebindCompleteCtrl(buttonNumber, 1))
            .Start();
        if (pc != null)
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

        updateButtonIcons(ControllerButtons[buttonNumber], buttonNumber);
        /*ControllerButtons[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = InputControlPath.ToHumanReadableString(
            controls[buttonNumber].action.bindings[binding].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);*/
        if (pc != null)
            pc.UpdateControls();
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
        if (pc != null)
            pc.UpdateControls();
    }
    private void updateButtonIcons(GameObject button ,int x)
    {
        int buttonNum = 0;
        bool updateImage = true;
        switch (InputControlPath.ToHumanReadableString(controls[x].action.bindings[1].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice))
        {
            case "Button North":
                buttonNum = 0;
                break;
            case "Button East":
                buttonNum = 1;
                break;
            case "Button South":
                buttonNum = 2;
                break;
            case "Button West":
                buttonNum = 3;
                break;
            case "Left Shoulder":
                buttonNum = 4;
                break;
            case "Right Shoulder":
                buttonNum = 5;
                break;
            case "Left Trigger":
                buttonNum = 6;
                break;
            case "Right Trigger":
                buttonNum = 7;
                break;
            default:
                updateImage = false;
                break;
        }
        if (updateImage)
        {
            button.GetComponent<Image>().sprite = controllerImages[buttonNum];
            button.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
        }
        else
        {
            button.GetComponent<Image>().sprite = defaultImage;
            button.GetComponentInChildren<TextMeshProUGUI>().text = InputControlPath.ToHumanReadableString(
            controls[x].action.bindings[1].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        }
    }
}
