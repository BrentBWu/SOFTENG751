using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InitPageSection : MonoBehaviour {
	public GameObject pageSection;

	// Use this for initialization
	void Awake () {
		int pageNum = GameObject.Find ("ResourceManager").GetComponent<ResourceManage> ().getSlide ().Length;
		for (int i = 0; i < pageNum; i++) {
			GameObject ps = Instantiate (pageSection);
			ps.transform.SetParent (transform);
			ps.transform.GetComponent<Button> ().onClick.AddListener (() => {
				setPage();
			});
		}
	}

	void setPage(){
		foreach(GameObject sr in GameObject.FindGameObjectsWithTag("LecturePanel")){
			sr.GetComponent<ScrollSnapRect>().LerpToPage(EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex());
		} 
	}
}
