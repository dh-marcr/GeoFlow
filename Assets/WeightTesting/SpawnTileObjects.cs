using UnityEngine;
using System.Collections;

public class SpawnTileObjects : MonoBehaviour {

	public void spawnObjectsOnTiles(){

		allTiles = FindObjectsOfType<TileController> ();

		foreach (TileController tc in allTiles) {

			int objectCount = Random.Range (0, 5);

			for (int i = 0; i < objectCount; i++) {

				GameObject newObject = (GameObject)Instantiate (possibleObjects [Random.Range (0, possibleObjects.Length)], Vector3.zero, tc.transform.rotation) as GameObject;
				newObject.transform.localScale = new Vector3 (1, 1, 1);
				newObject.transform.parent = tc.objectHolder.transform;
				newObject.transform.localPosition = randomPositionOnTile (tc.transform);
			}
		}
	}

	Vector3 randomPositionOnTile(Transform in_tile){

		Vector3 tileBounds = in_tile.GetComponent<Collider> ().bounds.size;
		float xBounds = tileBounds.x * 0.5f;
		float yBounds = tileBounds.y * 0.5f;

		return new Vector3 (Random.Range (-xBounds, xBounds), Random.Range (-yBounds, yBounds), 0);
	}

	TileController[] allTiles;
	public GameObject[] possibleObjects;
}
