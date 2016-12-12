using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class SelectTile : MonoBehaviour, IPointerDownHandler {

	void Start () {
		image = GetComponent<Image> ();
		image.color = Color.white;

		faceConfig = transform.parent.GetComponent<CubeFaceConfiguration> ();
	}
	
	public void OnPointerDown(PointerEventData in_data){

		switch (color) {

		case 0:
			image.color = Color.white;
			removeTileFromGrid ();
			break;

		case 1:
			image.color = Color.blue;
			addTileToGrid ();
			break;

		case 2:
			image.color = Color.red;
			removeTileFromGrid ();
			break;

		case 3:
			image.color = Color.green;
			removeTileFromGrid ();
			break;
		}

		if (color < 3) {
			color++;
		} else {
			color = 0;
		}
	}

	void addTileToGrid(){

		if (!faceConfig.tilePositions.Contains (gridPosition)) {
			faceConfig.tilePositions.Add (gridPosition);
		}
	}

	void removeTileFromGrid(){

		if (faceConfig.tilePositions.Contains (gridPosition)) {
			faceConfig.tilePositions.Remove (gridPosition);
		}
	}

	CubeFaceConfiguration faceConfig;

	Image image;
	int color = 1;
	public int gridPosition;
}
