using System.Collections;
using System.Collections.Generic;
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

	public TaskPack[] taskPacks;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
