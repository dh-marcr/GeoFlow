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

			Vector3[] newCurve = calculateCurve ();
			//temporary ref values
			middleOfCurve.position = newCurve[2];
			beginningOffset.position = newCurve [3];
			beginningPoint.position = newCurve [4];
			//

			bezierCurve = Curver.MakeSmoothCurve (newCurve, curveIterations);

			Vector3 screenPoint = Input.mousePosition;
			screenPoint.z = Mathf.Abs(pointerCam.transform.position.z) + 90;
			pointerRef.position = pointerCam.ScreenToWorldPoint (screenPoint);


			line.enabled = true;
			line.SetVertexCount (bezierCurve.Length);
			line.SetPositions (bezierCurve);
		} else {
			line.enabled = false;
			line.SetVertexCount (1);
			line.SetPosition (0, Vector3.zero);
		}
	}

	Vector3[] calculateCurve(){

		Vector3[] newCurve = new Vector3[5];

		newCurve [0] = pointerRef.position;
		newCurve [1] = pointerOffset.position;
		newCurve [2] = Vector3.zero;
		newCurve [3] = beginningOffset.position;
		newCurve [4] = firstSelected.transform.position;

		return newCurve;
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

		calculatePointerRotation (in_node.transform);
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

	void calculatePointerRotation(Transform in_transform){

		Vector3 tilePosition = in_transform.parent.transform.position;
		Vector3 nodePosition = in_transform.position;

		Vector3 diff = nodePosition - tilePosition;

		Vector3 newOffset = Vector3.zero;

		if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y)) {
			Debug.Log ("x is greater");
			if (diff.x > 0) {
				Debug.Log ("turn towards left");
				newOffset = new Vector3 (6, 0, 0);
			} else {
				Debug.Log ("turn towards right");
				newOffset = new Vector3 (-6, 0, 0);
			}
		} else {
			Debug.Log ("y is greater");
			if (diff.y > 0) {
				Debug.Log ("turn towards down");
				newOffset = new Vector3 (0, 6, 0);
			} else {
				Debug.Log ("turn towards up");
				newOffset = new Vector3 (0, -6, 0);
			}
		}

		pointerOffset.localPosition = newOffset;
	}

	void connectionMade ()
	{
		//create new line render for that connection
		GameObject newObject = new GameObject("Connection (" + connections.Count + ")");
		newObject.transform.parent = transform;
		newObject.AddComponent<LineRenderer> ();
		LineRenderer newConnection = newObject.GetComponent<LineRenderer> ();

		newConnection.SetVertexCount (bezierCurve.Length);
		newConnection.SetPositions (bezierCurve);
	}

	LineRenderer line;

	public Camera pointerCam;
	//remove meshes when this works
	public Transform pointerRef;
	public Transform pointerOffset;
	//temporary visual reference until this works
	public Transform middleOfCurve;
	public Transform beginningOffset;
	public Transform beginningPoint;

	Vector3[] bezierCurve;

	public float curveIterations = 5;

	public bool _hovering;
	public GameObject hoverObject;
	public bool _makeLine;
	public bool isDown;

	public NodeSelector firstSelected;
	public NodeSelector secondSelected;

	List<LineRenderer> connections = new List<LineRenderer>();
}
