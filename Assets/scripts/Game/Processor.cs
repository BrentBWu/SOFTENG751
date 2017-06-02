using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Processor : MonoBehaviour {
	public int totalTime;
	public Slot taskSlot;
	public Task duration;

	//Instantiate Slot in processor
	public void createTaskSlot(int weight){
		Slot slot = Slot.Instantiate (taskSlot);
		slot.transform.SetParent(transform);
		slot.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (60, weight * 10);
		slot.transform.SetAsFirstSibling();
	}

	//Instantiate duration in processor
	public void createDuration(int durWeight){
		Task dur = Instantiate (duration);
		dur.transform.SetParent (transform);
		dur.transform.SetAsFirstSibling();
		dur.weight = durWeight;
		dur.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (60, dur.weight * 10);
	}

	public int calculateTotalTime(){
		int time = 0;
		foreach (Transform child in transform) {
			if (child.childCount > 0) {
				time += child.transform.GetChild (0).transform.GetComponent<Task> ().weight;
			} else if (child.tag == "Duration") {
				time += child.transform.GetComponent<Task> ().weight;
			}

		}
		totalTime = time;
		return totalTime;
	}
}
