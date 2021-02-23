using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

// TODO: Sometimes the rep, when dragged off screen, will return to the center of the canvas
// rather than its previous position

// TODO: Stop reps from overlapping with active reps
public class RepDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3[] parentCorners = new Vector3[4], corners = new Vector3[4];
    private Vector3 prevPos;
    private RectTransform rt, parentRt;

    private void Start()
    {
        // Get the UI transform equivalents needed for position analysis
        rt = GetComponent<RectTransform>();
        parentRt = this.transform.parent.GetComponent<RectTransform>();
        
        // Set the last valid position to the initial position
        prevPos = transform.localPosition;
        
        // Get the parent's corner positions in world coordinates
        parentRt.GetWorldCorners(parentCorners);
    }

    public bool inBounds() 
    {
        // Access the rep's new corner positions in world coordinates
        rt.GetWorldCorners(corners);
        
        // Check if each corner is within the canvas
        for (var i = 0; i < 4; i++) 
        {
            if (corners[0].x < parentCorners[0].x || corners[2].x > parentCorners[2].x)
                return false;
            if (corners[0].y < parentCorners[0].y || corners[2].y > parentCorners[2].y)
                return false;
        }
        return true;
    }

    public void OnBeginDrag(PointerEventData data)
    {
        return;
    }

    public void OnDrag(PointerEventData data) 
    {
        // Ensures that the new position has the same z-coordinate as the canvas
        // prevents accidental "englargement"
        if (data.pointerCurrentRaycast.worldPosition.z == transform.position.z)
            transform.position = data.pointerCurrentRaycast.worldPosition;
    }

    public void OnEndDrag(PointerEventData data)
    {
        if (!inBounds())
            // Move the rep back to its last valid position
            transform.localPosition = prevPos;
        else
            // Update the last valid position
            prevPos = transform.localPosition;
    }
}
