using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenDebugger : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
		Vector3 position = Vector3.zero;

		if (Input.GetMouseButton (0)) {

			position = Input.mousePosition;
			position.z = Mathf.Abs(refCam.transform.position.z);
			transform.position = refCam.ScreenToWorldPoint (position);
		}

		Vector2 screen = new Vector2 (refCam.pixelWidth, refCam.pixelHeight);
		float half = screen.x * 0.555f;
		float screenX = screen.x - half;
		float adjustment = screen.x / 22.34f;

		screenPosition.text = "Screen Position : " + position.ToString();
		screenSize.text = "Screen Size : " + screen.ToString();
		halfScreen.text = "Half Screen Size : " + half.ToString() + "  With xAdjustment : " + (half + adjustment).ToString();
		clampedPosition.text = "Clamped X : " + screenX.ToString () + "  with xAdjustment : " + (screenX - adjustment).ToString (); 
	}

	public Camera refCam;

	public Text screenPosition;
	public Text screenSize;
	public Text halfScreen;
	public Text clampedPosition;
}
