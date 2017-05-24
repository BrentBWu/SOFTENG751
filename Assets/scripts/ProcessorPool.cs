using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessorPool : MonoBehaviour {
	public int processorNumer;
	public GameObject proccessor; 
	public int totalTime;


	// Use this for initialization
	void Start () {
		for (int i = 0; i < processorNumer; i++) {
			Instantiate (proccessor).transform.SetParent (this.transform);
		}
		totalTime = 0;
		calculateTime ();
	}

	//Find the processor has maximum time and set it to totalTime
	public void calculateTime(){
		int[] processorTimes = new int[4];
		int index = 0;
		foreach (Transform child in transform) {
			if (child.tag == "Processor") {
				processorTimes [index] = child.GetComponent<Processor> ().totalTime;
				index++;
			}
		}

		GameObject.Find ("TotalTime").GetComponent<Text> ().text = "Total time: " + Mathf.Max (processorTimes);
	}
		
}
