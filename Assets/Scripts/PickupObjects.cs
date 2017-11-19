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
		Debug.Log (nearestObject);
		//nearestObject.GetComponent<ObjectScript> ().HighlightOn ();


		if (Input.GetButtonUp ("Fire1")) {
			PickupObject (nearestObject);
			//Debug.Log ("clicked mouse");
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
		if (ObjectToPick){
			if (targetReached && ObjectToPick.gameObject.CompareTag ("reward")) {
				GetComponent<AudioSource>().PlayOneShot(rewardSound, 1F);
				Destroy (ObjectToPick.gameObject);
				rewardsReleased = +1;
				Debug.Log ("biscuits rewarded = " + rewardsReleased);
			}
			if (ObjectToPick.gameObject.CompareTag ("collectable")) {
				GetComponent<AudioSource>().PlayOneShot(dropSound, 1F);
				Destroy (ObjectToPick.gameObject);
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
		if(!TriggerList.Contains(other) && other.gameObject.CompareTag("collectable")|| other.gameObject.CompareTag("reward")){
			TriggerList.Add(other);
			if (other.GetComponent <ObjectScript> ()) {
				other.GetComponent<ObjectScript> ().HighlightOn ();
			}
			//Debug.Log("adding " + other);
		} if(!TriggerList.Contains(other) && other.gameObject == basket){
			Debug.Log ("eggs in basket = " + inBasket);
			overBasket = true;
			//Debug.Log("adding " + other);
		}
	}

	Transform FindNearestObject(){
		Transform nearestObject = null;
		float minDist = Mathf.Infinity;
		Vector3 currentPos = transform.position;
		foreach (Collider Object in TriggerList) {
			if (Object.GetComponent<MeshCollider> ()) {
				float dist = Vector3.Distance (Object.transform.position, currentPos);
				if (dist < minDist && Object.GetComponent<ObjectScript> ()) {
					nearestObject = Object.transform;
					minDist = dist;
				}
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
		
	