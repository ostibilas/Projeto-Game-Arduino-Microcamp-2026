using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidScript : MonoBehaviour {

	public int tamanho; // tamanho 3 ou 2 ou 1
	public GameObject asteroid1,asteroid2,asteroid3,explosion;
	public float speed;
	public Rigidbody2D rb;
	public Vector3 pos;

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
		

		if(collider.gameObject.tag == "tiro1"){
		
		switch (tamanho)
			{
			  
			 case 3: 
			 Instantiate(explosion, this.transform.position, this.transform.rotation);
			 
			 // Define a posição atual como base
				 pos = transform.position;

				// Instancia o primeiro um pouco para a esquerda/cima
				Instantiate(asteroid2, pos + new Vector3(-0.5f, 0.5f, 0f), Quaternion.identity);

				// Instancia o segundo um pouco para a direita/baixo
				Instantiate(asteroid2, pos + new Vector3(0.5f, -0.5f, 0f), Quaternion.identity);
				
			 stageManagerScript.ScorePlayer2+= 100f;
			 Destroy(this.gameObject,0f);
			 break;

			 case 2: 
			 Instantiate(explosion, this.transform.position, this.transform.rotation);
				// Define a posição atual como base	
				 pos = transform.position;

				// Instancia o primeiro um pouco para a esquerda/cima
				Instantiate(asteroid1, pos + new Vector3(-0.5f, 0.5f, 0f), Quaternion.identity);

				// Instancia o segundo um pouco para a direita/baixo
				Instantiate(asteroid1, pos + new Vector3(0.5f, -0.5f, 0f), Quaternion.identity);
				

			 stageManagerScript.ScorePlayer2+= 50f;
			 Destroy(this.gameObject,0f);
			 break;
			 
			 case 1: 
			 Instantiate(explosion, this.transform.position, this.transform.rotation);
			 stageManagerScript.ScorePlayer2+= 50f;
			 Destroy(this.gameObject,0f);
			 break;  
			}

		}
		
		if(collider.gameObject.tag == "tiro2"){
		
		switch (tamanho)
			{
			 case 3: 
			 Instantiate(explosion, this.transform.position, this.transform.rotation);
			 
			 // Define a posição atual como base
				 pos = transform.position;

				// Instancia o primeiro um pouco para a esquerda/cima
				Instantiate(asteroid2, pos + new Vector3(-0.5f, 0.5f, 0f), Quaternion.identity);

				// Instancia o segundo um pouco para a direita/baixo
				Instantiate(asteroid2, pos + new Vector3(0.5f, -0.5f, 0f), Quaternion.identity);
				
			 stageManagerScript.ScorePlayer2+= 100f;
			 Destroy(this.gameObject,0f);
			 break;

			 case 2: 
			 Instantiate(explosion, this.transform.position, this.transform.rotation);
				// Define a posição atual como base	
				 pos = transform.position;

				// Instancia o primeiro um pouco para a esquerda/cima
				Instantiate(asteroid1, pos + new Vector3(-0.5f, 0.5f, 0f), Quaternion.identity);

				// Instancia o segundo um pouco para a direita/baixo
				Instantiate(asteroid1, pos + new Vector3(0.5f, -0.5f, 0f), Quaternion.identity);
				

			 stageManagerScript.ScorePlayer2+= 50f;
			 Destroy(this.gameObject,0f);
			 break;
			 
			 case 1: 
			 Instantiate(explosion, this.transform.position, this.transform.rotation);
			 stageManagerScript.ScorePlayer2+= 50f;
			 Destroy(this.gameObject,0f);
			 break;  
			}

		}

	}

}
