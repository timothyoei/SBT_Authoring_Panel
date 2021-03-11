using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleZoom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ZoomIn() {

        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x - 100f, GetComponent<RectTransform>().sizeDelta.y - 100);
        foreach (RectTransform rect in GetComponentsInChildren<RectTransform>())
        {
            if (rect.gameObject.name != "Content")
                rect.localScale = new Vector3(rect.localScale.x + 0.1f, rect.localScale.y + 0.1f, rect.localScale.z + 0.1f);
        }

    }

    public void ZoomOut() {

        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x + 100f, GetComponent<RectTransform>().sizeDelta.y + 100);
        foreach (RectTransform rect in GetComponentsInChildren<RectTransform>())
        {
            if (rect.gameObject.name != "Content")
                rect.localScale = new Vector3(rect.localScale.x - 0.1f, rect.localScale.y - 0.1f, rect.localScale.z - 0.1f);
        }
        
    }
}
