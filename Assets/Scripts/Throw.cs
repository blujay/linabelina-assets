using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {

	public GameObject throwPrefab;
	private ObjectScript objectScript;


	void CloneObject(){
		//Debug.Log("Cloning");
		throwPrefab.GetComponent <ObjectScript> ().CloneMe();
	}

	void ThrowObject () {
		//Debug.Log("Throwiiing");
		throwPrefab.GetComponent <ObjectScript> ().ReleaseMe();
		//StartCoroutine(stopRolling());
	}

//		IEnumerator stopRolling(){
//			print(Time.time);
//			yield return new WaitForSeconds(5);
//			Vector3 objectPosition = transform.position;
//			Debug.Log (objectPosition);
//			//GameObject NewEgg = Instantiate (staticEggPrefab, eggPosition) as GameObject;
//			
//		}
	}


