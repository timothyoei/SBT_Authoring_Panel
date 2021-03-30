using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine.UI;

public class DisplayInteractions : MonoBehaviour, IMixedRealityPointerHandler
{
    public HandleInteractions interactHandlerScript;
    private Text textMsg;
    private bool showing = false;

    private void Start()
    {   
        textMsg = this.transform.GetChild(0).gameObject.GetComponent<Text>();
    }

    public void OnPointerClicked(MixedRealityPointerEventData data)
    {
        // Display or hide all interactions of the current rep and
        // change this button's text accordingly
        interactHandlerScript.toggleInteractions(!showing);
        showing = !showing;
        if (showing)
            textMsg.text = "Hide Interactions";
        else
            textMsg.text = "Show Interactions";
    }

    public void OnPointerDown(MixedRealityPointerEventData data) {}

    public void OnPointerDragged(MixedRealityPointerEventData data) {}

    public void OnPointerUp(MixedRealityPointerEventData data) {}
}
