using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandleModelClick : MonoBehaviour 
{
    public GameObject objectRep;
    private GameObject textHolder, showing = null;
    private TextMeshProUGUI textMesh;
    private RectTransform rt;
    private Dictionary<string, GameObject> objectReps;
    private HandleInteractions interactHandlerScript;

    private void Start() 
    {
		objectReps = new Dictionary<string, GameObject>();
        
		rt = this.transform.GetComponent<RectTransform>();
        interactHandlerScript = this.transform.GetComponent<HandleInteractions>();
    }

    public void getRep(string modelName) 
    {
        // Checks if there is an existing rep for the selected model
        if (!objectReps.ContainsKey(modelName))
            createRep(modelName);

        // Displays the rep and all of its interactions
        showRep(modelName);
        interactHandlerScript.toggleInteractions(true);
    }

    private void createRep(string modelName) 
    {
        // Make a new rep and update its name and displayed text
        GameObject newRep = Instantiate(objectRep, this.transform);
        newRep.name = modelName.Substring(0, modelName.IndexOf(" ")) + "Representation";
        textHolder = newRep.transform.GetChild(0).gameObject;
        textMesh = textHolder.GetComponent<TextMeshProUGUI>();
        textMesh.text = modelName.Substring(0, modelName.IndexOf(" "));
        
        // Move the new rep to an initial position
        newRep.transform.localPosition = new Vector3((rt.sizeDelta.x * rt.localScale.x)/ 4, 0, 0);
        
        // Make the new rep invisible; let the showRep() handle display functionality
        newRep.SetActive(false);
        
        // Add the new rep to the dictionary so it can be searched
        objectReps[modelName] = newRep;
    }

    private void showRep(string modelName) 
    {
        // Hides the current rep and all of its interacts
        if (showing != null)
        {
            showing.SetActive(false);
            interactHandlerScript.toggleInteractions(false);
        }
        
        // Make the selected rep visible and update which rep is being shown
        showing = objectReps[modelName];
        objectReps[modelName].SetActive(true);
    }
    
    public GameObject getShowing() 
    {
        return showing; // the gameobject of the currently displayed rep
    }
}
