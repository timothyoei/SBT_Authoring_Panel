using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Personal TODO: figure out why tabbing is so weird in editor

public class ModelClick : MonoBehaviour, IPointerClickHandler 
{
    public GameObject canvas;
    private HandleModelClick canvasScript;

    private void Start() 
    {
        canvasScript = canvas.transform.GetComponent<HandleModelClick>();
    }

    public void OnPointerClick(PointerEventData data) 
    {
        // Display the model's rep and all of its interactions, if any, or
        // create a new rep if one does not already exist
        canvasScript.getRep(this.name);
    }

}
