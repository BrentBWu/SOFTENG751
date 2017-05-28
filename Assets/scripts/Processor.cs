using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Processor : MonoBehaviour {
	public int totalTime;

	// Use this for initialization
	void Start () {
		
	}
	
	public void createTaskSlot(){
		
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
