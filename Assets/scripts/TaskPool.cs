using System.Collections;
using System.Collections.Generic;
using System.IO;  
using UnityEngine;

public class TaskPool : MonoBehaviour {
	public TextAsset[] dictionaryTasks;
	private string wholeFile;
	private List<string> eachLine;
	public Task task;

	void Start(){
		LoadTask (0);
	}

	void LoadTask(int textIndex){
		wholeFile = dictionaryTasks[textIndex].text;

		eachLine = new List<string> ();
		eachLine.AddRange (wholeFile.Split ("\n" [0]));

		//Iterate the file and instantiate task
		for (int i = 0; i < eachLine.Count; i++) {
			string taskName = eachLine [i].Split ('[', ']') [0];
			string taskInfo = eachLine [i].Split ('[', ']') [1];
			string[] taskType = taskName.Split (' ');
			Debug.Log (taskInfo.Replace("Weight=",""));

			if (taskType.Length == 2) {
				Task t = Task.Instantiate (task);
				t.transform.SetParent(this.transform);
				t.weight = int.Parse (taskInfo.Replace ("Weight=", ""));
				t.taskColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
				//t.weight = 
			}else if(taskType.Length == 4){
				
			}
			//eachLine [i].Split (" ");
			//Task task = new Task ();
			//task.name = 
			//task.weight = 
			//task.communicationTime = 
			//task.taskColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
		}
	}

}
