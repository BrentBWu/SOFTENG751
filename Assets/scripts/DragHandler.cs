using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public static GameObject itemBeingDragged;
	Vector3 startPos;
	Transform startParent;


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
		foreach (GameObject slot in GameObject.FindGameObjectsWithTag("Slot")) {
			if (slot.transform.GetSiblingIndex ()== 0) {
				slot.GetComponent<Slot> ().active = true;
			} else {
				slot.GetComponent<Slot> ().active = false;
			}
		}
			

		//Check Dependency
		if(checkDependence()){
			bool inSlot = transform.parent.tag == "Slot";
			bool hasDep = transform.GetComponent<Task> ().dependenceName != "";
			int depEndTime = 0;
			Processor depPro = null;
			if (hasDep) {
				foreach (GameObject t in GameObject.FindGameObjectsWithTag("Task")) {
					if (t.GetComponent<Task>().taskName.Trim () == transform.GetComponent<Task> ().dependenceName) {
						depEndTime = t.GetComponent<Task>().startTime + t.GetComponent<Task>().weight + transform.GetComponent<Task>().dependenceWeight;
						depPro = t.transform.parent.parent.GetComponent<Processor> ();
					}
				}
			}

			foreach (GameObject processor in GameObject.FindGameObjectsWithTag("Processor")) {
				bool inProcessor = transform.parent.transform.parent.transform == processor.transform;

				//Instantiate duration
				if(hasDep && !inProcessor && depPro.transform != processor.GetComponent<Processor>().transform){
					//Calculate duration length
					int durWeight = depEndTime - processor.GetComponent<Processor>().calculateTotalTime();
					if (durWeight > 0) {
						processor.transform.GetComponent<Processor> ().createDuration (durWeight);
					}

				}

				//Check if slot is active
				if (inSlot) {
					if (!transform.parent.GetComponent<Slot> ().active || !transform.parent.GetComponent<Slot> ().depFree) {
						break;
					}
				}

				//Instantiate slots in processor
				bool isTaskSlot = transform.parent.transform.parent.transform.GetSiblingIndex () != processor.transform.GetSiblingIndex ();
				bool isTaskpool = transform.parent.GetComponent<Slot> ().isTaskPool;

				if ((isTaskSlot || !isTaskpool) && !inProcessor && !transform.GetComponent<Task>().answer) {
					processor.transform.GetComponent<Processor> ().createTaskSlot (transform.GetComponent<Task> ().weight);
				}
			}
		}

	}
	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		if (transform.parent.GetComponent<Slot> ().active && transform.parent.GetComponent<Slot> ().depFree && !transform.GetComponent<Task> ().answer) {
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


		deleteUnused ();

	}

	#endregion

	//Check if task's dependence has been allocated
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

	public void deleteUnused(){
		//Destroy unused slot
		foreach (GameObject slot in GameObject.FindGameObjectsWithTag("Slot")) {
			if(slot.transform.childCount == 0){
				Destroy (slot);
			}
		}

		//Destroy unused duration
		if(!transform.GetComponent<Task>().answer){
			foreach(GameObject dur in GameObject.FindGameObjectsWithTag("Duration")){
				if (dur.transform.parent != transform.parent.transform.parent && dur.transform.GetSiblingIndex() == 1 && 
					(transform.parent.GetComponent<Slot> ().active && transform.parent.GetComponent<Slot> ().depFree)) {
					Destroy (dur);
				}
			}
		}



		foreach (GameObject p in GameObject.FindGameObjectsWithTag("Processor")) {
			p.transform.GetComponent<Processor> ().calculateTotalTime ();
		}

		GameObject.Find ("Processor Pool").GetComponent<ProcessorPool> ().calculateTime ();
	}
		
}
