using System.Collections;
using System.Collections.Generic;
using System.IO;  
using UnityEngine;

public class TaskPool : MonoBehaviour {
	public TextAsset[] dictionaryTasks;
	private string wholeFile;
	private List<string> eachLine;
	public Task task;
	private List<Task> taskSet = new List<Task> ();

	void Start(){
		//LoadTask (0);
		GameObject.Find ("Processor Pool").transform.GetComponent<ProcessorPool> ().loadAnswer(dictionaryTasks[0]);
	}

	void LoadTask(int textIndex){
		wholeFile = dictionaryTasks[textIndex].text;

		eachLine = new List<string> ();
		eachLine.AddRange (wholeFile.Split ("\n" [0]));

		//Iterate the file and instantiate task
		for (int i = 1; i < eachLine.Count; i++) {
			if (eachLine [i].Trim() == "}") {
				break;
			}

			string taskName = eachLine [i].Split ('[', ']') [0];
			string taskInfo = eachLine [i].Split ('[', ']') [1];
			string[] taskType = taskName.Split (' ');

			//Add Task or dependence
			if (taskType.Length == 2) {
				Task t = Task.Instantiate (task);
				t.transform.SetParent (this.transform);
				t.taskName = taskType[0];
				t.weight = int.Parse (taskInfo.Replace ("Weight=", ""));
				t.taskColor = Random.ColorHSV (0f, 1f, 1f, 1f, 0.5f, 1f);
				t.answer = false;
				taskSet.Add (t);
			} else if (taskType.Length == 4) {
				foreach (Task t in taskSet) {
					if (t.taskName == taskType [2]) {
						t.addDependence (taskType [0], int.Parse (taskInfo.Replace ("Weight=", "")));
						break;
					}
				}
			}
		}


	}

}
