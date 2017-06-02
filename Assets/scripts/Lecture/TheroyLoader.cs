using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TheroyLoader : MonoBehaviour {
	public GameObject theoryField;

	// Use this for initialization
	void Awake () {
		TextAsset[] theories = GameObject.Find ("ResourceManager").transform.GetComponent<ResourceManage> ().getTheory ();
		foreach (TextAsset t in theories) {
			GameObject tf = Instantiate (theoryField);
			tf.transform.SetParent (transform);
			tf.transform.Find ("Text").GetComponent<Text> ().text = t.text;
		}

	}

}
