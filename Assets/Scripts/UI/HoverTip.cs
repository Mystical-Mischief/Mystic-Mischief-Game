using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tipToShow;
    private float timeToWait = 0.5f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Detects if the mouse is hovering over the game object it is attached to
        Debug.Log("Hovered");
        ShowMessage();
        StopAllCoroutines();
        StartCoroutine(StartTimer());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Detects if the mouse has left the game object it is attached to
        Debug.Log("Exited");
        StopAllCoroutines();
        HoverTipManager.OnMouseLoseFocus();
    }

    private void ShowMessage()
    {
        HoverTipManager.OnMouseHover(tipToShow, Input.mousePosition);
    }

    private IEnumerator StartTimer()
    {
        //waits for half a second before showing the tip
        yield return new WaitForSeconds(timeToWait);

        ShowMessage();
    }
}
