using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stageManagerScript : MonoBehaviour {
    
    [Header("Prefabs dos Asteroides")]
    public GameObject[] prefabsAsteroides;

    [Header("Configurações do Spawner")]
    public float distanciaMinimaCentro = 3f;
    public Vector2 limitesTela = new Vector2(8f, 5f);

    [Header("Interface (UI)")]
    public Text textoNivel;

    // Controle Interno
    private int nivelAtual = 1;
    private int totalNiveis = 5;
    private bool esperandoProximoNivel = false;
    
    // OTIMIZAÇÃO: Contador simples em memória
    private int totalAsteroidesVivos = 0;

    public GameObject PlayerOne, PlayerTwo;
    public static int playerNum; 
    public int playernumTest; 
    public GameObject atualPlayer1, atualPlayer2;
    public int stageLevel;
    public static float ScorePlayer1, ScorePlayer2; 
    public static int player1Life, player2Life;    
    
    public Text TXT_PontosP1, TXT_PontosP2;
    public Text TXT_LIFEP1, TXT_LIFEP2;

    private Vector3 posicaoSpawn1 = new Vector3(-1f, 0f, 0f);
    private Vector3 posicaoSpawn2 = new Vector3(1f, 0f, 0f);

    void Start () {
        player1Life = 3;
        player2Life = 3;
        //playerNum = playernumTest;
        IniciarPlayer();
        IniciarNivel(nivelAtual);
    }
    
    void Update () {
        // UI Atualizada sem buscas pesadas de objetos
        if (TXT_PontosP1 != null) TXT_PontosP1.text = "Pontos P1: " + ((int)ScorePlayer1).ToString("D6");
        if (TXT_LIFEP1 != null) TXT_LIFEP1.text = "Vidas = " + ((int)player1Life).ToString("D2");

        if (TXT_PontosP2 != null) TXT_PontosP2.text = "Pontos P2: " + ((int)ScorePlayer2).ToString("D6");
        if (TXT_LIFEP2 != null) TXT_LIFEP2.text = "Vidas = " + ((int)player2Life).ToString("D2");
    }

    // REGISTRO: Chamado pelo asteroide assim que ele é criado
    public void RegistrarAsteroide() {
        totalAsteroidesVivos++;
    }

    // REMOÇÃO: Chamado pelo asteroide quando ele morre
    public void RemoverAsteroide() {
        totalAsteroidesVivos--;

        // Verifica a vitória sem precisar de Find no Update
        if (totalAsteroidesVivos <= 0 && !esperandoProximoNivel) {
            totalAsteroidesVivos = 0; // Trava de segurança
            StartCoroutine(ProximoNivelRoutine());
        }
    }

    int ObterQuantidadePorNivel(int nivel) {
        switch (nivel) {
            case 1: return 4;
            case 2: return 6;
            case 3: return 8;
            case 4: return 10;
            case 5: return 12;
            default: return 4;
        }
    }

    int ObterTipoMaximoPorNivel(int nivel) {
        switch (nivel) {
            case 1: return 0; 
            case 2: return 1; 
            case 3: return 2; 
            case 4: return 2; 
            case 5: return 2; 
            default: return 0;
        }
    }

    Vector3 GerarPosicaoForaDoCentro() {
        Vector3 posicao;
        int tentativas = 0;

        do {
            float posX = Random.Range(-limitesTela.x, limitesTela.x);
            float posY = Random.Range(-limitesTela.y, limitesTela.y);
            posicao = new Vector3(posX, posY, 0f);

            tentativas++;
            if (tentativas > 100) break;

        } while (Vector3.Distance(posicao, Vector3.zero) < distanciaMinimaCentro);

        return posicao;
    }

    public void TentarRessucitar(int Qualplayer) {
        StartCoroutine(RessucitarRoutine(Qualplayer));
    }

    private IEnumerator RessucitarRoutine(int Qualplayer) {
        yield return new WaitForSeconds(1.5f);
        
        if (Qualplayer == 1) {
            if (player1Life > 0) {
                player1Life--;
                atualPlayer1 = Instantiate(PlayerOne, posicaoSpawn1, Quaternion.identity);
            }
        } 
        else if (Qualplayer == 2) {
            if (player2Life > 0) {
                player2Life--;
                atualPlayer2 = Instantiate(PlayerTwo, posicaoSpawn2, Quaternion.identity);
            }
        }
    }

    IEnumerator ProximoNivelRoutine() {
        esperandoProximoNivel = true;

        if (nivelAtual < totalNiveis) {
            nivelAtual++;
            AtualizarTextoUI("Nível " + nivelAtual);
            yield return new WaitForSeconds(2f);
            
            IniciarNivel(nivelAtual);
            esperandoProximoNivel = false;

            if(atualPlayer1 != null){
                atualPlayer1.GetComponent<playerControler>()?.StartCoroutine("IniciarPlayer");
            }
        
            if(atualPlayer2 != null){
                atualPlayer2.GetComponent<playerControler>()?.StartCoroutine("IniciarPlayer");
            }
        } else {
            AtualizarTextoUI("Você Venceu todos os Níveis!");
        }
    }

    void AtualizarTextoUI(string mensagem) {
        if (textoNivel != null) {
            textoNivel.text = mensagem;
        }
    }

    void IniciarNivel(int nivel) {
        AtualizarTextoUI("Nível " + nivel);

        int quantidade = ObterQuantidadePorNivel(nivel);
        int tipoMaximo = ObterTipoMaximoPorNivel(nivel);

        for (int i = 0; i < quantidade; i++) {
            int tipoSorteado = Random.Range(0, tipoMaximo + 1);
            tipoSorteado = Mathf.Clamp(tipoSorteado, 0, prefabsAsteroides.Length - 1);

            Vector3 posicaoSpawn = GerarPosicaoForaDoCentro();
            Instantiate(prefabsAsteroides[tipoSorteado], posicaoSpawn, Quaternion.identity);
        }
    
        stageLevel = nivel; 
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