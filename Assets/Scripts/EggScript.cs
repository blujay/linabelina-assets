using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EggScript : MonoBehaviour {

	public GameObject parentBone;
	public Rigidbody rigid;


	// Use this for initialization
	void Start () {
		transform.parent = parentBone.transform;
		rigid.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ReleaseMe () {
		transform.parent = null;
		rigid.useGravity = true;
		//transform.rotation = parentBone.transform.rotation;
		//rigid.AddForce (transform.forward * 20000);

	}
}
