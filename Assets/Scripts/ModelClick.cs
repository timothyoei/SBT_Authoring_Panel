using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Microsoft.MixedReality.Toolkit.Input;

public class ModelClick : MonoBehaviour, IMixedRealityPointerHandler 
{
    public GameObject content;
    private HandleReps handleRepsScript;

    private void Start() 
    {
        handleRepsScript = content.transform.GetComponent<HandleReps>();
    }

    public void OnPointerClicked(MixedRealityPointerEventData data)
    {
        // Display the model's rep and all of its interactions, if any, or
        // create a new rep if one does not already exist
        handleRepsScript.getRep(this.name);
    }

    public void OnPointerDown(MixedRealityPointerEventData data) {}

    public void OnPointerDragged(MixedRealityPointerEventData data) {}

    public void OnPointerUp(MixedRealityPointerEventData data) {}
}
