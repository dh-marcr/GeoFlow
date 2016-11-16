using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour {

	void Start () {
	
		curvePoints = new Vector3[5];
	}
	
	void Update () {

		if (Input.GetMouseButton (0)) {

			Vector3 screenPosition = Input.mousePosition;
			screenPosition.z = 99;
			transform.position = Camera.main.ScreenToWorldPoint (screenPosition);
		}

		Vector3 lookRotation = new Vector3 (tile.transform.position.x, tile.transform.position.y, transform.position.z);
		//transform.LookAt (lookRotation);

		calculatePoints ();

		Vector3[] newLine = Curver.MakeSmoothCurve (curvePoints, smoothness);

		for (int i = 0; i < newLine.Length; i++) {
			Vector3 v = newLine [i];
			v.z = 89;
			newLine [i] = v;
		}

		line.SetVertexCount (newLine.Length);
		line.SetPositions (newLine);
	}

	void calculatePoints(){

		curvePoints [0] = firstTouch.position;
		curvePoints [1] = new Vector3 (firstTouch.position.x + offset, firstTouch.position.y, firstTouch.position.z);
		curvePoints [2] = middlePoint.position;
		curvePoints [3] = secondPoint.position;
		curvePoints [4] = transform.position;
	}

	public LineRenderer line;

	public float offset;

	public Transform secondPoint;
	public Transform middlePoint;

	public float smoothness;
	public Vector3[] curvePoints;

	public Transform tile;
	public Transform firstTouch;
}
