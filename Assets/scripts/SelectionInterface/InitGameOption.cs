using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitGameOption : MonoBehaviour {
	public GameObject gameOption;

	// Use this for initialization
	void Start () {
		TextAsset[] games = GameObject.Find ("ResourceManager").GetComponent<ResourceManage> ().games;


		foreach (TextAsset t in games) {
			GameObject option = Instantiate (gameOption);
			option.transform.SetParent (transform);
			option.transform.Find ("Text").GetComponent<Text> ().text = t.name;
			option.GetComponent<Button>().onClick.AddListener (() => { 
				GameObject.Find ("ResourceManager").GetComponent<ResourceManage> ().setGameIndex(option.transform.GetSiblingIndex());
			});
			option.GetComponent<Button> ().onClick.AddListener (() => {
				GameObject.Find ("Main Camera").GetComponent<LoadScene> ().loadScene ("Game");
			});
		}
	}

}
