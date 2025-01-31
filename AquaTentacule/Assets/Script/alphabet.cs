﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class alphabet : MonoBehaviour {

	public static alphabet instance = null;
	public GameObject[] alpha;
	public List<string> speech;
	public Transform target;
	private Dictionary<char,GameObject> alphaDictionary = new Dictionary<char,GameObject>();
	private List<GameObject> instanciatesGameObjects = new List<GameObject> ();
	public float width;
	private bool next = true;

	void Awake ()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}


	void Start () {
		int i = 0;
		foreach (char c in "abcdefghijklmnopqrstuvwxyz.?!: ".ToCharArray()) {
			alphaDictionary.Add(c,alpha[i]);
			i++;
		}
	
	}
	void Update () {
		if (Input.GetKeyDown("space")&& next)
			StartCoroutine(showGeorgeBubble());
	
		float x = 0;
		foreach (GameObject gobj in instanciatesGameObjects) {
			float xy= target.position.x+x;
			gobj.transform.position = new Vector3(xy,target.position.y,target.position.z+1);
			x = width+x;
		}
	}
	
	public IEnumerator showGeorgeBubble(){
		next=false;
		int r = Random.Range(0,speech.Count-1);
		displayString(speech[r]);
		yield return new WaitForSeconds(2);
		foreach (GameObject gobj in instanciatesGameObjects) {
			Destroy(gobj);
		}
		instanciatesGameObjects.Clear();
		next =true;
	}



	private void displayString(string sentence){
		foreach (char c in sentence.ToCharArray()){
			if(alphaDictionary.ContainsKey(c)){
				GameObject go = Instantiate (alphaDictionary[c])as GameObject;
				instanciatesGameObjects.Add (go);
			}
		}
	}
}
