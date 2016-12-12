using UnityEngine;
using System.Collections;

public class PointerAdjuster : MonoBehaviour
{

	void Start ()
	{
	
	}

	void Update ()
	{
		Vector3 screenPosition = Input.mousePosition;
		screenPosition.z = Mathf.Abs (pointerCam.transform.position.z);
		screenPosition = pointerCam.ScreenToWorldPoint (screenPosition);
	
		if (Input.GetMouseButtonDown (0)) {
			firstClick = screenPosition - centerPoint.position;
			_isDown = true;
		} else if (Input.GetMouseButtonUp (0)) {
			firstClick = Vector3.zero;
			_isDown = false;
		}

		if (_isDown) {
			pointerRef.position = screenPosition;

			Vector3 diff = screenPosition - pointerRef.position;
			Vector3 newOffset = Vector3.zero;
			float xAdj = 0;
			float yAdj = 0;

			if (Mathf.Abs (firstClick.x) > Mathf.Abs (firstClick.y)) {
				//x is greater
				xAdj = 0.15f;
				yAdj = 0.3f;
			} else {
				//y is greater
				xAdj = 0.3f;
				yAdj = 0.15f;
			}

			newOffset = new Vector3 (pointerRef.position.x - ((pointerRef.position.x - firstClick.x) * xAdj), 
				pointerRef.position.y - ((pointerRef.position.y - firstClick.y) * yAdj), 
				pointerRef.position.z);

			pointerOffset.position = newOffset;
		}
	}

	public Vector3 firstClick;

	bool _isDown;

	public Camera pointerCam;
	public Transform centerPoint;
	public Transform pointerRef;
	public Transform pointerOffset;
}
