using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DecryptTexture : MonoBehaviour
{

	void Start ()
	{
		decryptGridGuide ();
	}
	
	public void decryptGridGuide ()
	{
		for (int j = 0; j < 6; j++) {
			if (gridGuide.Length == 0) {
				pixels = soloGridGuide.GetPixels ();
			} else {
				pixels = gridGuide [j].GetPixels ();
			}
			gridSize = (int)Mathf.Sqrt (pixels.Length);

			List<GameObject> currentGrid = buildGrid ();
			List<GameObject> toRemove = new List<GameObject> ();

			//separate black tiles
			for (int i = 0; i < currentGrid.Count; i++) {

				if (pixels [i].r < 0.5f && pixels [i].g < 0.5f && pixels [i].b < 0.5f) {
					//black pixels
					toRemove.Add (currentGrid [i]);
				}
			}

			//remove from current grid
			foreach (GameObject go in toRemove) {

				currentGrid.Remove (go);
				Destroy (go);
			}

			//rename current grid
			int nameRef = 1;
			foreach (GameObject go in currentGrid) {
				go.name = "Tile(" + nameRef + ")";
				nameRef++;
			}
		}
	}

	List<GameObject> buildGrid ()
	{
		List<GameObject> newGrid = new List<GameObject> ();
		GameObject newFaceHolder = new GameObject ("NewFace");
		float offset = (tileSpace * gridSize) + tileSpace;
		newFaceHolder.transform.position = new Vector3 (((interval * gridSize) + offset) * 0.5f, ((interval * gridSize) + offset) * 0.5f, ((interval * gridSize) + (offset - tileSpace)) * 0.5f);

		Vector3 lastPosition = Vector3.zero;
		Vector3 newTilePos = Vector3.zero;
		GameObject newTile = null;

		int xAxisCount = 1;
		int yAxisCount = 1;

		for (int i = (int)Mathf.Pow (gridSize, 2); i > 0; i--) {

			Debug.Log ("Created tile (" + i + ")");

			newTile = (GameObject)Instantiate (tilePrefab, Vector3.zero, Quaternion.identity) as GameObject;

			if (xAxisCount == 1) {
				if (yAxisCount == 1) {
					newTilePos = new Vector3 ((lastPosition.x + tileSpace) + (interval * 0.5f), (lastPosition.y + tileSpace) + (interval * 0.5f), lastPosition.z);
				} else {
					newTilePos = new Vector3 ((lastPosition.x + tileSpace) + (interval * 0.5f), (lastPosition.y + tileSpace) + interval, lastPosition.z);
				}
			} else {
				newTilePos = new Vector3 ((lastPosition.x + tileSpace) + interval, lastPosition.y, lastPosition.z);
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
			newTile.transform.parent = newFaceHolder.transform;
			newGrid.Add (newTile);
		}

		newFaceHolder.transform.position = Vector3.zero;
		calculateFaceRotation (newFaceHolder.transform);
		return newGrid;
	}

	int faceIndex = 0;
	void calculateFaceRotation(Transform in_grid){
		
		//which orientation the new face should be - left, right, front, back, up, down

		Vector3 newRotation = Vector3.zero;
		switch (faceIndex) {

		//front is default rotation

		case 1:
			//bottom face
			newRotation = new Vector3 (270, 0, 0);
			break;

		case 2:
			//back face
			newRotation = new Vector3 (0, 180, 0);
			break;

		case 3:
			//left face
			newRotation = new Vector3 (0, 90, 0);
			break;

		case 4:
			//right face
			newRotation = new Vector3 (0, 270, 0);
			break;

		case 5:
			//top face
			newRotation = new Vector3 (90, 0, 0);
			break;
		}

		in_grid.name = "Face (" + faceIndex + ")";
		in_grid.eulerAngles = newRotation;
		faceIndex++;
	}

	public Texture2D soloGridGuide;
	public Texture2D[] gridGuide;
	public Color[] pixels;
	public int gridSize;
	float interval = 1;
	public float tileSpace;
	public GameObject tilePrefab;
}
