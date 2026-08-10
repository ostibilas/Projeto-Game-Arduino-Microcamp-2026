using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageManagerScript : MonoBehaviour {

	public GameObject PlayerOne,PlayerTwo,Asteroid1,Asteroid2,Asteroid3;,
	public int numPlayer;
	public GameObject atualPlayer1,atualPlayer2;
	public int stageLevel;
	public static float ScorePlayer1,ScorePlayer2;
	public static int player1Life,player2Life;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		
	}
	

	void IniciarLevel(){
		if(numPlayer==2){
		atualPlayer1 = Instantiate(PlayerOne, transform.position, Quaternion.identity);
		atualPlayer2 = Instantiate(PlayerTwo, transform.position, Quaternion.identity);		
		}else{
		atualPlayer1 = Instantiate(PlayerOne, transform.position, Quaternion.identity);	
		}

	
	stageLevel = 1;	
	ScorePlayer1=0f;	
	ScorePlayer2=0f;
	player1Life=3;
	player2Life=3;

	}


}
