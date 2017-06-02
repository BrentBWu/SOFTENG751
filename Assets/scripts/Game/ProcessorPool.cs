﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessorPool : MonoBehaviour {
	public GameObject proccessor; 
	public int totalTime;
	public Task task;
	public Task duration;
	public Slot slot;
	private Color32[] colorPool = {new Color32(128,192,206,255), new Color32(154,222,162,255), new Color32(255,141,104,255), new Color32(255,236,186,255), new Color32(234,231,237,255), 
		new Color32(183,147,230,255), new Color32(255,251,133,255), new Color32(194,246,155,255), new Color32(250,91,117,255), new Color32(71,151,177,255), 
		new Color32(195,190,240,255), new Color32(95,189,197,255), new Color32(252,138,21,255), new Color32(216,217,92,255), new Color32(204,168,233,255) };


	//Find the processor has maximum time and set it to totalTime
	public void calculateTime(bool ans){
		int[] processorTimes = new int[5];
		int index = 0;
		foreach (Transform child in transform) {
			if (child.tag == "Processor") {
				processorTimes [index] = child.GetComponent<Processor> ().totalTime;
				index++;
			}
		}

		if (ans) {
			GameObject.Find ("Canvas").GetComponent<SubmitAnswer> ().setStandardAnswer (Mathf.Max (processorTimes));
		} else {
			GameObject.Find ("TotalTime").GetComponent<Text> ().text = "Total time: " + Mathf.Max (processorTimes);
		}

	}

	//Instantiate processor in processor pool
	public void initProcessor(int proNum){
		for (int i = 0; i < proNum; i++) {
			Instantiate (proccessor).transform.SetParent (this.transform);
		}
		totalTime = 0;
	}

	public void loadAnswer(TextAsset inputFile){
		string wholeFile = inputFile.text;
		List<string> eachLine = new List<string> ();
		List<Task> tasks = new List<Task> ();
		eachLine.AddRange (wholeFile.Split ("\n" [0]));
		bool ansStart = false;
		int processorNum = int.Parse (eachLine [0].Split (' ') [1]);
		int colorIndex = 0;

		initProcessor (processorNum);

		//Load task infomation
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
				t.startTime = startTime;
				t.taskColor = colorPool[colorIndex];
				colorIndex++;
				t.processor = processor;
				tasks.Add (t);

			}
			if (eachLine [i].Trim () == "Answer{") {
				ansStart = true;
			}
		}

		tasks.Sort ((p1, p2) => p1.startTime.CompareTo (p2.startTime));

		//Allocate answer tasks into processors
		foreach (Task t in tasks) {
			int proTotalTime = transform.GetChild (t.processor - 1).GetComponent<Processor> ().calculateTotalTime ();
			if (proTotalTime < t.startTime) {
				Task dur = Instantiate (duration);
				dur.transform.SetParent (transform.GetChild (t.processor - 1));
				dur.transform.SetAsFirstSibling();
				dur.weight = t.startTime - proTotalTime;
				dur.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (60, dur.weight * 10);
			}
			Slot s = Instantiate (slot);
			s.transform.SetParent(transform.GetChild (t.processor - 1));
			s.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (60, t.weight * 10);
			s.transform.SetAsFirstSibling();
			s.isTaskPool = true;
			s.active = false;
			s.depFree = false;
			s.isTaskPool = false;
			t.answer = true;
			t.transform.SetParent (s.transform);
		}
		foreach (GameObject p in GameObject.FindGameObjectsWithTag("Processor")) {
			p.transform.GetComponent<Processor> ().calculateTotalTime ();
		}
		calculateTime (true);
	}
}
