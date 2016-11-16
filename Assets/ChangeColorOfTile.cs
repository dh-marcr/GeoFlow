using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class ChangeColorOfTile : MonoBehaviour, IPointerDownHandler {

	void Start () {
		image = GetComponent<Image> ();
		image.color = Color.white;
	}
	
	public void OnPointerDown(PointerEventData in_data){

		switch (color) {

		case 0:
			image.color = Color.white;
			break;

		case 1:
			image.color = Color.red;
			break;

		case 2:
			image.color = Color.green;
			break;
		}

		if (color < 2) {
			color++;
		} else {
			color = 0;
		}
	}

	Image image;
	int color = 1;
}
