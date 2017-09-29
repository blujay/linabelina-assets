using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour {

	public GameObject rewardPrefab;
	public Transform spawnPoint;
	public GameObject particles;
	public AudioClip releaseSound;
	public AudioClip gaspSound;
	private AudioSource audio;
	private PickupObjects pickupScript;


	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void releaseParticles(){
		GameObject particlesInstance = Instantiate (particles, spawnPoint.transform) as GameObject;
		particles.transform.localPosition = Vector3.zero;

	}

	void releaseReward (){
		GameObject rewardObject = Instantiate (rewardPrefab, spawnPoint.transform) as GameObject;
		//rewardObject.transform.localPosition = Vector3.zero;
		audio.PlayOneShot (releaseSound);
		audio.PlayOneShot (gaspSound);
	}
		
}
