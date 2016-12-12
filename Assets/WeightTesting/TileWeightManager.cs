using UnityEngine;
using System.Collections;

public class TileWeightManager : MonoBehaviour
{

	void Start ()
	{
	
		cubeCenter = GameObject.Find ("CubeCenter").transform;
		objectHolder = GetComponent<TileController> ().objectHolder;
		originalWeightValue = transform.localPosition.z;
	}

	void Update ()
	{
	
		if (objectHolder) {
			objectsOnTile = objectHolder.GetComponentsInChildren<ObjectWeight> ();
		}

		totalWeightValue = 0;
		for (int i = 0; i < objectsOnTile.Length; i++) {
		
			totalWeightValue += objectsOnTile [i].value;
		}

		Vector3 localPos = transform.localPosition;
		transform.localPosition = Vector3.MoveTowards (transform.localPosition, 
			new Vector3 (localPos.x, localPos.y, originalWeightValue + 1 * (totalWeightValue / 85)), 
			Time.deltaTime * 0.25f);
	}

	public Transform cubeCenter;
	public GameObject objectHolder;
	public ObjectWeight[] objectsOnTile;

	public float originalWeightValue;
	public float totalWeightValue;

}
