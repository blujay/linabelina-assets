using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PickupObjects : MonoBehaviour {
	
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

	//private GameObject handholder;
	private Animator animator;
	
	public List<Collider> TriggerList;

	// Use this for initialization
	void Start () {
		//handholder = GameObject.Find("HandHolder") as GameObject;
		animator =  GetComponentInParent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.R)) {
			//print("mouse pressed");
			animator.SetBool ("reaching", true);
		} else{
			animator.SetBool("reaching",false);
		}
		if (Input.GetKey(KeyCode.S)) {
			//print("S pressed");
			animator.SetBool ("waving", true);
		} else{
			animator.SetBool("waving",false);
		}
		var nearestEgg = FindClosestEgg();
		if (nearestEgg)
		{
			nearestEgg.GetComponent<EggScript>().HighlightOn();
			if (Input.GetKey(KeyCode.P))
			{
				PickupEgg(nearestEgg);
			}
			if (Input.GetKey(KeyCode.T))
			{
				Debug.Log("starting to turn");
				//StartCoroutine(TurnTowards(nearestEgg));
			}
		}
		
	}

	void PickupEgg(Transform nearestEgg)
	{
		animator.SetBool("pickup",true);
		float pickupHeight = nearestEgg.position.y - transform.position.y;
		Debug.Log("ph = " + pickupHeight);
		animator.SetFloat("PickupHeight", pickupHeight);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(!TriggerList.Contains(other) && other.gameObject.CompareTag("Egg"))
		{
			TriggerList.Add(other);
			Debug.Log("adding " + other);
		}
	}

	Transform FindClosestEgg()
	{
		Transform tMin = null;
		float minDist = Mathf.Infinity;
		Vector3 currentPos = transform.position;
		foreach (Collider t in TriggerList)
		{
			float dist = Vector3.Distance(t.transform.position, currentPos);
			if (dist < minDist)
			{
				tMin = t.transform;
				minDist = dist;
			}
		}
		return tMin;
	}
		
	void OnTriggerExit(Collider other)
	{
		if(TriggerList.Contains(other))
		{
			//remove it from the list
			TriggerList.Remove(other);
			other.GetComponent<EggScript>().HighlightOff();
			Debug.Log("removing " + other);
		}
	}
	
}
	
