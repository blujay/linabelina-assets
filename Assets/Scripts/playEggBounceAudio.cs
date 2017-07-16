using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playEggBounceAudio : MonoBehaviour {

	public AudioClip EggBounce;

	// Use this for initialization
	void Start () {
		
	}

	void PlayFootsteps(){
		GetComponent<AudioSource>().clip = EggBounce;
		GetComponent<AudioSource> ().Play ();
	}
}
