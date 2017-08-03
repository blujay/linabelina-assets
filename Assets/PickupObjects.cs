using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObjects : MonoBehaviour {

	//private GameObject handholder;
	private Animator animator;

	// Use this for initialization
	void Start () {
		//handholder = GameObject.Find("HandHolder") as GameObject;
		animator =  GetComponentInParent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.R)) {
			print("mouse pressed");
			animator.SetBool ("reaching", true);
		} else{
			animator.SetBool("reaching",false);
		}
		if (Input.GetKey(KeyCode.S)) {
			print("S pressed");
			animator.SetBool ("waving", true);
		} else{
			animator.SetBool("waving",false);
		}

	}

	void OnCollisionEnter(Collision col) {
		if(col.transform.tag == "collectable")
		{
			Debug.Log ("collided with " + col.gameObject.name);
			//animator.SetTrigger("collect");
		}
	}
		
}
