using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class PickupObjects : MonoBehaviour {


	public List<Collider> TriggerList;
	public GameObject parentBone = null;
	public AudioClip pickupSound = null;
	public AudioClip dropSound = null;
	public int collectablesTarget;
	public Text progressText;
	public GameObject box;
	public GameObject basket;
	public ParticleSystem glitter;

	private int collectablesNeeded;
	private int inPocket;
	private bool overBasket;
	private GameObject ObjectToPick = null;
	private Animator playerAnim;
	private Animator boxAnim;
	private Transform nearestObject = null;
	private bool targetReached = false;
	private int rewardRelease;

	


	// Use this for initialization
	void Start () {
		//handholder = GameObject.Find("HandHolder") as GameObject;
		playerAnim =  GetComponentInParent<Animator> ();
		boxAnim = box.GetComponentInChildren<Animator> ();
		collectablesNeeded = collectablesTarget;

	}
	
	// Update is called once per frame
	void Update () {
		nearestObject = FindNearestObject();

		if (nearestObject) {
			nearestObject.GetComponent<ObjectScript> ().HighlightOn ();

			if(Input.GetButton("Fire1")) {
				PickupObject (nearestObject);
				//Debug.Log ("clicked mouse");
			}

		}

		if(Input.GetKeyUp(KeyCode.R)) {
			dropObject ();
		}



		progressText.text = collectablesNeeded.ToString();
		if (collectablesNeeded == 0) {
			targetReached = true;
		}
	}

	void PickupObject(Transform nearestObject){
		playerAnim.SetTrigger("pickup");
		float pickupHeight = nearestObject.position.y - transform.position.y;
		//Vector3 pickupDistance = nearestObject.transform.position - this.transform.position;
		//Debug.Log("pickup height = " + pickupHeight);
		//Debug.Log ("distance from Object = " + pickupDistance);
		playerAnim.SetFloat("PickupHeight", pickupHeight);
		ObjectToPick = nearestObject.gameObject;
	}

	public void SetParent(){
		ObjectToPick.transform.parent = parentBone.transform;
		ObjectToPick.transform.localPosition = Vector3.zero;
		ObjectToPick.transform.localRotation = Quaternion.identity;
		GetComponent<AudioSource>().PlayOneShot(pickupSound, 1F);
		}


	public void DestroyObject(){
		if (ObjectToPick){
			GetComponent<AudioSource>().PlayOneShot(dropSound, 1F);
			Destroy (ObjectToPick.gameObject);
			ObjectToPick = null;
			nearestObject = null;
			collectablesNeeded -= 1;
			inPocket += 1;
			Debug.Log ("objects in pocket= " + inPocket);
			}
		}
		
	
	void OnTriggerEnter(Collider other){
		if(!TriggerList.Contains(other) && other.gameObject.CompareTag("collectable")){
			TriggerList.Add(other);
			//Debug.Log("adding " + other);
		} if(!TriggerList.Contains(other) && other.gameObject == basket){
			Debug.Log ("eggs in basket = " + rewardRelease);
			overBasket = true;
			
		}
	}

	Transform FindNearestObject(){
		Transform nearestObject = null;
		float minDist = Mathf.Infinity;
		Vector3 currentPos = transform.position;
		foreach (Collider Object in TriggerList){
			float dist = Vector3.Distance(Object.transform.position, currentPos);
			if (dist < minDist){
				nearestObject = Object.transform;
				minDist = dist;
			}
		}
		return nearestObject;

	}
		
	void OnTriggerExit(Collider other){
		if (TriggerList.Contains (other)) {
			//remove it from the list
			TriggerList.Remove (other);
			other.GetComponent<ObjectScript> ().HighlightOff ();
			//Debug.Log("removing " + other);
		}
	}

	void dropObject(){
		if (inPocket > 0 && !playerAnim.GetBool("drop")) {
			playerAnim.SetBool ("drop", true);
			if (overBasket) {
				rewardRelease += 1;
				inPocket -= 1;
				Debug.Log ("eggs in basket = " + rewardRelease);
				if (inPocket == 0) {
					boxAnim.SetBool ("BoxOpen", true);
					releaseParticles ();
				} 
			}
		} 	
	}

	void releaseParticles(){
		Instantiate (glitter, box.transform.position, Quaternion.identity);
	}
}
		
	