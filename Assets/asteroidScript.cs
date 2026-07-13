using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D ( Collider2D collider ) {
		print(collider);

		if(collider.gameObject.tag == "tiro1" || collider.gameObject.tag == "tiro2"){
		Destroy(this.gameObject,0f);
		}
	}

}
