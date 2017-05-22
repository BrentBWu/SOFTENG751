using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public static GameObject itemBeingDragged;
	Vector3 startPos;
	Transform startParent;
	public Slot taskSlot;

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{
		itemBeingDragged = this.gameObject;
		startPos = transform.position;
		startParent = transform.parent;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;

		//Display Task info
		GameObject.Find ("TaskName").GetComponent<Text> ().text = "Task: " + transform.GetComponent<Task> ().taskName;
		GameObject.Find ("Weight").GetComponent<Text> ().text = "Weight: " + transform.GetComponent<Task> ().weight;
		GameObject.Find ("DependsOn").GetComponent<Text> ().text = "Depends on:\n " + transform.GetComponent<Task> ().getDependenceList();
		Debug.Log (transform.parent.transform.parent.name);

		//Check Dependency




		foreach (GameObject processor in GameObject.FindGameObjectsWithTag("Processor")) {
			//Instantiate duration

			//Instantiate slots in processor
			bool isTaskSlot = transform.parent.transform.parent.transform.GetSiblingIndex () != processor.transform.GetSiblingIndex ();
			bool isTaskpool = transform.parent.GetComponent<Slot> ().isTaskPool;
			if (isTaskSlot || !isTaskpool) {
				Slot slot = Slot.Instantiate (taskSlot);
				slot.transform.SetParent(processor.transform);
				slot.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (60, transform.GetComponent<Task> ().weight * 10);
				slot.transform.SetAsFirstSibling();
			}


		}
	}
	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		if (checkDependence ()) {
			transform.position = Input.mousePosition;
		}

	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		itemBeingDragged = null;
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		if (transform.parent == startParent) {
			transform.position = startPos;
		}

		//Destroy unused slot
		foreach (GameObject slot in GameObject.FindGameObjectsWithTag("Slot")) {
			if(slot.transform.childCount == 0){
				Destroy (slot);
			}
		}

		//Destroy unused duration
		foreach(GameObject dur in GameObject.FindGameObjectsWithTag("Duration")){
			if (dur.transform.GetSiblingIndex() == 0) {
				Destroy (dur);
			}
		}

		//Set slot lock
		foreach (GameObject slot in GameObject.FindGameObjectsWithTag("Slot")) {
			if (slot.transform.GetSiblingIndex () == 0) {
				slot.GetComponent<Slot> ().active = true;
			} else {
				slot.GetComponent<Slot> ().active = false;
			}
		}

	}

	#endregion

	private bool checkDependence(){
		/*if(transform.GetComponent<Task>().)
		foreach (GameObject task in GameObject.FindGameObjectsWithTag("Task")) {
			
		}*/
		return true;
	}
}
