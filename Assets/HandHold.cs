using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHold : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void onTriggerEnter(Collider other){
		Debug.Log ("collided with " + other.gameObject.name);
	}
}
