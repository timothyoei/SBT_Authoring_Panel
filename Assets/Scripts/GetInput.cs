using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetInput : MonoBehaviour
{
    public HandleInteractions canvasScript;
    private InputField iField;
    private ShowInput createInteractionScript;
    private string interactName, defaultString = "";

	private void Start()
    {
		iField = this.transform.GetComponent<InputField>();
		iField.text = defaultString;
    }

    public void getText() 
    {
	    // Check for invalid interact names
        if (iField.text == defaultString)
            return;

        // Display the interact, if it exists, or create a new 
		// interact if the entered one does not already exist
        interactName = iField.text;
        canvasScript.getInteraction(interactName);
		
		// Reset text
		iField.text = defaultString;
    }
}
