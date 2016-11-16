using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TileController : MonoBehaviour
{
	void Start ()
	{
		uiControl.SetActive (false);
	}

	public void moveTile (int in_value)
	{
		Transform node = null;
		Vector2 moveValue = Vector2.zero;

		switch (in_value) {
		case 0:
			node = leftNode;
			moveValue = new Vector2 (-1.1f, 0);
			break;

		case 1:
			node = rightNode;
			moveValue = new Vector2 (1.1f, 0);
			break;

		case 2:
			node = upNode;
			moveValue = new Vector2 (0, 1.1f);
			break;

		case 3:
			node = downNode;
			moveValue = new Vector2 (0, -1.1f);
			break;
		}

		bool canMove = canMoveTile (node);

		if (canMove) {
			Vector3 tile = transform.position;
			tile = new Vector3 (tile.x + moveValue.x, tile.y + moveValue.y, 0);
			LeanTween.value (gameObject, transform.position, tile, 0.25f).setOnUpdate ((Vector3 val) => transform.position = val);
		} else {
			Debug.Log ("<color=red>Cannot move tile, there is a tile already there</color>");
		}
	}

	public bool canMoveTile (Transform in_transform)
	{
		
		bool _canMove = true;
		Collider[] colliders = Physics.OverlapSphere (in_transform.position, radius);

		foreach (Collider c in colliders) {

			if (c.tag == "Tile" && c.transform != transform) {
				_canMove = false;
			}
		}

		return _canMove;
	}

	public GameObject uiControl;

	public float radius = 0.5f;
	public Transform leftNode, rightNode, upNode, downNode;
}

