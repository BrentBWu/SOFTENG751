using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResourceManage : MonoBehaviour {

	public Object[] loader;
	public TextAsset[] games;

	// Use this for initialization
	void Start () {
		loader = Resources.LoadAll("game", typeof(TextAsset));
		games = new TextAsset[loader.Length];

		for(int i = 0; i < loader.Length; i++){
			games [i] = (TextAsset)loader [i];
		}
	}

	public TextAsset getGame(int index){
		return games [index];
	}

}
