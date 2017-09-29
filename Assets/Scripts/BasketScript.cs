using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketScript : MonoBehaviour {

	public AudioClip basketDropSound;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("collectable")){
			audioSource.PlayOneShot (basketDropSound, 1F);
		}
	}
}