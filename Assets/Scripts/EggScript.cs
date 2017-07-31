using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EggScript : MonoBehaviour {

	public GameObject EggPrefab;
	private GameObject parentBone;



	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

	}

	public void CloneMe(){
		parentBone = GameObject.Find("HandHolder");
		GameObject myEgg = Instantiate (EggPrefab, parentBone.transform) as GameObject;
		Debug.Log ("cloned");
		myEgg.name = "Current Clone";
		myEgg.transform.localPosition = Vector3.zero;
		myEgg.GetComponent<MeshCollider> ().enabled = false;
		myEgg.GetComponent<Rigidbody> ().useGravity = false;

	}

	public void ReleaseMe () {
		GetComponent<Rigidbody> ().useGravity = true;
		GetComponent<MeshCollider> ().enabled = true;
		transform.parent = null;
		this.name = "Egg";

	}
}