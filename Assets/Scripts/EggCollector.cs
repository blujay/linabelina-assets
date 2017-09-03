using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCollector : MonoBehaviour {

	Animator handAnimator;

	// Use this for initialization
	void Start () {
		handAnimator = transform.Find("FirstPersonCharacter").transform.Find("hand").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			handAnimator.SetTrigger("Reach");
		}
	}
}
