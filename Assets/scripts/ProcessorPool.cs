using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessorPool : MonoBehaviour {
	public int processorNumer;
	public GameObject proccessor; 


	// Use this for initialization
	void Start () {
		for (int i = 0; i < processorNumer; i++) {
			Instantiate (proccessor).transform.SetParent (this.transform);
		}
	}

	public int calculateTime(){
		int[] processorTimes = new int[4];
		int index = 0;
		foreach (Transform child in transform) {
			if (child.tag == "Processor") {
				processorTimes [index] = child.GetComponent<Processor> ().totalTime;
				index++;
			}
		}

		return Mathf.Max (processorTimes);
	}



}
