using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShot : MonoBehaviour {

	public float tiroVelocidade;
	private Rigidbody2D rb;
	public GameObject spark;
	public int PlayerNumero;
	public bool tiroDoPlayer;

	void Start () {
			
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.up * tiroVelocidade;
	//	Invoke("DestroyMySelfNOW", 1f);

	}
	

	void Update (){

	}
	
	void OnBecameInvisible() {
		Destroy (this.gameObject);
	}


	void OnTriggerEnter2D (Collider2D outro){
		if(tiroDoPlayer == true){
			if(outro.gameObject.tag == "player1" || outro.gameObject.tag == "player2" || outro.gameObject.tag == "inimigo" || outro.gameObject.tag == "asteroid"){
			//Instantiate(spark, outro.transform.position, outro.transform.rotation);	
			Destroy(this.gameObject); 
			
			}
		}

		
	}

	void DestroyMySelfNOW(){
		Destroy(this.gameObject,0f);
	}
	
	
}
