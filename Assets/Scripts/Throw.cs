using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {

	public GameObject eggPrefab;
	public GameObject staticEggPrefab;
	private GameObject newObject;
	private EggScript objectScript;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CloneObject(){
		//Debug.Log("Cloning");
		eggPrefab.GetComponent <EggScript> ().CloneMe();
	}

	void ThrowObject () {
		//Debug.Log("Throwiiing");
		newObject = GameObject.Find ("Current Clone");
		newObject.GetComponent <EggScript> ().ReleaseMe();
		StartCoroutine(stopRolling());
	}

		IEnumerator stopRolling(){
			print(Time.time);
			yield return new WaitForSeconds(5);
			Vector3 eggPosition = newObject.transform.position;
			Debug.Log (eggPosition);
			//GameObject NewEgg = Instantiate (staticEggPrefab, eggPosition) as GameObject;
			
		}
	}


