using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineHandler : MonoBehaviour
{
	private GameObject target;
	private LineRenderer lineRenderer;
	
	private void Start()
	{
		lineRenderer = this.transform.GetComponent<LineRenderer>();
	}
	
	public void targetRep(GameObject destination)
	{
		target = destination;
	}

	private void LateUpdate()
	{
		lineRenderer.SetPosition(0, this.transform.position);
		lineRenderer.SetPosition(1, target.transform.position);
	}
}
