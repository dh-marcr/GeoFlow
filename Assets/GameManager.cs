using UnityEngine;
using System.Collections;

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

				if (hit.collider.tag == "Object") {
					Destroy (hit.transform.parent.gameObject);
				}

				if (hit.collider.tag == "Tile") {
					//selectTile (hit.collider.GetComponent<TileController> ());
				} else if (hit.collider.tag == "BehindGame") {
					//selectedTile.uiControl.SetActive (false);
					//selectedTile = null;
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

	static public GameManager instance;

	public TileController selectedTile;
}
