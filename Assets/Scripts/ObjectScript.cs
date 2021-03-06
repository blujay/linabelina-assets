﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectScript : MonoBehaviour {

	public GameObject throwPrefab;
	private GameObject parentBone;
	private GameObject handheldObject;
	private Color initialColor;
	public Animator playAnim;


	// Use this for initialization
	void Start () {
		initialColor = gameObject.GetComponent<Renderer>().material.color;
	}

	public void CloneMe(){
		parentBone = GameObject.Find("HandHolder");
		handheldObject = Instantiate (throwPrefab, parentBone.transform) as GameObject;
		handheldObject.name = "Handheld object";
		Debug.Log ("cloned");
		handheldObject.transform.localPosition = Vector3.zero;
		handheldObject.GetComponent<MeshCollider> ().enabled = false;
		handheldObject.GetComponent<Rigidbody> ().useGravity = false;

	}

	public void ReleaseMe () {
		handheldObject.transform.parent = null;
		handheldObject.GetComponent<Rigidbody> ().useGravity = true;
		handheldObject.GetComponent<MeshCollider> ().enabled = true;

	}

	public void HighlightOn(){
			gameObject.GetComponent<Renderer>().material.color = Color.white;
	}

	
	public void HighlightOff() {
			gameObject.GetComponent<Renderer>().material.color = initialColor;
	}
}