using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class PickupEggs : MonoBehaviour {

	IEnumerator TurnTowards(Transform target)
	{
		Debug.Log("turning");
		int damping = 2;
		var lookPos = target.position - transform.position;
		lookPos.y = 0;
		Quaternion rotation = Quaternion.LookRotation(lookPos);	
		Debug.Log("turn " + rotation.eulerAngles.y);
		while (true) 
		{
			Debug.Log("=" + rotation.eulerAngles);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
			yield return null; 
		}
	}



	public List<Collider> TriggerList;
	public GameObject parentBone = null;
	public AudioClip pickupSound = null;
	public AudioClip dropSound = null;
	public int collectablesNeeded;
	public Text progressText;

	private int score;
	private GameObject eggToPick = null;
	private Animator animator;
	private Transform nearestEgg = null;

	


	// Use this for initialization
	void Start () {
		//handholder = GameObject.Find("HandHolder") as GameObject;
		animator =  GetComponentInParent<Animator> ();
		score = collectablesNeeded;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.R)) {
			animator.SetBool ("reaching", true);
		} else {
			animator.SetBool("reaching",false);
		}
		//if (Input.GetKey(KeyCode.W)) {
			//animator.SetBool ("waving", true);
			//Debug.Log ("waving");
		//} else{
			//animator.SetBool("waving",false);
		//}
		nearestEgg = FindNearestEgg();

		if (nearestEgg) {
			nearestEgg.GetComponent<ObjectScript> ().HighlightOn ();
			if(Input.GetButton("Fire1")) {
				PickupEgg (nearestEgg);
			}
			if (Input.GetKey (KeyCode.T)) {
				//Debug.Log("starting to turn");
				//StartCoroutine(TurnTowards(nearestEgg));
			}
		}
		progressText.text = score.ToString();
	}

	void PickupEgg(Transform nearestEgg){
		animator.SetTrigger("pickup");
		float pickupHeight = nearestEgg.position.y - transform.position.y;
		Vector3 pickupDistance = nearestEgg.transform.position - this.transform.position;
		Debug.Log("pickup height = " + pickupHeight);
		Debug.Log ("distance from egg = " + pickupDistance);
		animator.SetFloat("PickupHeight", pickupHeight);
		eggToPick = nearestEgg.gameObject;
	}

	public void SetParent(){
		eggToPick.transform.parent = parentBone.transform;
		eggToPick.transform.localPosition = Vector3.zero;
		eggToPick.transform.localRotation = Quaternion.identity;
		GetComponent<AudioSource>().PlayOneShot(pickupSound, 1F);
		}


	public void DestroyEgg(){
		if (eggToPick){
			GetComponent<AudioSource>().PlayOneShot(dropSound, 1F);
			Destroy (eggToPick.gameObject);
				eggToPick = null;
				nearestEgg = null;
				score -= 1;
				Debug.Log ("Score: " + score);
			}
		}
	
	void OnTriggerEnter(Collider other){
		if(!TriggerList.Contains(other) && other.gameObject.CompareTag("Egg")){
			TriggerList.Add(other);
			Debug.Log("adding " + other);
		}
	}

	Transform FindNearestEgg(){
		Transform nearestEgg = null;
		float minDist = Mathf.Infinity;
		Vector3 currentPos = transform.position;
		foreach (Collider egg in TriggerList){
			float dist = Vector3.Distance(egg.transform.position, currentPos);
			if (dist < minDist){
				nearestEgg = egg.transform;
				minDist = dist;
			}
		}
		return nearestEgg;

	}
		
	void OnTriggerExit(Collider other){
		if(TriggerList.Contains(other)){
			//remove it from the list
			TriggerList.Remove(other);
			other.GetComponent<ObjectScript>().HighlightOff();
			Debug.Log("removing " + other);
		}
	}
}
	