using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class PickupObjects : MonoBehaviour {


	public List<Collider> TriggerList;
	public GameObject parentBone = null;
	public AudioClip pickupSound = null;
	public AudioClip pickupRewardSound = null;
	public AudioClip dropSound = null;
	public AudioClip rewardSound = null;
	public int collectablesTarget;
	public Text progressText;
	public GameObject box;
	public GameObject basket;

	private int collectablesNeeded;
	private int inPocket;
	private int inBasket;
	private int rewardsReleased;
	private bool overBasket;
	private GameObject ObjectToPick = null;
	private Animator playerAnim;
	private Animator boxAnim;
	private Transform nearestObject = null;
	private bool targetReached = false;
	private BoxScript boxScript;
	private ObjectScript objectScript;

	


	// Use this for initialization
	void Start () {
		playerAnim =  GetComponentInParent<Animator> ();
		boxAnim = box.GetComponentInChildren<Animator> ();
		collectablesNeeded = collectablesTarget;

	}
	
	// Update is called once per frame
	void Update () {
		nearestObject = FindNearestObject ();
		if (nearestObject) {
			//Debug.Log ("the nearest object to you is: " + nearestObject.gameObject.name);
		}
			
		if (Input.GetButtonUp ("Fire1")) {
			if (nearestObject) {
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

		OpenBox ();
	}

	void PickupObject(Transform nearestObject){
		float pickupHeight = nearestObject.position.y - transform.position.y;
		//Vector3 pickupDistance = nearestObject.transform.position - this.transform.position;
		//Debug.Log("pickup height = " + pickupHeight);
		//Debug.Log ("distance from Object = " + pickupDistance);
		playerAnim.SetFloat("PickupHeight", pickupHeight);
		ObjectToPick = nearestObject.gameObject;
		playerAnim.SetBool("pickup", true);
	}

	public void SetParent(){
		
		ObjectToPick.transform.parent = parentBone.transform;
		if (ObjectToPick.GetComponent<Rigidbody> ()) {
			ObjectToPick.GetComponent<Rigidbody> ().isKinematic = true;
			ObjectToPick.GetComponent<MeshCollider> ().enabled = false;
		}
		ObjectToPick.transform.localPosition = Vector3.zero;
		ObjectToPick.transform.localRotation = Quaternion.identity;


		if (ObjectToPick.gameObject.CompareTag ("reward")) {
			GetComponent<AudioSource>().PlayOneShot(pickupRewardSound, 1F);
		}
		if (ObjectToPick.gameObject.CompareTag ("collectable")) {
			GetComponent<AudioSource>().PlayOneShot(pickupSound, 1F);
		}
			
	}


	public void DestroyObject(){
		//check if there is an object picked up

		if (ObjectToPick){
			//if the object selected is a reward like a biscuit then destroy it and play a 'reward' sound when it reaches the pocket
			//Debug.Log ("object to pick is: " + ObjectToPick.gameObject.name);
			if (targetReached && ObjectToPick.gameObject.CompareTag ("reward")) {
				GetComponent<AudioSource>().PlayOneShot(rewardSound, 1F);
				Destroy (ObjectToPick);
				rewardsReleased = +1;
				Debug.Log ("biscuits rewarded = " + rewardsReleased);
			}
			//if the object selected is a collectable like an 'egg' then destroy it and play a 'collectable' sound when it reaches the pocket
			if (ObjectToPick.gameObject.CompareTag ("collectable")) {
				GetComponent<AudioSource>().PlayOneShot(dropSound, 1F);
				//check if the collider list still has this object in and remove it if it does
				if (TriggerList.Contains (ObjectToPick.GetComponent<MeshCollider>())) {
					TriggerList.Remove (ObjectToPick.GetComponent<MeshCollider>());
				}
				//put the object in pocket and make it vanish
				Destroy (ObjectToPick);
				inPocket += 1;
				Debug.Log ("objects in pocket= " + inPocket);
				collectablesNeeded -= 1;

			}
			ObjectToPick = null;
			nearestObject = null;
			playerAnim.SetBool("pickup", false);
			}
		}
		
	
	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("collectable")) {
			//Debug.Log ("I am a collectable object so look at me!: " + other.gameObject.name);
			//add the object to the trigger list only if it's a collectable or reward
			if (!TriggerList.Contains (other) && other.gameObject.CompareTag ("collectable") || other.gameObject.CompareTag ("reward")) {
				TriggerList.Add (other);

				//highlight the object so that we can see it
				if (other.GetComponent <ObjectScript> ()) {
					other.GetComponent<ObjectScript> ().HighlightOn ();
				}
			}
		} else if (other == basket) {
			//Debug.Log ("I am a basket  - I have this many eggs in me: " + inBasket);
			overBasket = true;
			//Debug.Log("adding " + other);
		} else {
			//Debug.Log ("I am not a collectable object - ignore me please!: " + other.gameObject.name);
		}
	}

	Transform FindNearestObject(){
		Transform nearestObject = null;
		float minDist = Mathf.Infinity;
		Vector3 currentPos = transform.position;
		foreach (Collider Object in TriggerList) {
			float dist = Vector3.Distance (Object.transform.position, currentPos);
			if (dist < minDist && Object.GetComponent<ObjectScript> ()) {
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
			if (other.GetComponent <ObjectScript> ()) {
				other.GetComponent<ObjectScript> ().HighlightOff ();
			}

			//Debug.Log("removing " + other);
		}
	}

	void dropObject(){
		if (inPocket > 0 && !playerAnim.GetBool("drop")) {
			playerAnim.SetBool ("drop", true);
			if (overBasket) {
				inBasket += 1;
				inPocket -= 1;
				Debug.Log ("eggs in basket = " + inBasket);

			}
		} 	
	}

	void OpenBox(){
		if (inPocket == 0 && targetReached == true) {
			Debug.Log ("yeah! magic box now has to open");
			boxAnim.SetBool ("BoxOpen", true);
			rewardsReleased = 0;
		}
	}
		
}
		
	