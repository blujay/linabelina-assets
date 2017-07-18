using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAudioClipOnTrigger : MonoBehaviour {

	public AudioClip[] ClipToPlay;

	// Use this for initialization
	void Start () {
		
	}
		

	void playSound(){
		GetComponent<AudioSource>().clip = ClipToPlay [Random.Range (0, ClipToPlay.Length)];
		GetComponent<AudioSource> ().Play ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			playSound();
			Debug.Log ("played sound");
		}
	}

}