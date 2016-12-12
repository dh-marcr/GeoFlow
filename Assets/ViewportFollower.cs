using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ViewportFollower : MonoBehaviour {

	void Start () {

		viewportSize = new Vector2 (pointerCam.pixelWidth, pointerCam.pixelHeight);
		boxAdjustment = viewportSize.x / 22.34f;
	}
	
	void Update () {
		
		if (Input.GetMouseButton (0)) {
			screenPosition = Input.mousePosition;
			float deadzoneX = viewportSize.x - (viewportSize.x * 0.555f);
			if (screenPosition.x < deadzoneX) {		
				screenPosition.x = Mathf.Clamp (screenPosition.x, boxAdjustment + edgeOffset, deadzoneX - (boxAdjustment + edgeOffset));
				screenPosition.y = Mathf.Clamp (screenPosition.y, boxAdjustment + edgeOffset, viewportSize.y - (boxAdjustment + edgeOffset));
				screenPosition.z = Mathf.Abs(pointerCam.transform.position.z);
				transform.position = pointerCam.ScreenToWorldPoint (screenPosition);
			}
		}
	}

	public Camera pointerCam;

	float boxAdjustment;
	public float edgeOffset;
	Vector3 screenPosition;
	Vector2 viewportSize;
}
