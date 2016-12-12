using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Grid : MonoBehaviour {

	void Start () {

		faceConfig = GetComponent<CubeFaceConfiguration> ();
		makeGrid ();
	}

	public void makeGrid(){

		float interval = Mathf.RoundToInt((gridBG.sizeDelta.x - (gridBorder)) / gridSize);
		//Debug.Log ("interval : " + interval);
		Vector3 lastPosition = Vector3.zero;
		Vector3 newTilePos = Vector3.zero;
		GameObject newTile = null;

		int xAxisCount = 1;
		int yAxisCount = 1;

		for (int i = 0; i < gridSize * gridSize; i++) {

			newTile = (GameObject)Instantiate (tileUIPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			newTile.transform.parent = transform;
			newTile.AddComponent<SelectTile> ();
			newTile.GetComponent<SelectTile> ().gridPosition = i + 1; 
			newTile.GetComponent<RectTransform> ().sizeDelta = new Vector2 (interval, interval);

			if (xAxisCount == 1) {
				if (yAxisCount == 1) {
					newTilePos = new Vector3 ((lastPosition.x + gridBorder) + (interval * 0.5f), lastPosition.y + (interval * 0.5f), lastPosition.z);
				} else {
					newTilePos = new Vector3 ((lastPosition.x + gridBorder) + (interval * 0.5f), lastPosition.y + interval, lastPosition.z);
				}
			} else {
				newTilePos = new Vector3 (lastPosition.x + (interval), lastPosition.y, lastPosition.z);
			}

			if (xAxisCount >= gridSize) {
				xAxisCount = 0;
				yAxisCount++;
				lastPosition = new Vector3 (0, lastPosition.y, lastPosition.z);
			} else {
				lastPosition = newTilePos;
			}

			xAxisCount++;

			newTile.transform.localPosition = newTilePos;
		}
	}

	CubeFaceConfiguration faceConfig;

	public RectTransform gridBG;
	public GameObject tileUIPrefab;

	public int gridSize = 4;
	public float gridBorder = 1;
}

