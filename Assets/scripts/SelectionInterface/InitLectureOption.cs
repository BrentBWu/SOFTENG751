using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InitLectureOption : MonoBehaviour {
	public int lectureStartIndex;
	public int lectureEndIndex;
	public int gameIndex;
	public int quizStartIndex, quizEndIndex;
	public bool gameInclude, quizInclude;

	// Use this for initialization
	void Start () {
		transform.GetComponent<Button>().onClick.AddListener (() => { 
			GameObject.Find ("ResourceManager").GetComponent<ResourceManage> ().setLectureIndex(lectureStartIndex, lectureEndIndex);
		});
		transform.GetComponent<Button>().onClick.AddListener (() => { 
			GameObject.Find ("ResourceManager").GetComponent<ResourceManage>().innerLecture = true;
		});
		transform.GetComponent<Button>().onClick.AddListener (() => { 
			GameObject.Find ("ResourceManager").GetComponent<ResourceManage>().setGameIndex(gameIndex);
		});
		transform.GetComponent<Button>().onClick.AddListener (() => { 
			GameObject.Find ("ResourceManager").GetComponent<ResourceManage>().setQuizStartIndex(quizStartIndex);
		});
		transform.GetComponent<Button>().onClick.AddListener (() => { 
			GameObject.Find ("ResourceManager").GetComponent<ResourceManage>().setQuizEndIndex(quizEndIndex);
		});
		transform.GetComponent<Button>().onClick.AddListener (() => { 
			GameObject.Find ("ResourceManager").GetComponent<ResourceManage>().setIncludeSection(gameInclude, quizInclude);
		});
		transform.GetComponent<Button> ().onClick.AddListener (() => {
			GameObject.Find ("Main Camera").GetComponent<LoadScene> ().loadScene ("Lecture");
		});
	}

}
