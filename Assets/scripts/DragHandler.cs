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

		//Set slot lock
		/*foreach (GameObject slot in GameObject.FindGameObjectsWithTag("Slot")) {
			Debug.Log (slot.transform.GetSiblingIndex ());
			if (slot.transform.GetSiblingIndex ()== 0) {
				slot.GetComponent<Slot> ().active = true;
			} else {
				slot.GetComponent<Slot> ().active = false;
			}
		}*/

		//Check Dependency
		if(checkDependence()){
			foreach (GameObject processor in GameObject.FindGameObjectsWithTag("Processor")) {
				//Instantiate duration

				//Instantiate slots in processor
				if (transform.parent.tag == "Slot") {
					if (!transform.parent.GetComponent<Slot> ().active) {
						break;
					}
				}
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

	}
	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
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



	}

	#endregion

	private bool checkDependence(){

		string depName = transform.GetComponent<Task>().dependenceName;
		if (depName != "") {
			foreach (GameObject slot in GameObject.FindGameObjectsWithTag("Slot")){
				if (slot.transform.childCount > 0) {
					if (slot.transform.GetChild (0).transform.GetComponent<Task> ().taskName.Trim() == depName) {
						return true;
					}

				}
			}
		}else{
			return true;
		}

		return false;

	}
}
