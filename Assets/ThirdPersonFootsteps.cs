using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonFootsteps : MonoBehaviour {

	public AudioClip[] Footsteps;

	// Use this for initialization
	void Start () {
		
	}

	void PlayFootsteps(){
		GetComponent<AudioSource>().clip = Footsteps [Random.Range (0, Footsteps.Length)];
		GetComponent<AudioSource> ().Play ();
	}

}
