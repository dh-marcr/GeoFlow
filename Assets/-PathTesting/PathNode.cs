using UnityEngine;
using System.Collections;

public class PathNode : MonoBehaviour
{
	void Start(){

		if (!exitPoint)
			connected = true;
	}

	public Transform exitPoint;
	public bool connected;
}
