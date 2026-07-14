using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControler : MonoBehaviour {

	public int playerNum; //Caso for player 1 == 1;
	public int playerLife = 3; 	//vida do jogador
	public float veloMotor,velocidade,tiroSeg,velocidadeRotacao,rotacaoInput,fire_rate;
	public GameObject shot,mira1,playerExplosion;
	public Rigidbody2D rb;
	private float nextFire;
	private string Horizontal, Vertical, Fire1;
	// Use this for initialization
	void Start () {
		if(playerNum == 10)
		{
			Horizontal ="Horizontal";
			Vertical = "Verical";
			Fire1 = "Fire1";	
		}
		else
		{
			Horizontal ="Horizontal2";
			Vertical = "Verical2";
			Fire1 = "Fire2";
		}
	}
	// Update is called once per frame
	void Update () {

		rotacaoInput = -Input.GetAxisRaw(Horizontal);
		veloMotor = Input.GetAxis(Vertical);	

    	// Aplicamos a velocidade angular diretamente no Rigidbody
    	rb.angularVelocity = rotacaoInput * velocidadeRotacao;

		if (veloMotor > 0f)
		{
        rb.AddForce(transform.up * veloMotor * velocidade);
    	}	

		//if (Input.GetButton("Jump")  && Time.time > nextFire && UI_Manager.isPaused==false && player_IsAtive==true) {
			
		if (Input.GetButton(Fire1)  && Time.time > nextFire){
		nextFire = Time.time + fire_rate;				
		Instantiate(shot, mira1.transform.position, mira1.transform.rotation);
		}

		

	}	

	void OnTriggerEnter2D ( Collider2D collider ) {

		print("BATEU");
		if(collider.gameObject.tag == "asteroid" || collider.gameObject.tag == "inimigo" || collider.gameObject.tag == "boss"){
		Instantiate(playerExplosion, transform.position, Quaternion.identity);	
		Destroy(this.gameObject,0F);
		}
	}	
}
