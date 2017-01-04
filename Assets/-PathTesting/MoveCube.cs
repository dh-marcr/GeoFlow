using UnityEngine;
using System.Collections;

public class MoveCube : MonoBehaviour {

	void Start () {
	
		pathMan = FindObjectOfType<PathManager> ();
	}

	public void StartMoving(){
		FindNextPoint ();
	}
	
	void Update () {
	
		if (!nextPointInPath)
			return;

		Vector3 newPosition = new Vector3 (nextPointInPath.position.x, nextPointInPath.position.y, transform.position.z);

		transform.position = Vector3.MoveTowards (transform.position, newPosition, Time.deltaTime * 1);

		if (transform.position == newPosition) {
			if (pathIndex < pathMan.currentPath.Count) {
				FindNextPoint ();
			} else {
				pathIndex = 0;
				transform.parent = null;
				transform.position = pathMan.currentPath [0].GetComponent<PathNode> ().exitPoint.position;
				nextPointInPath = null;
			}
		}
	}

	public void FindNextPoint(){

		nextPointInPath = pathMan.currentPath [pathIndex];
		transform.parent = nextPointInPath.GetComponentInParent<TileController> ().transform;

		pathIndex += 2;
	}

	PathManager pathMan;

	int pathIndex; 
	public Transform nextPointInPath;
}
