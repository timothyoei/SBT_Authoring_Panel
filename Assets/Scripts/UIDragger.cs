using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using Microsoft.MixedReality.Toolkit.Input;

// TODO: Sometimes the rep, when dragged off screen, will return to the center of the canvas
// rather than its previous position

// TODO: Stop reps from overlapping with active reps

// TODO 2/25/21: Modify inBounds to work for all rotations of canvas
public class UIDragger : MonoBehaviour, IMixedRealityPointerHandler
{
    private Vector3[] parentCorners = new Vector3[4], corners = new Vector3[4];
    private Vector3 prevPos;
    private float initX;
    private RectTransform rt, parentRt;
    private List<Connection> connections;

    private void Start()
    {
        // Get the UI transform equivalents needed for position analysis
        rt = GetComponent<RectTransform>();
        parentRt = this.transform.parent.GetComponent<RectTransform>();
        
        // Set the last valid position to the initial position
        prevPos = transform.localPosition;
        
        // Get the parent's corner positions in world coordinates
        parentRt.GetWorldCorners(parentCorners);

        initX = parentCorners[0].x;
    }

    public bool inBounds() 
    {
        // Access the rep's new corner positions in world coordinates
        rt.GetWorldCorners(corners);
        
        // Check if each corner is within the canvas
        for (var i = 0; i < 4; i++) 
        {
            if (corners[0].z < parentCorners[0].z || corners[2].z > parentCorners[2].z)
                return false;
            if (corners[0].y < parentCorners[0].y || corners[2].y > parentCorners[2].y)
                return false;
        }
        return true;
    }
    public void OnPointerClicked(MixedRealityPointerEventData data) {}

    public void OnPointerDown(MixedRealityPointerEventData data) {}

    public void OnPointerDragged(MixedRealityPointerEventData data)
    {
        // Ensures that the new position has the same x-coordinate as the canvas
        // prevents accidental "englargement"
        transform.position = new Vector3(initX, data.Pointer.Position.y, data.Pointer.Position.z);
    }

    public void OnPointerUp(MixedRealityPointerEventData data)
    {
        if (!inBounds())
            // Move the rep back to its last valid position
            transform.localPosition = prevPos;
        else
            // Update the last valid position
            prevPos = transform.localPosition;
    }
}
