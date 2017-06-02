using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScrollRect : MonoBehaviour {
	public GameObject game;
	public GameObject quiz;

	public void enableButton(bool enable){
		ResourceManage rm = GameObject.Find ("ResourceManager").transform.GetComponent<ResourceManage> ();
		if (rm.gameInclude) {
			game.SetActive (enable);
		}

		if (rm.quizInclude) {
			quiz.SetActive (enable);
		}

	}
}
