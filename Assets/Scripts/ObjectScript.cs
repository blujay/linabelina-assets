using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectScript : MonoBehaviour {

	public GameObject dropPrefab;
	private GameObject parentBone;
	private GameObject handheldObject;
	private Color initialColor;


	// Use this for initialization
	void Start () {
		initialColor = gameObject.GetComponent<Renderer>().material.color;
	}

	public void CloneMe(){
		//creates an instance of the object in the player's hand when the animation event reaches the point where the player's hand reaches inside the pocket
		parentBone = GameObject.Find("HandHolder");
		handheldObject = Instantiate (dropPrefab, parentBone.transform) as GameObject;
		handheldObject.GetComponent<MeshCollider> ().enabled = false;
		handheldObject.GetComponent<Rigidbody> ().useGravity = false;
		handheldObject.name = "Handheld object";
		Debug.Log ("cloned");

	}

	public void ReleaseMe () {
		//releases the object from the hand when the animation event is triggered when the hand is fully extended
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