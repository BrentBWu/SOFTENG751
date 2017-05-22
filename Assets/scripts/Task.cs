using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour {

	public string taskName;
	public int weight;
	public string dependenceName;
	public int dependenceWeight;
	public Color32 taskColor;

	// Use this for initialization
	void Start () {
		this.GetComponent<Image> ().color = taskColor;
		this.transform.GetChild (0).GetComponent<Text> ().text = taskName;
		transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (60, weight * 10);
	}
	
	public void addDependence(string name, int weight){
		dependenceName = name;
		dependenceWeight = weight;
	}

	public string getDependenceList(){
		string dep = "\n" + dependenceName + " (Transfer Time:" + dependenceWeight + ")";
		return dep;
	}


}
