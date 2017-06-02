using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitQuizOption : MonoBehaviour {
	public GameObject quizOption;

	// Use this for initialization
	void Start () {
		TextAsset[] quizs = GameObject.Find ("ResourceManager").GetComponent<ResourceManage> ().quizs;

		foreach (TextAsset t in quizs) {
			GameObject option = Instantiate (quizOption);
			option.transform.SetParent (transform);
			option.transform.Find ("Text").GetComponent<Text> ().text = t.name;
			option.GetComponent<Button>().onClick.AddListener (() => { 
				GameObject.Find ("ResourceManager").GetComponent<ResourceManage> ().setQuizStartIndex(option.transform.GetSiblingIndex());
			});
			option.GetComponent<Button>().onClick.AddListener (() => { 
				GameObject.Find ("ResourceManager").GetComponent<ResourceManage> ().setQuizEndIndex(option.transform.GetSiblingIndex());
			});
			option.GetComponent<Button>().onClick.AddListener (() => { 
				GameObject.Find ("ResourceManager").GetComponent<ResourceManage>().innerLecture = false;
			});
			option.GetComponent<Button> ().onClick.AddListener (() => {
				GameObject.Find ("Main Camera").GetComponent<LoadScene> ().loadScene ("Quiz");
			});
		}
	}
}
