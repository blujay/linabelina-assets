using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {

	public GameObject theObject;
	private EggScript objectScript;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ThrowObject () {
		Debug.Log("Throwiiing");
		theObject.GetComponent <EggScript> ().ReleaseMe();
	}
}
