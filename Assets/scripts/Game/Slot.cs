﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {
	public bool isTaskPool;
	public bool active;
	public bool depFree;

	void Start(){
		active = true;
		depFree = true;
	}

	public GameObject item{
		get{ 
			if (transform.childCount > 0) {
				return transform.GetChild (0).gameObject;
			}

			return null;
		}
	}

	public void lockSlot(bool lockSlot){
		active = lockSlot;
	}

	#region IDropHandler implementation

	public void OnDrop (PointerEventData eventData)
	{
		if (!item && !isTaskPool && transform.childCount != 1) {
			DragHandler.itemBeingDragged.transform.SetParent (transform);
			DragHandler.itemBeingDragged.transform.GetComponent<Task>().startTime = transform.parent.transform.GetComponent<Processor>().calculateTotalTime()
				- DragHandler.itemBeingDragged.transform.GetComponent<Task>().weight;
			foreach (GameObject task in GameObject.FindGameObjectsWithTag("Task")) {
				if (task.GetComponent<Task>().taskName.Trim() == DragHandler.itemBeingDragged.transform.GetComponent<Task> ().dependenceName) {
					task.transform.parent.GetComponent<Slot> ().depFree = false;
				}
			}
		}else if(isTaskPool){
			DragHandler.itemBeingDragged.transform.SetParent (transform);
			DragHandler.itemBeingDragged.transform.GetComponent<Task>().startTime = 0;
			foreach (GameObject task in GameObject.FindGameObjectsWithTag("Task")) {
				if (task.GetComponent<Task>().taskName.Trim() == DragHandler.itemBeingDragged.transform.GetComponent<Task> ().dependenceName) {
					task.transform.parent.GetComponent<Slot> ().depFree = true;
				}
			}

		}
			
	}

	#endregion
}
