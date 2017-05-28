using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessorPool : MonoBehaviour {
	public int processorNumer;
	public GameObject proccessor; 
	public int totalTime;
	public Task task;


	// Use this for initialization
	void Start () {
		initProcessor (processorNumer);
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

	public void initProcessor(int proNum){
		for (int i = 0; i < processorNumer; i++) {
			Instantiate (proccessor).transform.SetParent (this.transform);
		}
		totalTime = 0;
		calculateTime ();
	}

	public void loadAnswer(TextAsset inputFile){
		string wholeFile = inputFile.text;
		List<string> eachLine = new List<string> ();
		eachLine.AddRange (wholeFile.Split ("\n" [0]));
		bool ansStart = false;
		int processorNum = int.Parse (eachLine [0].Split (' ') [1]);

		for (int i = 1; i < eachLine.Count; i++) {
			if (ansStart) {
				//Break when reach the end
				if (eachLine [i].Trim () == "}") {
					break;
				}

				string taskName = eachLine [i].Split ('[', ']') [0];
				string[] taskInfo = eachLine [i].Split ('[', ']') [1].Split (',');
				int taskweight = int.Parse(taskInfo[0].Replace ("Weight=", ""));
				int startTime = int.Parse(taskInfo[1].Replace ("Start=", ""));
				int processor = int.Parse(taskInfo [2].Replace ("Processor=", ""));

				Task t = Task.Instantiate (task);
				t.taskName = taskName;
				t.weight = taskweight;
				t.taskColor = Random.ColorHSV (0f, 1f, 1f, 1f, 0.5f, 1f);

			}

			if (eachLine [i].Trim () == "Answer{") {
				ansStart = true;
			}


		}
	}
		
}
