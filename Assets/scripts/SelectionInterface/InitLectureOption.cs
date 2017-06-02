using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InitLectureOption : MonoBehaviour {
	public int lectureStartIndex;
	public int lectureEndIndex;

	// Use this for initialization
	void Start () {
		transform.GetComponent<Button>().onClick.AddListener (() => { 
			GameObject.Find ("ResourceManager").GetComponent<ResourceManage> ().setLectureIndex(lectureStartIndex, lectureEndIndex);
		});
		transform.GetComponent<Button> ().onClick.AddListener (() => {
			GameObject.Find ("Main Camera").GetComponent<LoadScene> ().loadScene ("Lecture");
		});
	}

}
