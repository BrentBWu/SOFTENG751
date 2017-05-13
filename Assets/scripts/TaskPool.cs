using System.Collections;
using System.Collections.Generic;
using System.IO;  
using UnityEngine;

public class TaskPool : MonoBehaviour {

	[System.Serializable]
	public class TaskPack
	{
		// Unused variable elsewhere, just allows for organizing in the editor (hence private).
		[SerializeField]
		private string name;
		public GameObject[] tasks;
	}

	private List<TaskPack> taskPacks;
	public TextAsset[] dictionaryTasks;
	private string wholeFile;
	private List<string> eachLine;

	void Start(){
		
	}

	void LoadTask(int textIndex){
		TaskPack pack = new TaskPack ();
		wholeFile = dictionaryTasks[textIndex].text;

		eachLine = new List<string> ();
		eachLine.AddRange (wholeFile.Split ("\n" [0]));

		for (int i = 0; i < eachLine.Count; i++) {
			//eachLine [i].Split (" ");
			Task task = new Task ();
			//task.name = 
			//task.weight = 
			//task.communicationTime = 
			//task.taskColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
		}
	}


}
