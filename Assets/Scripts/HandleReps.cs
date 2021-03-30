using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleReps : MonoBehaviour
{
    public GameObject objectRep, inspectorRep, interactCreator, interactDisplayer;
    private GameObject showing = null;
    private RectTransform rt;
    private Dictionary<string, GameObject> objectReps;
    private HandleInteractions interactHandlerScript;
    private bool showingOptions = false;

    private void Start() 
    {
        objectReps = new Dictionary<string, GameObject>();
        rt = this.transform.GetComponent<RectTransform>();
        interactHandlerScript = this.transform.GetComponent<HandleInteractions>();

        // Create and setup inspector rep to define object rep interacts with
        inspectorRep = Instantiate(objectRep, this.transform);
        inspectorRep.SetActive(false);
        inspectorRep.transform.localPosition = new Vector3(-250, 0, 0);
        inspectorRep.name = "Inspector Representation";
        inspectorRep.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Inspector";
    }

    public void getRep(string modelName)
    {
        // Checks if there is an existing rep for the selected model
        if (!objectReps.ContainsKey(modelName))
            createRep(modelName);

        // Displays the rep and all of its interactions
        showRep(modelName);

        interactHandlerScript.toggleInteractions(true);

        if (!showingOptions)
            showOptions();
    }

    private void createRep(string modelName)
    {
        // Make a new rep and update its name and displayed text
        GameObject newRep = Instantiate(objectRep, this.transform);
        newRep.transform.localPosition = new Vector3(250, 0, 0);
        newRep.name = modelName + " Representation";
        newRep.transform.GetChild(0).gameObject.GetComponent<Text>().text = modelName;

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
        
        // Update which rep is being shown
        showing = objectReps[modelName];
        objectReps[modelName].SetActive(true);
    }
    
    private void showOptions()
    {
        inspectorRep.SetActive(true);
        interactCreator.SetActive(true);
        interactDisplayer.SetActive(true);
        showingOptions = true;
    }
    
    public GameObject getShowing() 
    {
        return showing;
    }
}
