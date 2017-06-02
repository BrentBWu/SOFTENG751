using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScrollRect : MonoBehaviour {
	public GameObject game;
	public GameObject quiz;

	public void enableButton(bool enable){
		game.SetActive (enable);
		quiz.SetActive (enable);
	}
}
