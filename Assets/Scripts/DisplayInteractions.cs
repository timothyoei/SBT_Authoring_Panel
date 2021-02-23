using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DisplayInteractions : MonoBehaviour, IPointerClickHandler
{
    private GameObject textHolder;
    private TextMeshProUGUI textMesh;
    private HandleInteractions interactHandlerScript;
    private bool showing = false;

    private void Start()
    {
        interactHandlerScript = this.transform.parent.GetComponent<HandleInteractions>();
        
        // Retrieve the TMP components needed to update the text fields
        textHolder = this.transform.GetChild(0).gameObject;
        textMesh = textHolder.GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerClick(PointerEventData data)
    {
        // Display or hide all interactions of the current rep and
        // change this button's text accordingly
        interactHandlerScript.toggleInteractions(!showing);
        showing = !showing;
        if (showing)
            textMesh.text = "Hide Interactions";
        else
            textMesh.text = "Show Interactions";
    }
}
