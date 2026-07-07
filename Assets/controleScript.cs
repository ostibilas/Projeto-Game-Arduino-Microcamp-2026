using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleScript : MonoBehaviour {
	public float horizontalInput;
	public Rigidbody2D rb;
	public int velocidade = 5;
	public Transform pePersonagem;
	public LayerMask chaoLayer;
	public bool estaNoChao;
	public float forca;

	// Use this for initialization
	void Start () {
	

		
	}
	void OnCollisionStay2D( Collision2D player ) {

		if(player.gameObject.tag == "chao"){
			print("estao no chao");
			estaNoChao = true;

		}else{
			estaNoChao = false;
		}


	}
	
	// Update is called once per frame
	void Update () {
		
		//horizontalInput = Input.GetAxis("horizontal");
		if(Input.GetKey(KeyCode.Space) && estaNoChao == true){
			rb.AddForce(Vector2.up * forca);
			print("PULOU");
		}


	}
}

