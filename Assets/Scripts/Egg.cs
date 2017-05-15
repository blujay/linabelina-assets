using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Lina"))
        {
            Debug.Log("EGG collided with: " + collision.collider.tag);  
            //remove self
            //updateUI
            GameObject.Destroy(collision.gameObject);
        }
    }
}
