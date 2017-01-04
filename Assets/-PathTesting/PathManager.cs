using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathManager : MonoBehaviour
{

	void Update ()
	{
		if (!startTile)
			return;

		PathNode[] nodes = FindObjectsOfType<PathNode> ();
		foreach (PathNode pn in nodes) {
			if (pn.exitPoint) {
				pn.connected = false;
			}
		}

		currentPath.Clear ();
		tilesUsedForPath = GetNeighbours();
	}


	List<TileController> GetNeighbours(){

		List<TileController> found = new List<TileController> ();
		List<TileController> toCheck = new List<TileController> ();

		toCheck.Add (startTile);
		found.Add (startTile);
		while (toCheck.Count > 0) {

			TileController current = toCheck [0];
			toCheck.RemoveAt (0);

			foreach (Transform t in current.pathNodes) {
				PathNode node = t.GetComponent<PathNode> ();
				PathNode exitNode = node.exitPoint.GetComponent<PathNode> ();

				if (!node.connected && exitNode.connected) {

					AddToPath (node, exitNode);

					Collider[] cols = Physics.OverlapSphere (node.transform.position, 0.25f);

					foreach (Collider c in cols) {
						if (c.transform != node.transform && c.tag == "PathNode") {

							TileController nextTile = c.GetComponentInParent<TileController> ();
							PathNode connectedNode = c.GetComponent<PathNode> ();

							found.Add (nextTile);
							node.connected = true;
							connectedNode.connected = true;

							toCheck.Add (nextTile);
						}
					}
				}
			}
			current = null;
		}
		return found;
	}

	void AddToPath (PathNode in_entrance, PathNode in_exit){

		if (in_exit.exitPoint && !currentPath.Contains (in_exit.transform)) {
			currentPath.Add (in_exit.transform);
		}

		if (in_entrance.exitPoint && !currentPath.Contains (in_entrance.transform)) {
			currentPath.Add (in_entrance.transform);
		}
	}

	public TileController startTile;
	public List<TileController> tilesUsedForPath;

	public List<Transform> currentPath;
}
