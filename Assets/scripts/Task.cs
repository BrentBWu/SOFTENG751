using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour {

	public int weight;
	public GameObject[] dependencies;
	public int communicationTime;
	public Color32 taskColor;

	// Use this for initialization
	void Start () {
		this.GetComponent<Image> ().color = taskColor;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
