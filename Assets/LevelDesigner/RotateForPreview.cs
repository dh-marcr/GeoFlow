using UnityEngine;
using System.Collections;

public class RotateForPreview : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
		transform.Rotate (rotateAngle * Time.deltaTime);
	}

	public Vector3 rotateAngle;
}
