using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SubmitAnswer : MonoBehaviour {
	public GameObject processorPool;
	public GameObject taskPool;
	public GameObject taskInfo;
	public Text submitButton;
	bool submitted;
	ProcessorPool pp;


	void start(){
		submitted = false;
	}

	public void submit(){
		if (!submitted) {
			int gameIndex = GameObject.Find ("ResourceManager").transform.GetComponent<ResourceManage> ().gameIndex;
			pp = Instantiate (processorPool).GetComponent<ProcessorPool>();
			pp.transform.SetParent (GameObject.Find ("Panel Grider").transform);
			pp.loadAnswer (GameObject.Find("ResourceManager").GetComponent<ResourceManage>().getGame(gameIndex));
			taskPool.SetActive (false);
			taskInfo.transform.SetAsLastSibling ();
			submitButton.text = "Try Again";
			submitted = true;
		} else {
			Destroy (pp.gameObject);
			taskPool.SetActive (true);
			submitted = false;
			submitButton.text = "Submit";
		}

	}
}
