using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAnswer : MonoBehaviour {
	public TextAsset t;

	public void loadAnswer(){
		GameObject.Find ("Task Pool").transform.gameObject.SetActive (false);
		GameObject.Find ("TaskInfo").transform.gameObject.SetActive (false);
		GameObject.Find ("Processor Pool").transform.GetComponent<ProcessorPool> ().loadAnswer(t);
	}
}
