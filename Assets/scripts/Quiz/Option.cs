using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour {

	public bool chose;

	void start(){
		chose = false;
	}

	public void optionChosen(){
		foreach (Transform child in GameObject.Find("Option Panel").transform) {
			child.transform.GetComponent<Option> ().chose = false;
			child.transform.Find ("Button Layer").transform.GetComponent<Button> ().enabled = false;
		}
		this.chose = true;

		GameObject.Find ("GameManager").GetComponent<GameManager> ().checkAnswer();
	}
}
