using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SlideLoader : MonoBehaviour {
	public GameObject slideField;

	// Use this for initialization
	void Awake () {
		Sprite[] slides = GameObject.Find ("ResourceManager").transform.GetComponent<ResourceManage> ().getSlide();
		foreach (Sprite t in slides ) {
			GameObject tf = Instantiate (slideField);
			tf.transform.SetParent (transform);
			tf.transform.GetComponent<Image> ().sprite = t;
		}

	}
}
