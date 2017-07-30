using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EggScript : MonoBehaviour {

	public GameObject parentBone;
	public Rigidbody rigid;


	// Use this for initialization
	void Start () {
		transform.parent = parentBone.transform;
		transform.localPosition = Vector3.zero;
		GetComponent<Rigidbody> ().useGravity = false;
		GetComponent<MeshCollider> ().enabled = false;
	}

	// Update is called once per frame
	void Update () {

	}

	public void ReleaseMe () {
		transform.parent = null;
		GetComponent<Rigidbody> ().useGravity = true;
		GetComponent<MeshCollider> ().enabled = true;

	}
}