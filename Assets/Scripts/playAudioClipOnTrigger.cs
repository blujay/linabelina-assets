using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAudioClipOnTrigger : MonoBehaviour {



	public AudioClip ClipToPlay;
	private AudioSource source;
	bool played = false;
	bool stopping = false;
	public int secondsToFadeIn = 10;
	public int secondsToFadeOut = 7;
	//private float timeRemaining;


	//  This script will start to play an audio clip it enters a collider acting as a trigger. 
	//  It will play clip and fade in from 0 on entering, then fade out again when re-entering (like leaving through same door you entered)
	//  It can blend between colliders and fades out when passing back through.
	//  Designed for putting in a game where a player can choose different routes to take at a fork in the road or similar. 
	//  Place this script on a game object with an audio source. Put an audio clip into the ClipToPlay field 


	void Start () {
		source = GetComponent<AudioSource> () as AudioSource;
		source.clip = ClipToPlay;

	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && !played) {
			//Debug.Log ("trigger entered");
			source.Play ();
			played = true;
			//timeRemaining = timeRemaining - Time.deltaTime;
			//Debug.Log ("played = " + played);

		} else if(other.tag == "Player" && played) {
			played = false;
			stopping = true;
			//Debug.Log ("stopping activated");
		}
	}


	void FixedUpdate () {
		if (played) {
			if (source.volume < 1) {
				//source.volume = timeRemaining/secondsToFadeIn;
				source.volume = source.volume + (Time.deltaTime / (secondsToFadeIn + 1));
			} 
		} 

		if (stopping) {
			//Debug.Log ("fading out...");
			source.volume = source.volume - (Time.deltaTime / (secondsToFadeOut + 1));
		} 

		if (stopping && source.volume == 0) {
			source.Stop ();
			stopping = false;
			played = false;
			//Debug.Log ("stopped");
		}
	}
}