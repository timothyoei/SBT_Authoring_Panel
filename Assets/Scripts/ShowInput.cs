using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

// TODO: When switching between reps, the input field's text does not update

public class ShowInput : MonoBehaviour, IPointerClickHandler
{
    public GameObject inputField;
    private GameObject textHolder;
    private TextMeshProUGUI textMesh;
    
    public void Start()
    {
        // Retrieve the TMP components needed to update the text fields
        textHolder = this.transform.GetChild(0).gameObject;
        textMesh = textHolder.GetComponent<TextMeshProUGUI>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!inputField.activeSelf) // display inputField
        {
            inputField.SetActive(true);
            textMesh.text = "Return";
        }
        else // hide inputField
        {
            inputField.SetActive(false);
            textMesh.text = "Create Interaction";
        }
    }

    public void returnDefault()
    {
        // Hide inputField after interaction is created
        inputField.SetActive(false);
        textMesh.text = "Create Interaction";
    }
}
