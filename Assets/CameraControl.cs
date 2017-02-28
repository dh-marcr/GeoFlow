using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{

	public void SetAngle ()
	{
		transform.localEulerAngles = defaultPosition;
	}

	public void SwipeCamera (Direction in_dir)
	{
		if (rotating)
			return;

		Vector3 newRot = Vector3.zero;
		Vector3 current = transform.localEulerAngles;
		Vector3 newAngle = Vector3.zero;
		Vector3 currentAngle = angle.localEulerAngles;

		switch (in_dir) {
		case Direction.right:
			newRot = new Vector3 (current.x, current.y - 90, current.z);
			RotateLeftRightCamera (newRot);
			break;

		case Direction.left:
			newRot = new Vector3 (current.x, current.y + 90, current.z);
			RotateLeftRightCamera (newRot);
			break;

		case Direction.up:
			if (currentAngle.z < 224) {
				newAngle = new Vector3 (currentAngle.x, currentAngle.y, currentAngle.z + 45);
				RotateUpDownCamera (newAngle);
			}
			break;

		case Direction.down:
			if (currentAngle.z > 136) {
				newAngle = new Vector3 (currentAngle.x, currentAngle.y, currentAngle.z - 45);
				RotateUpDownCamera (newAngle);
			}
			break;
		}

	}

	void RotateLeftRightCamera (Vector3 in_moveTo)
	{
		rotating = true;
		LeanTween.value (gameObject, transform.localEulerAngles, in_moveTo, rotationSpeed).setOnUpdate ((Vector3 val) => transform.localEulerAngles = val).setOnComplete(FinishRotation);
	}

	void RotateUpDownCamera(Vector3 in_moveTo){
		rotating = true;
		LeanTween.value (gameObject, angle.localEulerAngles, in_moveTo, rotationSpeed).setOnUpdate ((Vector3 val) => angle.localEulerAngles = val).setOnComplete(FinishRotation);
	}

	void FinishRotation(){
		rotating = false;
	}

	bool rotating = false;

	public Vector3 defaultPosition;
	public float rotationSpeed = 0.5f;
	public Transform angle;
}
