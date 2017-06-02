using System.Collections;
using System.Collections.Generic;
using System.IO;  
using UnityEngine;

public class TaskPool : MonoBehaviour {
	private string wholeFile;
	private List<string> eachLine;
	public Task task;
	private List<Task> taskSet = new List<Task> ();
	private Color32[] colorPool = {new Color32(128,192,206,255), new Color32(154,222,162,255), new Color32(255,141,104,255), new Color32(255,236,186,255), new Color32(234,231,237,255), 
		new Color32(183,147,230,255), new Color32(255,251,133,255), new Color32(194,246,155,255), new Color32(250,91,117,255), new Color32(71,151,177,255), 
		new Color32(195,190,240,255), new Color32(95,189,197,255), new Color32(252,138,21,255), new Color32(216,217,92,255), new Color32(204,168,233,255) };

	void Start(){
		int gameIndex = GameObject.Find ("ResourceManager").transform.GetComponent<ResourceManage> ().gameIndex;
		LoadTask (GameObject.Find("ResourceManager").GetComponent<ResourceManage>().getGame(gameIndex));
	}
		

	void LoadTask(TextAsset game){
		wholeFile = game.text;

		eachLine = new List<string> ();
		eachLine.AddRange (wholeFile.Split ("\n" [0]));
		int processorNum = int.Parse (eachLine [0].Split (' ') [1]);
		GameObject.Find ("Processor Pool").transform.GetComponent<ProcessorPool> ().initProcessor(processorNum);

		int colorIndex = 0;

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
				t.taskColor = colorPool[colorIndex];
				colorIndex++;
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
