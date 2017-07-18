using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAudioClipOnCollision : MonoBehaviour {

	public AudioClip[] ClipToPlay;
	public string[] GameTag;

	// Use this for initialization
	void Start () {
		
	}
		

	void playSound(){
		GetComponent<AudioSource>().clip = ClipToPlay [Random.Range (0, ClipToPlay.Length)];
		GetComponent<AudioSource> ().Play();
	}

	void OnCollisionEnter(Collision collision) {
		foreach(var tag in GameTag){
			playSound();
			Debug.Log ("played collision sound");

		}
	}

}