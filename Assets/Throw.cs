using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {

	public GameObject theObject;
	private GameObject newObject;
	private EggScript objectScript;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CloneObject(){
		Debug.Log("Cloning");
		theObject.GetComponent <EggScript> ().CloneMe();
	}

	void ThrowObject () {
		Debug.Log("Throwiiing");
		newObject = GameObject.Find ("Current Clone");
		newObject.GetComponent <EggScript> ().ReleaseMe();
	}
}
