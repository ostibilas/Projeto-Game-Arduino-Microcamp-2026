using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidScript : MonoBehaviour {

	public int tamanho; // tamanho 3 ou 2 ou 1
	public GameObject asteroid1,asteroid2,asteroid3,explosion;
	public float speed;
	public Rigidbody2D rb;


	// Use this for initialization
	void Start () {
		switch (tamanho)
		{ 
			case 1: 
			speed = 0.0003f; 
			break; 

			case 2: 
			speed = 0.0002f;  
			break; 

			case 3: 
			speed = 0.0001f;  
			break; 
			
		}

		Vector2 randomDirection = Random.insideUnitCircle.normalized;
		rb.AddForce(randomDirection * speed, ForceMode2D.Impulse);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D ( Collider2D collider ) {
		

		if(collider.gameObject.tag == "tiro1" || collider.gameObject.tag == "tiro2"){
		
		switch (tamanho)
		{
			 case 3: 
			 Instantiate(explosion, this.transform.position, this.transform.rotation);
			 
			 Instantiate(asteroid2, transform.position, Quaternion.identity);
			 Instantiate(asteroid2, transform.position, Quaternion.identity);
			 Destroy(this.gameObject,0f);
			 break;

			 case 2: 
			 Instantiate(explosion, this.transform.position, this.transform.rotation);

			 Instantiate(asteroid1, transform.position, Quaternion.identity);
			 Instantiate(asteroid1, transform.position, Quaternion.identity);
			 Destroy(this.gameObject,0f);
			 break;
			 
			 case 1: 
			 
			 Destroy(this.gameObject,0f);
			 break;  
		}

	

		}
	}

}
