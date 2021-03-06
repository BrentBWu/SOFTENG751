﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResourceManage : MonoBehaviour {

	public Object[] loader;
	public TextAsset[] games;
	public TextAsset[] quizs;
	public TextAsset[] theories;
	public Sprite[] slides;
	public int startIndex, endIndex;
	public int gameIndex;
	public int lectureStartIndex, lectureEndIndex;
	public bool innerLecture, gameInclude, quizInclude;

	// Use this for initialization
	void Start () {
		loadGame ();
		loadQuiz ();
		loadSilde ();
		loadTheory ();
		if (GameObject.FindGameObjectsWithTag ("ResourceManager").Length < 2) {
			DontDestroyOnLoad (transform.gameObject);
		}
	}

	//Load game data from resource/game
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

	//Load quiz data from resource/quiz
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

	//Load quiz data from resource/slide
	void loadSilde(){
		loader = Resources.LoadAll("slide", typeof(Sprite));
		slides = new Sprite[loader.Length];

		for(int i = 0; i < loader.Length; i++){
			slides [i] = (Sprite)loader [i];
		}
	}

	public Sprite[] getSlide(){
		Sprite[] returnSlide = new Sprite[lectureEndIndex - lectureStartIndex + 1];
		int j = 0;
		for (int i = lectureStartIndex; i < lectureEndIndex + 1; i++) {
			returnSlide [j] = slides [i];
			j++;
		}
		return returnSlide;
	}

	//Load quiz data from resource/theroy
	void loadTheory(){
		loader = Resources.LoadAll("theory", typeof(TextAsset));
		theories = new TextAsset[loader.Length];

		for(int i = 0; i < loader.Length; i++){
			theories [i] = (TextAsset)loader [i];
		}
	}

	public TextAsset[] getTheory(){
		TextAsset[] returnTheory = new TextAsset[lectureEndIndex - lectureStartIndex + 1];
		int j = 0;
		for (int i = lectureStartIndex; i < lectureEndIndex + 1; i++) {
			returnTheory  [j] = theories[i];
			j++;
		}
		return returnTheory ;
	}
		

	//Set the quiz to be instantiate
	public void setQuizStartIndex(int startIndex){
		this.startIndex = startIndex;
	}

	public void setQuizEndIndex(int endIndex){
		this.endIndex = endIndex;
	}

	//Set the game to be instantiate
	public void setGameIndex(int gameIndex){
		this.gameIndex = gameIndex;
	}

	//Set the lecture to be instantiate
	public void setLectureIndex(int startIndex, int endIndex){
		this.lectureStartIndex = startIndex;
		this.lectureEndIndex = endIndex;
	}

	public void setIncludeSection(bool gameInclude, bool quizInclude){
		this.gameInclude = gameInclude;
		this.quizInclude = quizInclude;
	}
		
}
