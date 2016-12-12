using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class NodeSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

	void Start(){
		if (!creationController) {
			creationController = FindObjectOfType<PathCreationController> ();
		}
	}

	public void OnPointerEnter(PointerEventData in_data){

		if (partOfConnection)
			return;
		
		changeSelected (true , Color.white);
		creationController.hoverOver (true, gameObject);
	}

	public void OnPointerExit(PointerEventData in_data){

		if (partOfConnection)
			return;

		changeSelected (false, Color.white);
		creationController.hoverOver (false, null);
	}

	public void OnPointerDown(PointerEventData in_data){

		if (partOfConnection)
			return;
		
		changeSelected (true, Color.green);
		creationController.clickDown (this);
	}

	public void OnPointerUp(PointerEventData in_data){

		if (partOfConnection)
			return;
		
		hoveringOver.gameObject.SetActive (false);
		creationController.clickUp (this);
		//changeSelected (false, Color.white);
	}

	public void changeSelected(bool in_isActive, Color in_color){

		hoveringOver.gameObject.SetActive (in_isActive);
		hoveringOver.color = in_color;
	}

	PathCreationController creationController;

	public Image hoveringOver;

	public bool partOfConnection;
}
