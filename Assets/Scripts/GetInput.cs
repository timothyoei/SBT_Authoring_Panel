using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetInput : MonoBehaviour 
{
    private TMP_InputField iField;
    private HandleInteractions canvasScript;
    private ShowInput createInteractionScript;
	private string interactName, defaultString = "ENTER INTERACTION NAME";

	private void Start()
    {
        canvasScript = this.transform.parent.GetComponent<HandleInteractions>();

		// Retrieve TMP components and set default displayed string
        iField = this.transform.GetComponent<TMP_InputField>();
        iField.text = defaultString;
    }

    public void getText() 
    {
		// Check for invalid interact names
        if (iField.text == defaultString || iField.text == "")
            return;

		// Display the interact, if it exists, or create a new 
		// interact if the entered one does not already exist
        interactName = iField.text;
        canvasScript.getInteraction(interactName);
		
		// Reset text
		iField.text = defaultString;
    }
}
