using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chicken : MonoBehaviour {

	[Tooltip("Minimum time that the chicken will remain in rest state")]
	public float MinIdleTime = 1;
	[Tooltip("Maximum time that the chicken will remain in rest state")]
	public float MaxIdleTime = 3;

	Animator chickenAnimator;
	enum ChickenAnimationState
	{
		RESTING,
		IDLE,
		PECKING,
		WALKING
	};
	ChickenAnimationState currentState = ChickenAnimationState.RESTING;
	float timeToNextStateChange = 0;
	Vector3 goalPosition;
	private const float MAX_VELOCITY = 2;

	// Use this for initialization
	void Start () {
		chickenAnimator = transform.Find("chicken-rigged").GetComponent<Animator>();
		SetToResting();
	}
	
	// Update is called once per frame
	void Update () {
		if (chickenAnimator.GetCurrentAnimatorStateInfo(0).IsName("Rest") && currentState != ChickenAnimationState.RESTING) {
			SetToResting();
		}

		timeToNextStateChange -= Time.deltaTime;

		if(timeToNextStateChange <= 0 && currentState == ChickenAnimationState.RESTING) {
			switch(Random.Range(0, 3)) {
				case 0:
					currentState = ChickenAnimationState.PECKING;
					chickenAnimator.SetTrigger("pecking");
					break;
				case 1:
					currentState = ChickenAnimationState.IDLE;
					chickenAnimator.SetTrigger("idle");
					break;
				case 2:
					currentState = ChickenAnimationState.WALKING;
					chickenAnimator.SetBool("walking", true);
					goalPosition = new Vector3(transform.position.x + Random.Range(-2.5f, 2.5f), transform.position.y, transform.position.z + Random.Range(-2.5f, 2.5f));
					break;
			}
		}

		if(Vector3.Distance(transform.position, goalPosition) > 0.2f) {
			Vector3 goalVelocity = (goalPosition - transform.position).normalized;
			Vector3 steering = goalVelocity - transform.forward;
			//now I need to rotate the chicken
			//transform.Rotate()
			//Vector3 newPosition = transform.position + ;
			//transform.position = newPosition;
		} else if(currentState == ChickenAnimationState.WALKING) {
			chickenAnimator.SetBool("walking", false);
			SetToResting();
		}
	}

	void SetToResting()
	{
		goalPosition = transform.position;
		currentState = ChickenAnimationState.RESTING;
		timeToNextStateChange = Random.Range(MinIdleTime, MaxIdleTime);
	}
}
