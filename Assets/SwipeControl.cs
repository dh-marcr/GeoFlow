using UnityEngine;
using System.Collections;

public class SwipeControl : MonoBehaviour
{

	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {

			swipeStart = Input.mousePosition;

			Ray ray = Camera.main.ScreenPointToRay (swipeStart);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.tag == "BehindGame") {
					objectToSwipe = hit.transform;
					cameraSwipe = true;
					swiping = true;
				} else if (hit.collider.tag == "Tile") {
					objectToSwipe = hit.transform;
					tileSwipe = true;
					swiping = true;
				}
			}
		}

		if (!swiping)
			return;
		
		if (Input.GetMouseButtonUp (0)) {

			swipeEnd = Input.mousePosition;
			Vector3 swipeDelta = swipeEnd - swipeStart;

			Debug.Log ("swipe delta : " + swipeDelta);

			swiping = false;

			DoSwipe (swipeDelta);
		}
	}

	void DoSwipe(Vector3 in_delta){

		Direction newDir = Direction.none;

		if (in_delta.x > minimumSwipe && in_delta.y < minimumSwipe * 2) {
			newDir = Direction.right;
		} else if (in_delta.x < -minimumSwipe && in_delta.y > -(minimumSwipe * 2)) {
			newDir = Direction.left;
		}else if(in_delta.y > minimumSwipe && in_delta.x < minimumSwipe * 2){
			newDir = Direction.up;
		}else if(in_delta.y < -minimumSwipe && in_delta.x > -(minimumSwipe * 2)){
			newDir = Direction.down;
		}

		if (newDir == Direction.none)
			return;

		if (tileSwipe) {

			TileController tc = objectToSwipe.GetComponent<TileController> ();
			tc.moveTile (newDir);

			tileSwipe = false;
		} else if (cameraSwipe) {

			CameraControl cc = objectToSwipe.GetComponentInParent<CameraControl> ();
			cc.SwipeCamera (newDir);

			cameraSwipe = false;
		}
	}

	public float minimumSwipe = 15;

	bool swiping = false;
	bool tileSwipe;
	bool cameraSwipe;

	Transform objectToSwipe;

	Vector3 swipeStart;
	Vector3 swipeEnd;
}
