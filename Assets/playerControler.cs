using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControler : MonoBehaviour {

     // Caso for player 1 == 1;
    public int PlayerControlerNum;  // numero do jogador jogador
    public float veloMotor, velocidade, tiroSeg, velocidadeRotacao, rotacaoInput, fire_rate;
    public Collider2D thisCollider;
    private SpriteRenderer spriteRenderer;
    public GameObject StageManager;

    [Header("Configurações de Freio")]
    public float desaceleracao = 1.5f; // Taxa de parada quando solta o acelerador
    public float forcaFreio = 5f;      // Força ao apertar para trás (S / Seta Baixo)

    public GameObject shot, fogoFoguete,mira1, miraFoguete, playerExplosion;
    public Rigidbody2D rb;
    private float nextFire;

    private string horizontalPlayer,verticalPlayer,firePlayer,pausePlayer;
    
    public bool jogoPausado;

    void Start () {
        if (PlayerControlerNum ==1)
        {
            horizontalPlayer = "Horizontal";
            verticalPlayer = "Vertical";
            firePlayer = "Fire1";
            pausePlayer = "Pause1";
        
        }
         if (PlayerControlerNum ==2)
        {
            horizontalPlayer = "Horizontal2";
            verticalPlayer = "Vertical2";
            firePlayer = "Fire2";
            pausePlayer = "pause2";
        
        }
         StageManager = GameObject.Find("StageManager");
        spriteRenderer = GetComponent<SpriteRenderer>();
        thisCollider = GetComponent<Collider2D>();
        StartCoroutine(IniciarPlayer());
       
        
        if (rb == null) {
            rb = GetComponent<Rigidbody2D>();
        }


    }

    public void DefinirAlpha(float alpha)
    {
        // Garante que o alpha fique entre 0.0 e 1.0
        alpha = Mathf.Clamp01(alpha);

        // No Unity, a cor precisa ser reatribuída por inteiro
        Color corAtual = spriteRenderer.color;
        corAtual.a = alpha;
        spriteRenderer.color = corAtual;
    }

    IEnumerator IniciarPlayer() {
        DefinirAlpha(0.5f);
        thisCollider.enabled = false;
        yield return new WaitForSeconds(3f);

        DefinirAlpha(1f);
        thisCollider.enabled = true;
    }
    
    void Update () {


        rotacaoInput = -Input.GetAxisRaw(horizontalPlayer);
        veloMotor = Input.GetAxis(verticalPlayer);  

        // Aplicamos a velocidade angular diretamente no Rigidbody
        rb.angularVelocity = rotacaoInput * velocidadeRotacao;

        // Acelerar para a frente
        if (veloMotor > 0f)
        {
            rb.AddForce(transform.up * veloMotor * velocidade);
            Instantiate(fogoFoguete, miraFoguete.transform.position, miraFoguete.transform.rotation);
        } 
        // Freio ativo (apertando para trás)
        else if (veloMotor < 0f)
        {
            rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, forcaFreio * Time.deltaTime);
        }
        // Desaceleração passiva (soltou os botões)
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, desaceleracao * Time.deltaTime);
        }

        // Disparo
        if (Input.GetButton(firePlayer) && Time.time > nextFire){
            nextFire = Time.time + fire_rate;               
            Instantiate(shot, mira1.transform.position, mira1.transform.rotation);
        }
         if (Input.GetButton(pausePlayer) ){
            Pausar();

        if (jogoPausado == true)
            {
                Despausar();
            
            }
            else
            {
                Pausar();
            }

        }

    }  
    public void Pausar()
    {
        Time.timeScale = 0f; // Congela o tempo do jogo
        jogoPausado = true;
    }

    public void Despausar()
    {
        Time.timeScale = 1f; // Retorna o tempo ao normal
        jogoPausado = false;
    } 

    void OnTriggerEnter2D (Collider2D collider) {
        print("BATEU");
        if(collider.gameObject.tag == "asteroid" || collider.gameObject.tag == "inimigo" || collider.gameObject.tag == "boss"){
            Instantiate(playerExplosion, transform.position, Quaternion.identity);  
            if(stageManagerScript.playerNum == 1){
                if(PlayerControlerNum==1){
                 StageManager.GetComponent<stageManagerScript>().TentarRessucitar(1);
                }
               
                
                }
            if(stageManagerScript.playerNum == 2){
                 if(PlayerControlerNum==1){
                 StageManager.GetComponent<stageManagerScript>().TentarRessucitar(1);
                }
                 if(PlayerControlerNum==2){
                 StageManager.GetComponent<stageManagerScript>().TentarRessucitar(2);
                }
                              
            }
                       
            Destroy(this.gameObject, 0F);
            

        }
    }   
}