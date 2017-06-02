using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour {
	string sceneName;

	// Use this for initialization
	void Start () {
		if (GameObject.Find ("ResourceManager").GetComponent<ResourceManage> ().innerLecture) {
			sceneName = "Lecture";
		} else {
			sceneName = "ModeSelection";
		}
		transform.GetComponent<Button>().onClick.AddListener (() => { 
			GameObject.Find ("Main Camera").GetComponent<LoadScene>().loadScene(sceneName);
		});
	}

}
