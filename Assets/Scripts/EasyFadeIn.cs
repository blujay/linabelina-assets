using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyFadeIn : MonoBehaviour {

	public int approxSecondsToFade = 15;

	// Use this for initialization


	void FixedUpdate () {
		if (GetComponent<AudioSource> ().volume < 1) {
			GetComponent<AudioSource> ().volume = GetComponent<AudioSource> ().volume + (Time.deltaTime / (approxSecondsToFade + 1));
		}
		else {
			Destroy (this);
		}
	}
}