using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(LineRenderer))]
public class PathCreationController : MonoBehaviour
{

	void Start ()
	{
		line = GetComponent<LineRenderer> ();
		line.enabled = false;
	}

	void Update ()
	{
		if (_makeLine) {
			line.enabled = true;
			line.SetVertexCount (2);

			line.SetPosition (0, firstSelected.transform.position);

			Vector3 screenPoint = Input.mousePosition;
			screenPoint.z = 89;
			line.SetPosition (1, Camera.main.ScreenToWorldPoint (screenPoint));
		} else {
			line.enabled = false;
		}
	}

	public void hoverOver (bool in_hovering, GameObject in_object)
	{
		_hovering = in_hovering;
		hoverObject = in_object;
	}

	public void clickDown (NodeSelector in_node)
	{
		isDown = true;
		_makeLine = true;

		firstSelected = in_node;
	}

	public void clickUp (NodeSelector in_node)
	{
		if (_hovering && isDown) {
			secondSelected = hoverObject.GetComponent<NodeSelector>();
			firstSelected.partOfConnection = true;
			secondSelected.partOfConnection = true;
			firstSelected.changeSelected (true, Color.green);
			secondSelected.changeSelected (true, Color.red);
			connectionMade ();
		}

		isDown = false;
		_makeLine = false;
	}

	void connectionMade ()
	{
		//create new line render for that connection
		GameObject newObject = new GameObject("Connection (" + connections.Count + ")");
		newObject.transform.parent = transform;
		newObject.AddComponent<LineRenderer> ();
		LineRenderer newConnection = newObject.GetComponent<LineRenderer> ();

		List<Vector3> linePositions = new List<Vector3> ();
		linePositions.Add (firstSelected.transform.position);
		linePositions.Add (secondSelected.transform.position);

		Vector3[] vectors = linePositions.ToArray ();

		for (int i = 0; i < vectors.Length; i++) {
			Vector3 v = vectors [i];
			v.z = 89;
			vectors [i] = v;
		}

		newConnection.SetVertexCount (linePositions.Count);
		newConnection.SetPositions (vectors);

		connections.Add (newConnection);
	}

	LineRenderer line;

	public bool _hovering;
	public GameObject hoverObject;
	public bool _makeLine;
	public bool isDown;

	public NodeSelector firstSelected;
	public NodeSelector secondSelected;

	List<LineRenderer> connections = new List<LineRenderer>();
}
