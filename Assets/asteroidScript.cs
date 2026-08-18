using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidScript : MonoBehaviour {

    public int tamanho; // tamanho 3 (Grande), 2 (Médio) ou 1 (Pequeno)
    public GameObject asteroid1, asteroid2, asteroid3, explosion;
    public float speed;
    public Rigidbody2D rb;
    public Vector3 pos;

    private stageManagerScript stageManager;
    private bool jaDestruido = false; // Evita que colisões duplas processem o código duas vezes

    void Start () {
        // Encontra o StageManager e registra este asteroide no contador
        GameObject managerObj = GameObject.Find("StageManager");
        if (managerObj != null) {
            stageManager = managerObj.GetComponent<stageManagerScript>();
            stageManager.RegistrarAsteroide();
        } else {
            Debug.LogError("StageManager não encontrado na cena!");
        }

        // Configuração de velocidade baseada no tamanho
        switch (tamanho)
        { 
            case 1: 
                speed = 0.0003f; 
                break; 

            case 2: 
                speed = 0.0002f;  
                break; 

            case 3: 
                speed = 0.0001f;  
                break; 
        }

        if (rb != null) {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            rb.AddForce(randomDirection * speed, ForceMode2D.Impulse);
        }
    }

    void OnTriggerEnter2D (Collider2D collider) {
        if (jaDestruido) return;

        bool atingidoP1 = collider.gameObject.CompareTag("tiro1");
        bool atingidoP2 = collider.gameObject.CompareTag("tiro2");

        if (atingidoP1 || atingidoP2) {
            jaDestruido = true;

            // Destrói o tiro que colidiu
            Destroy(collider.gameObject);

            // Efeito visual de explosão
            if (explosion != null) {
                Instantiate(explosion, transform.position, transform.rotation);
            }

            pos = transform.position;

            // Divisão dos Asteroides (Os novos criados rodarão seu próprio Start e RegistrarAsteroide)
            switch (tamanho) {
                case 3: 
                    if (asteroid2 != null) {
                        Instantiate(asteroid2, pos + new Vector3(-0.5f, 0.5f, 0f), Quaternion.identity);
                        Instantiate(asteroid2, pos + new Vector3(0.5f, -0.5f, 0f), Quaternion.identity);
                    }
                    AdicionarPontos(atingidoP1, 100f);
                    break;

                case 2: 
                    if (asteroid1 != null) {
                        Instantiate(asteroid1, pos + new Vector3(-0.5f, 0.5f, 0f), Quaternion.identity);
                        Instantiate(asteroid1, pos + new Vector3(0.5f, -0.5f, 0f), Quaternion.identity);
                    }
                    AdicionarPontos(atingidoP1, 50f);
                    break;

                case 1: 
                    AdicionarPontos(atingidoP1, 50f);
                    break;  
            }

            // Notifica o StageManager que este asteroide foi removido
            if (stageManager != null) {
                stageManager.RemoverAsteroide();
            }

            Destroy(gameObject);
        }
    }

    private void AdicionarPontos(bool ehPlayer1, float pontos) {
        if (ehPlayer1) {
            stageManagerScript.ScorePlayer1 += pontos;
        } else {
            stageManagerScript.ScorePlayer2 += pontos;
        }
    }
}