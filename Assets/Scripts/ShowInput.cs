using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine.UI;

public class ShowInput : MonoBehaviour, IMixedRealityPointerHandler 
{
    public GameObject inputField;
    private Text textHolder, inputTextHolder;
    private string defaultMsg = "Create Interaction", altMsg = "Return", inputDefaultMsg = "";

    private void Start()
    {
        // Get components needed to update text fields
        textHolder = this.transform.GetChild(0).gameObject.GetComponent<Text>();
        inputTextHolder = inputField.transform.GetChild(0).gameObject.GetComponent<Text>();
    }
    public void OnPointerClicked(MixedRealityPointerEventData data)
    {
        if (!inputField.activeSelf) // display inputField and update text for interact creation
        {
            inputField.SetActive(true);
            textHolder.text = altMsg;
        }
        else // hide inputField and update text for exiting interact creation
        {
            inputField.SetActive(false);
            textHolder.text = defaultMsg;
        }
    }

    public void OnPointerDown(MixedRealityPointerEventData data) {}

    public void OnPointerDragged(MixedRealityPointerEventData data) {}

    public void OnPointerUp(MixedRealityPointerEventData data) {}

    public void returnDefault()
    {
        inputField.SetActive(false);
        textHolder.text = defaultMsg;
        inputTextHolder.text = inputDefaultMsg;
    }
}
