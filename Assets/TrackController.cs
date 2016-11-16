using UnityEngine;
using System.Collections;

public class TrackController : MonoBehaviour {

	void Start () {
	
		assignEnterAndExit ();
	}
	
	void Update () {
	
	}

	void assignEnterAndExit(){

		int enterIndex = (int)enterHere;
		int exitIndex = (int)exitHere;

		if (enterIndex != exitIndex) {

			Vector2 enterP = enterExitPositionValues [enterIndex];
			Vector2 exitP = enterExitPositionValues [exitIndex];

			enterPoint.localPosition = new Vector3 (enterP.x, enterP.y, zOffset);
			exitPoint.localPosition = new Vector3 (exitP.x, exitP.y, zOffset);
		} else {
			Debug.Log ("<color=red>Cannot have the enter and exit on the same tile</color>");
		}
	}

	void createTrackPath(){

	}

	void canExit(){

	}

	public Transform enterPoint;
	public Transform exitPoint;

	public ExitType enterHere;
	public ExitType exitHere;

	public float zOffset = -1;
	public Vector2[]enterExitPositionValues;
}
