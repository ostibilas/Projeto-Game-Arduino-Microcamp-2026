using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stageManagerScript : MonoBehaviour {
    
    [Header("Prefabs dos Asteroides")]
    [Tooltip("Arraste aqui os 3 tipos/tamanhos de asteroides.")]
    public GameObject[] prefabsAsteroides; // Size = 3 (0: Pequeno/Tipo1, 1: Médio/Tipo2, 2: Grande/Tipo3)

    [Header("Configurações do Spawner")]
    public float distanciaMinimaCentro = 3f; // Raio central seguro para o jogador
    public Vector2 limitesTela = new Vector2(8f, 5f); // Limites X e Y do viewport da câmera

    [Header("Interface (UI)")]
    public Text textoNivel; // Opcional: Arraste um componente UI Text aqui

    // Controle Interno de Nível
    private int nivelAtual = 1;
    private int totalNiveis = 5;
    private List<GameObject> asteroidesAtivos = new List<GameObject>();
    private bool esperandoProximoNivel = false;

    public GameObject PlayerOne, PlayerTwo, Asteroid1, Asteroid2, Asteroid3;
    public static int playerNum; // aterar via codigo
    public int playernumTest; // apagar
    public GameObject atualPlayer1, atualPlayer2;
    public int stageLevel;
    public static float ScorePlayer1, ScorePlayer2; // vidas do player
    public static int player1Life, player2Life;    // vidas do player
    
    public Text TXT_PontosP1, TXT_PontosP2;
    public Text TXT_LIFEP1, TXT_LIFEP2;

    private Vector3 posicaoSpawn1 = new Vector3(-1f, 0f, 0f);
    private Vector3 posicaoSpawn2 = new Vector3(1f, 0f, 0f);


    // Use this for initialization
    void Start () {
        // AJUSTE: Define as vidas PRIMEIRO antes de instanciar ou rodar nível
        player1Life = 3;
        player2Life = 3;
        playerNum = playernumTest;
        IniciarPlayer();
        IniciarNivel(nivelAtual);
    }
    
    // Update is called once per frame
    void Update () {
        // Limpa referências nulas de asteroides que foram destruídos
        asteroidesAtivos.RemoveAll(item => item == null);

        // Se destruiu todos os asteroides do nível e não está aguardando
        if (asteroidesAtivos.Count == 0 && !esperandoProximoNivel)
        {
            StartCoroutine(ProximoNivelRoutine());
        }

        //=========================Player1===================================== 
        if (TXT_PontosP1 != null) TXT_PontosP1.text = "Pontos P1: " + ((int)ScorePlayer1).ToString("D6");
        if (TXT_LIFEP1 != null) TXT_LIFEP1.text = "Vidas = " + ((int)player1Life).ToString("D2");
        
        print("scoreP1: "+ ScorePlayer1);
        print("LIFEP1: "+player1Life);

        //=========================Player2=====================================
        if (TXT_PontosP2 != null) TXT_PontosP2.text = "Pontos P2: " + ((int)ScorePlayer2).ToString("D6");
        if (TXT_LIFEP2 != null) TXT_LIFEP2.text = "Vidas = " + ((int)player2Life).ToString("D2");
    }

    int ObterQuantidadePorNivel(int nivel)
    {
        switch (nivel)
        {
            case 1: return 4;
            case 2: return 6;
            case 3: return 8;
            case 4: return 10;
            case 5: return 12; // 12 Asteroides Grandes no Nível 5
            default: return 4;
        }
    }

    // Controla a evolução dos tipos permitidos (0 = Tipo 1, 1 = Tipo 2, 2 = Tipo 3 Grande)
    int ObterTipoMaximoPorNivel(int nivel)
    {
        switch (nivel)
        {
            case 1: return 0; // Apenas Tipo 1 (Começo)
            case 2: return 1; // Tipos 1 e 2
            case 3: return 2; // Tipos 1, 2 e 3
            case 4: return 2; // Todos os tipos
            case 5: return 2; // Nível 5: Apenas Tipo Grande
            default: return 0;
        }
    }

    // Calcula uma posição aleatória na tela garantindo que fique longe do centro
    Vector3 GerarPosicaoForaDoCentro()
    {
        Vector3 posicao;
        int tentativas = 0;

        do
        {
            float posX = Random.Range(-limitesTela.x, limitesTela.x);
            float posY = Random.Range(-limitesTela.y, limitesTela.y);
            posicao = new Vector3(posX, posY, 0f);

            tentativas++;
            if (tentativas > 100) break;

        } while (Vector3.Distance(posicao, Vector3.zero) < distanciaMinimaCentro);

        return posicao;
    }

    // AJUSTE: Chama a Coroutine para dar tempo da explosão sumir antes de nascer
    public void TentarRessucitar(int Qualplayer) {
        print("NASCE DEMONIO");
        StartCoroutine(RessucitarRoutine(Qualplayer));
    }

    private IEnumerator RessucitarRoutine(int Qualplayer) {
    yield return new WaitForSeconds(1.5f);

    
    if (Qualplayer == 1) {
        if (player1Life > 0) {
            player1Life--;
            atualPlayer1 = Instantiate(PlayerOne, posicaoSpawn1, Quaternion.identity);
            Debug.Log("PLAYER 1 RESSUSCITOU! Vidas restantes: " + player1Life);
        } else {
            Debug.LogWarning("Player 1 tentou renascer, mas NÃO TEM VIDAS! (player1Life: " + player1Life + ")");
        }
    } 
    else if (Qualplayer == 2) {
        if (player2Life > 0) {
            player2Life--;
            atualPlayer2 = Instantiate(PlayerTwo, posicaoSpawn2, Quaternion.identity);
            Debug.Log("PLAYER 2 RESSUSCITOU! Vidas restantes: " + player2Life);
        } else {
            Debug.LogWarning("Player 2 tentou renascer, mas NÃO TEM VIDAS! (player2Life: " + player2Life + ")");
        }
    }
}
    IEnumerator ProximoNivelRoutine()
    {
        esperandoProximoNivel = true;

        if (nivelAtual < totalNiveis)
        {
            nivelAtual++;
            AtualizarTextoUI("Nível " + nivelAtual);
            yield return new WaitForSeconds(2f); // Pausa de 2 segundos entre os níveis
            esperandoProximoNivel = false;
            IniciarNivel(nivelAtual);

            if(atualPlayer1 != null){
                atualPlayer1.GetComponent<playerControler>().StartCoroutine("IniciarPlayer");
            }
        
            if(atualPlayer2 != null){
                atualPlayer2.GetComponent<playerControler>().StartCoroutine("IniciarPlayer");
            }
        }
        else
        {
            AtualizarTextoUI("Você Venceu todos os Níveis!");
        }
    }

    void AtualizarTextoUI(string mensagem)
    {
        Debug.Log(mensagem);
        if (textoNivel != null)
        {
            textoNivel.text = mensagem;
        }
    }

    void IniciarNivel(int nivel) {

        AtualizarTextoUI("Nível " + nivel);

        int quantidade = ObterQuantidadePorNivel(nivel);
        int tipoMaximo = ObterTipoMaximoPorNivel(nivel);

        for (int i = 0; i < quantidade; i++)
        {
            int tipoSorteado = Random.Range(0, tipoMaximo + 1);
            tipoSorteado = Mathf.Clamp(tipoSorteado, 0, prefabsAsteroides.Length - 1);

            Vector3 posicaoSpawn = GerarPosicaoForaDoCentro();
            GameObject novoAsteroide = Instantiate(prefabsAsteroides[tipoSorteado], posicaoSpawn, Quaternion.identity);
            
            asteroidesAtivos.Add(novoAsteroide);
        }
    
        stageLevel = 1; 
    }

    void IniciarPlayer() {
        ScorePlayer1 = 0f;    
        ScorePlayer2 = 0f;
         
        if (playerNum == 2) {
            atualPlayer1 = Instantiate(PlayerOne, -transform.position, Quaternion.identity);
            atualPlayer2 = Instantiate(PlayerTwo, transform.position, Quaternion.identity);
        } else {
            atualPlayer1 = Instantiate(PlayerOne, -transform.position, Quaternion.identity); 
        }
    }
}