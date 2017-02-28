using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	void Awake () {
	
		if (!instance)
			instance = this;
	}
	
	void Update () {
	
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Input.GetMouseButtonUp (0)) {
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.tag == "Tile") {
					selectTile (hit.collider.GetComponent<TileController> ());
					EditingView (hit.transform);
				} else if (hit.collider.tag == "BehindGame") {
					if (selectedTile && selectedTile == hit.transform.GetComponent<TileController>()) {
						selectedTile.uiControl.SetActive (false);
						selectedTile = null;
					}
				}
			}
		}
	}

	void selectTile(TileController in_tileHit){

		if (selectedTile != null) {
			if (selectedTile == in_tileHit) {
				in_tileHit.uiControl.SetActive (false);
				selectedTile = null;
			} else {
				selectedTile.uiControl.SetActive (false);
				selectedTile = in_tileHit;
				in_tileHit.uiControl.SetActive (true);
			}
		} else {
			selectedTile = in_tileHit;
			in_tileHit.uiControl.SetActive (true);
		}
	}

	void EditingView(Transform in_tile){

		GameObject point = new GameObject ("Point");

		Vector3 mousePos = Input.mousePosition;
		mousePos = Camera.main.WorldToViewportPoint (mousePos);

		point.transform.position = new Vector3 (mousePos.x, mousePos.y, in_tile.position.z);

		Debug.Log ("point : " + point.transform.position);
	}

	static public GameManager instance;

	public TileController selectedTile;

	public List<Transform> currentPath = new List<Transform>();
}
