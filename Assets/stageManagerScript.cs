using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stageManagerScript : MonoBehaviour {

	public GameObject PlayerOne,PlayerTwo,Asteroid1,Asteroid2,Asteroid3;
	public int numPlayer;
	public GameObject atualPlayer1,atualPlayer2;
	public int stageLevel;
	public static float ScorePlayer1,ScorePlayer2; //vidas do player
	public static int player1Life,player2Life;    //vidas do player
	
	
    public Text TXT_PontosP1,TXT_PontosP2;
    public Text TXT_LIFEP1,TXT_LIFEP2;


	// Use this for initialization
	void Start () {
		
		IniciarLevel();
	}
	
	// Update is called once per frame
	void Update () {
	//=========================Player1=====================================	
	TXT_PontosP1.text = "Pontos P1: " + ((int)ScorePlayer1).ToString("D6");
	TXT_LIFEP1.text = "Vidas = " + ((int)player1Life).ToString("D2");
	//=========================Player2=====================================
	TXT_PontosP2.text = "Pontos P1: " + ((int)ScorePlayer2).ToString("D6");
	TXT_LIFEP2.text = "Vidas = " + ((int)player2Life).ToString("D2");
	
		
	}
	

	void IniciarLevel(){
	
	if(numPlayer==2){
		atualPlayer1 = Instantiate(PlayerOne, transform.position, Quaternion.identity);
		atualPlayer2 = Instantiate(PlayerTwo, -transform.position, Quaternion.identity);		
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
