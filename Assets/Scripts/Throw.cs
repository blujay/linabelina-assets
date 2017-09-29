using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {

	public GameObject throwPrefab;
	private ObjectScript objectScript;
	private Animator playerAnim;

	void Start(){
		playerAnim = GetComponent<Animator>();
	}


	void CloneObject(){
		//Debug.Log("Cloning");
		playerAnim.SetBool ("drop", true);
		throwPrefab.GetComponent <ObjectScript> ().CloneMe();
	}

	void ThrowObject () {
		throwPrefab.GetComponent <ObjectScript> ().ReleaseMe();
		playerAnim.SetBool ("drop", false);
	}



	}


