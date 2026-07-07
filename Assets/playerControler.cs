using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControler : MonoBehaviour {

	public static int playerNum; //Caso for player 1 == 1;
	public int playerLife = 3; 	//vida do jogador
	public float velocidade,tiroSeg;
	public GameObject shot,mira1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	if(Input.GetKey(KeyCode.Space))
	{
		Instantiate(shot, mira1.transform.position, mira1.transform.rotation);
	}
		


	}
}
