using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCollector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if(hit.gameObject.CompareTag("Egg")) {
			GameObject.Destroy(hit.gameObject);
		}
	}
}
