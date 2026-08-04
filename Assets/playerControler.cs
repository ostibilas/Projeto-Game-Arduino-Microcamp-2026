using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControler : MonoBehaviour {

	public static int playerNum; //Caso for player 1 == 1;
	public int playerLife = 3; 	//vida do jogador
	public float veloMotor,velocidade,tiroSeg,velocidadeRotacao,rotacaoInput,fire_rate;
	public float desaceleracao = 1.5f;
	public float forcaFreio = 5f;
	public GameObject shot,mira1,playerExplosion;
	public Rigidbody2D rb;
	private float nextFire;
	
	void Start () {
		if(rb == null) {
			rb = GetComponent<Rigidbody2D>();
		}
	}
	void Update ()	{

		rotacaoInput = -Input.GetAxis("Vertical");

		rb.angularVelocity = rotacaoInput * velocidadeRotacao;

		if (veloMotor > 0f)	{
		rb.AddForce(transform.up * veloMotor * velocidade);
		}
		else if (veloMotor < 0f)
		{
		rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, forcaFreio * Time.deltaTime);
		}
		else		{
		rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, desaceleracao * Time.deltaTime);
		}	
		if (Input.GetButton("Fire1") && Time.time > nextFire){
			nextFire = Time.time + fire_rate;
			Instantiate(shot, mira1.transform.position, mira1.transform.rotation);
		}
	}
	void OnTriggerEnter2D (Collider2D collider){
		
		if(collider.gameObject.tag == "asteroid" || collider.gameObject.tag == "inimigo" || collider.gameObject.tag == "boss"){

			Instantiate(playerExplosion, transform.position, Quaternion.identity);
			Destroy(this.gameObject, 0F);
		}
	}
}	

