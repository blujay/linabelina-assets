using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour {

	public GameObject rewardPrefab;
	public Transform spawnPoint;
	public GameObject particles;
	public AudioClip releaseSound;
	public AudioClip gaspSound;
	private AudioSource audioS;
	public GameObject player;
	private int rewardTarget;

	// Use this for initialization
	void Start () {
		audioS = GetComponent<AudioSource> ();
		rewardTarget = player.gameObject.GetComponent<PickupObjects> ().collectablesTarget;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void releaseParticles(){
		GameObject particlesInstance = Instantiate (particles, spawnPoint.transform) as GameObject;
		particlesInstance.transform.localPosition = Vector3.zero;

	}

	void openBox (){
		audioS.PlayOneShot (gaspSound);
	}

	public void releaseReward(){
		for (int r = 1; r <= rewardTarget; r++) {
			GameObject rewardObject = Instantiate (rewardPrefab, spawnPoint.transform) as GameObject;
			rewardObject.transform.localPosition = Vector3.zero;
			rewardObject.GetComponent<Rigidbody> ().AddForce (this.transform.up);
		}



	}
}
