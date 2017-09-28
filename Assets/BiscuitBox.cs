using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiscuitBox : MonoBehaviour {

	private PickupObjects contentCount;
	private AudioSource audiosource;

	// Use this for initialization
	void Start () {
		audiosource = GetComponentInChildren<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		}

	void CloseBox (){
		
	}
}