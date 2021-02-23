using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandleInteractions : MonoBehaviour
{
	public GameObject interactRep, inspectorRep, createButton, linePrefab;
	private GameObject currentRep, textHolder, modelTextHolder, prevShown = null;
    private TextMeshProUGUI textMesh, modelTextMesh;
    private HandleModelClick handleModelClickScript;
    private ShowInput showInputScript;
    private LineHandler lineHandlerScript;
    private Dictionary<GameObject, Dictionary<string, GameObject>> interactReps;

    private void Start()
    {
	    interactReps = new Dictionary<GameObject, Dictionary<string, GameObject>>();

	    handleModelClickScript = this.transform.GetComponent<HandleModelClick>();
		showInputScript = createButton.transform.GetComponent<ShowInput>();
    }

    public void getInteraction(string name)
	{
        // Get the rep that we want to add interactions
        // to and check if one exists
        currentRep = handleModelClickScript.getShowing();
        if (currentRep == null)
	        return;

        // Check if rep has any previous interactions
        if (!interactReps.ContainsKey(currentRep)) 
            // Delegate space for interactions with this rep
            interactReps[currentRep] = new Dictionary<string, GameObject>();

        // Check if the requested interact exists
        if (!interactReps[currentRep].ContainsKey(name))
	        createInteraction(name);
		toggleInteractions(false);
        showInteract(name);
        
        // Reset the inputfield
        showInputScript.returnDefault();
	}

    private void createInteraction(string name) 
	{
        // Create a button to represent the interaction
        GameObject newInteract = Instantiate(interactRep, currentRep.transform.parent.transform);

		// Retrieve the TMP components needed to update the text fields
		modelTextHolder = currentRep.transform.GetChild(0).gameObject;
		modelTextMesh = modelTextHolder.GetComponent<TextMeshProUGUI>();

		// Update the name and text fields
        newInteract.name = name + " Interaction " + "(" + modelTextMesh.text + ")";
        textHolder = newInteract.transform.GetChild(0).gameObject;
        textMesh = textHolder.GetComponent<TextMeshProUGUI>();
        textMesh.text = name;

		// Make the new interact invisible; let the showInteract() handle display functionality
        newInteract.SetActive(false);

        // Move the interact to the center of the canvas
        newInteract.transform.localPosition = new Vector3(0, 0, 0);
        
        // Set up the new interact's lines
        // Create a new line ending at the current rep
        GameObject newLine = Instantiate(linePrefab, newInteract.transform);
        newLine.transform.localPosition = new Vector3(0, 0, 0);
        newLine.name = name + " Line (" + currentRep.name + ")";
        lineHandlerScript = newInteract.transform.GetChild(1).GetComponent<LineHandler>();
        lineHandlerScript.targetRep(currentRep);
        // Create a new line ending at the inspector rep
        lineHandlerScript = newInteract.GetComponent<LineHandler>();
        lineHandlerScript.targetRep(inspectorRep);

        // Add that interact to the object's available interacts
        interactReps[currentRep][name] = newInteract;
	}

    private void showInteract(string name) 
	{
		// Checks if an interact is being displayed; make it invisible
        if (prevShown != null) 
            prevShown.SetActive(false);

		// Make the selected interact visible and update which interact is being shown
        prevShown = interactReps[currentRep][name];
        interactReps[currentRep][name].SetActive(true);
    }

	public void toggleInteractions(bool active)
	{
		currentRep = handleModelClickScript.getShowing();
		if (!interactReps.ContainsKey(currentRep))
			return;

		// Display or hide all of the current rep's interacts
		if (currentRep != null && interactReps[currentRep] != null)
			foreach (var entry in interactReps[currentRep])
				entry.Value.SetActive(active);
	}
}