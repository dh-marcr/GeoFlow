using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[ExecuteInEditMode]
public class ItemScaler : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {

		if (!layout) {
			layout = GetComponent<HorizontalLayoutGroup> ();
		}
	
		itemSlots = GetComponentsInChildren<Image> ();
		itemCount = itemSlots.Length;


		layout.padding.right = 50 - (itemCount * 9);
	}

	public Image[] itemSlots;
	int itemCount;
	HorizontalLayoutGroup layout;
}
