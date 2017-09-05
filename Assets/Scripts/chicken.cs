using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chicken : MonoBehaviour {

	[Tooltip("Minimum time that the chicken will remain in rest state")]
	public float MinIdleTime = 1;
	[Tooltip("Maximum time that the chicken will remain in rest state")]
	public float MaxIdleTime = 3;
	[Tooltip("Speed that the chicken will turn towards what it is aiming for")]
	public float MaxRotationSpeed = 30;

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
				goalPosition = new Vector3(transform.localPosition.x + Random.Range(-10.5f, 10.5f), transform.localPosition.y, transform.localPosition.z + Random.Range(-10.5f, 10.5f));


				//this is just a visualisation of the point the chicken is aiming for. It should only be uncommented when debugging
				//GameObject goal = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				//goal.transform.position = new Vector3(goalPosition.x, goalPosition.y + 2, goalPosition.z);
				break;
			}
		}

		float currentDistance = Vector3.Distance(transform.localPosition, goalPosition);
		if (currentDistance > 0.2f) {
			Vector3 goalVelocity = (goalPosition - transform.localPosition).normalized;
			float aimAngle = Mathf.Atan2(transform.forward.z, transform.forward.x) - Mathf.Atan2(goalVelocity.z, goalVelocity.x);

			if (aimAngle > Mathf.PI) {
				aimAngle -= 2 * Mathf.PI;
			} else if (aimAngle < -1 * Mathf.PI) {
				aimAngle += 2 * Mathf.PI;
			}

			//float rotationSpeed = MaxRotationSpeed*Mathf.Abs(1-(currentDistance-0.1f)/overallDistance);
			transform.Rotate(Vector3.up, aimAngle*Time.deltaTime* MaxRotationSpeed);
			Vector3 newPosition = transform.position + transform.forward * Time.deltaTime;
			transform.position = newPosition;
		} else if(currentState == ChickenAnimationState.WALKING) {
			chickenAnimator.SetBool("walking", false);
			SetToResting();
		}
	}

	void SetToResting()
	{
		goalPosition = transform.localPosition;
		currentState = ChickenAnimationState.RESTING;
		timeToNextStateChange = Random.Range(MinIdleTime, MaxIdleTime);
	}
}