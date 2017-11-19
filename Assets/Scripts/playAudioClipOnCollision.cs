using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAudioClipOnCollision : MonoBehaviour {

	public AudioClip[] ClipToPlay;
	public string GameTag;
	bool played = false;

	// Use this for initialization
	void Start () {
		
	}
		

	void playSound(){
		GetComponent<AudioSource>().clip = ClipToPlay [Random.Range (0, ClipToPlay.Length)];
		if (!played) {
			GetComponent<AudioSource> ().Play ();
			played = true;
		} else {
			played = false;
		}

	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == GameTag) {
			playSound();
			//Debug.Log ("collided with " + GameTag);
		}
	}

}