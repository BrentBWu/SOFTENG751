﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {
	public bool isTaskPool;
	public bool active;

	void Start(){
		active = true;
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
		if (!item || isTaskPool) {
			DragHandler.itemBeingDragged.transform.SetParent (transform);
		}
	}

	#endregion
}
