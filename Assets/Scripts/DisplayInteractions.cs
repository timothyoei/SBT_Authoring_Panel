using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine.UI;

// TODO 2/25/21: Update text when interaction is created
public class DisplayInteractions : MonoBehaviour, IMixedRealityPointerHandler
{
    private Text textMsg;
    private HandleInteractions interactHandlerScript;
    private bool showing = false;

    private void Start()
    {
        interactHandlerScript = this.transform.parent.GetComponent<HandleInteractions>();
        
        // Retrieve the TMP components needed to update the text fields
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
