using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TileController : MonoBehaviour
{
	void Start ()
	{
		uiControl.SetActive (false);
		restrictionVal = 0.5f * ((gridSize / 2) + 1);
	}

	public void moveTile (Direction in_dir)
	{
		Transform node = null;
		Vector2 moveValue = Vector2.zero;

		switch (in_dir) {
		case Direction.left:
			node = leftNode;
			moveValue = new Vector2 (-moveVal, 0);
			break;

		case Direction.right:
			node = rightNode;
			moveValue = new Vector2 (moveVal, 0);
			break;

		case Direction.up:
			node = upNode;
			moveValue = new Vector2 (0, moveVal);
			break;

		case Direction.down:
			node = downNode;
			moveValue = new Vector2 (0, -moveVal);
			break;
		}

		bool canMove = canMoveTile (node);

		if (canMove) {
			Vector3 tile = transform.localPosition;
			tile = new Vector3 (tile.x + moveValue.x, tile.y + moveValue.y, tile.z);
			LeanTween.value (gameObject, transform.localPosition, tile, 0.25f).setOnUpdate ((Vector3 val) => transform.localPosition = val);
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

	public float moveVal;

	public float radius = 0.5f;
	public Transform leftNode, rightNode, upNode, downNode;
	public int gridSize;
	public float restrictionVal;

	//testing only
	public GameObject objectHolder;

	public Transform[] pathNodes;
}

