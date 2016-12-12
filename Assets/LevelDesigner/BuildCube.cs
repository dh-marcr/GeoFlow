using UnityEngine;
using System.Collections;

public class BuildCube : MonoBehaviour
{

	void Start ()
	{
		
		for (int i = 0; i < 6; i++) {
			buildGrid ();
		}
	}

	void buildGrid ()
	{
		GameObject newFaceHolder = new GameObject ("NewFace");
		float offset = (tileSpace * gridSize) + tileSpace;
		newFaceHolder.transform.position = new Vector3 (((interval * gridSize) + offset) * 0.5f, ((interval * gridSize) + offset) * 0.5f, (((interval * gridSize) + (offset - tileSpace)) * 0.5f) + depthOffset);

		Vector3 lastPosition = Vector3.zero;
		Vector3 newTilePos = Vector3.zero;
		GameObject newTile = null;

		int xAxisCount = 1;
		int yAxisCount = 1;

		for (int i = 1; i < (gridSize * gridSize + 1); i++) {

			//Debug.Log ("Created tile (" + i + ")");

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

			float chance = Random.value;
			if (chance > 0.55f) {
				newTile = (GameObject)Instantiate (tilePrefab, Vector3.zero, Quaternion.identity) as GameObject;
				//newTilePos.z -= Random.Range (0, 0.5f);
				newTile.transform.localPosition = newTilePos;

				newTile.transform.parent = newFaceHolder.transform;
			}
		}

		newFaceHolder.transform.position = Vector3.zero;
		calculateFaceRotation (newFaceHolder.transform);
		newFaceHolder.transform.parent = transform;
	}

	int faceIndex = 0;

	void calculateFaceRotation (Transform in_grid)
	{

		//which orientation the new face should be - left, right, front, back, up, down

		Vector3 newRotation = Vector3.zero;
		switch (faceIndex) {

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
			newRotation = new Vector3 (0, 90, 180);
			break;

		case 4:
			//right face
			newRotation = new Vector3 (0, 270, 180);
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

	public int gridSize;
	public float tileSpace;
	public float interval;
	public float depthOffset;

	public GameObject tilePrefab;
}
