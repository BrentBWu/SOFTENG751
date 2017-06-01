using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResourceManage : MonoBehaviour {

	public Object[] loader;
	public TextAsset[] games;
	public TextAsset[] quizs;

	// Use this for initialization
	void Start () {
		loadGame ();
		loadQuiz ();
		DontDestroyOnLoad (transform.gameObject);
	}

	void loadGame(){
		loader = Resources.LoadAll("game", typeof(TextAsset));
		games = new TextAsset[loader.Length];

		for(int i = 0; i < loader.Length; i++){
			games [i] = (TextAsset)loader [i];
		}
	}

	public TextAsset getGame(int index){
		return games [index];
	}

	void loadQuiz(){
		loader = Resources.LoadAll("quiz", typeof(TextAsset));
		quizs = new TextAsset[loader.Length];

		for(int i = 0; i < loader.Length; i++){
			quizs [i] = (TextAsset)loader [i];
		}
	}

	public TextAsset[] getQuiz(){
		return quizs;
	}

}
