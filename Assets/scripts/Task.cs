using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour {

	public string taskName;
	public int weight;
	private List<Dependence> dependencies = new List<Dependence>();
	public Color32 taskColor;

	// Use this for initialization
	void Start () {
		this.GetComponent<Image> ().color = taskColor;
	}
	
	public void addDependence(string name, int weight){
		Dependence dep = new Dependence ();
		dep.name = name;
		dep.weight = weight;
		dependencies.Add (dep);
	}

	class Dependence{
		public string name;
		public int weight;
	}


}
