using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleInteractions : MonoBehaviour
{
	public GameObject interactRep, inspectorRep, interactCreator;
	private Connection tempConnection;
	private ConnectionPoint tempConnectionPoint;
	private List<Connection> connections;
	private GameObject currentRep, prevShown = null;
	private HandleReps handleRepsScript;
	private ShowInput showInputScript;
	private RectTransform interactRT, inspectorRepRT, currentRepRT, rt;
	private Dictionary<GameObject, Dictionary<string, GameObject>> interactReps;
	private string modelText;

	private void Start()
	{
		interactReps = new Dictionary<GameObject, Dictionary<string, GameObject>>();
		inspectorRep = this.transform.GetChild(0).gameObject;
		inspectorRepRT = inspectorRep.GetComponent<RectTransform>();
		handleRepsScript = this.transform.GetComponent<HandleReps>();
		showInputScript = interactCreator.transform.GetComponent<ShowInput>();
	}

	public void getInteraction(string interactName)
	{
		// Get the rep that we want to add interactions
		// to and check if one exists
		currentRep = handleRepsScript.getShowing();
		if (currentRep == null)
			return;

		// Check if rep has any previous interactions
		if (!interactReps.ContainsKey(currentRep))
			// Delegate space for interactions with this rep
			interactReps[currentRep] = new Dictionary<string, GameObject>();

		// Check if the requested interact exists
		if (!interactReps[currentRep].ContainsKey(interactName))
			createInteraction(interactName);

		toggleInteractions(false);
		showInteract(interactName);

		// Reset the inputfield for new interact creation
		showInputScript.returnDefault();
	}

	private void createInteraction(string interactName)
	{
		// Setup interaction
		GameObject newInteract = Instantiate(interactRep, currentRep.transform.parent.transform);
		modelText = currentRep.transform.GetChild(0).gameObject.GetComponent<Text>().text;
		newInteract.name = interactName + " Interaction " + "(" + modelText + ")";
		newInteract.transform.GetChild(0).gameObject.GetComponent<Text>().text = interactName;
		newInteract.transform.localPosition = new Vector3(0, 0, 0);
		newInteract.SetActive(false);

		// Get the RectTransforms of the new connections
		currentRepRT = currentRep.GetComponent<RectTransform>();
		interactRT = newInteract.GetComponent<RectTransform>();

		// Create connections
		ConnectionManager.CreateConnection(interactRT, inspectorRepRT);
		ConnectionManager.CreateConnection(interactRT, currentRepRT);

		// Set up inspector rep connection options
		tempConnection = ConnectionManager.FindConnection(interactRT, inspectorRepRT);
		tempConnectionPoint = tempConnection.points[0];
		tempConnectionPoint.direction = ConnectionPoint.ConnectionDirection.West;
		tempConnectionPoint.weight = 0.65f;
		tempConnectionPoint = tempConnection.points[1];
		tempConnectionPoint.direction = ConnectionPoint.ConnectionDirection.East;
		tempConnectionPoint.weight = 0.65f;

		// Set up current object rep connection options
		tempConnection = ConnectionManager.FindConnection(interactRT, currentRepRT);
		tempConnectionPoint = tempConnection.points[0];
		tempConnectionPoint.direction = ConnectionPoint.ConnectionDirection.East;
		tempConnectionPoint.weight = 0.65f;
		tempConnectionPoint = tempConnection.points[1];
		tempConnectionPoint.direction = ConnectionPoint.ConnectionDirection.West;
		tempConnectionPoint.weight = 0.65f;

		// Add that interact to the object's available interacts
		interactReps[currentRep][interactName] = newInteract;
	}

	private void showInteract(string interactName)
	{
		// Checks if an interact is being displayed; make it invisible
		if (prevShown != null)
		{
			prevShown.SetActive(false);
			toggleLines(prevShown, false);
		}

		// Make the selected interact visible and update which interact was last displayed
		prevShown = interactReps[currentRep][interactName];
		interactReps[currentRep][interactName].SetActive(true);
		toggleLines(prevShown, true);
	}

	public void toggleInteractions(bool active)
	{
		currentRep = handleRepsScript.getShowing();
		if (!interactReps.ContainsKey(currentRep))
			return;

		// Display or hide all of the current rep's interacts
		if (currentRep != null && interactReps[currentRep] != null)
			foreach (var entry in interactReps[currentRep])
			{
				entry.Value.SetActive(active);
				toggleLines(entry.Value, active);
			}
	}

	private void toggleLines(GameObject interact, bool active)
	{
		rt = interact.GetComponent<RectTransform>();
		connections = ConnectionManager.FindConnections(rt);
		foreach (Connection connection in connections)
			connection.gameObject.SetActive(active);
	}
}