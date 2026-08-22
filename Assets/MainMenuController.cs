using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Botões do Menu")]
    [SerializeField] private Button button1Player;
    [SerializeField] private Button button2Players;
    [SerializeField] private Button buttonCredits;

    [Header("Painéis / Cenas")]
    [SerializeField] private GameObject creditsPanel; // Se for um painel na mesma cena
    [SerializeField] private string gameSceneName = "cena teste"; // Nome da cena de jogo

    private void Start()
    {
        // Garante que o primeiro botão comece focado
        SetInitialFocus();

        // Configura os ouvintes dos botões por código (ou configure no Inspector)
        if (button1Player != null)
            button1Player.onClick.AddListener(On1PlayerClicked);

        if (button2Players != null)
            button2Players.onClick.AddListener(On2PlayersClicked);

        if (buttonCredits != null)
            buttonCredits.onClick.AddListener(OnCreditsClicked);
    }

    private void Update()
    {
        // Se o jogador mover o controle e nenhum botão estiver selecionado (ex: clicou com o mouse na tela)
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            // Detecta se houve input de navegação do controle/teclado
            if (horizontal != 0 || vertical != 0)
            {
                SetInitialFocus();
            }
        }
    }

    public void SetInitialFocus()
    {
        if (button1Player != null)
        {
            EventSystem.current.SetSelectedGameObject(null); // Limpa seleção anterior
            button1Player.Select(); // Força o foco no botão 1 Player
        }
    }

    // --- AÇÕES DOS BOTÕES ---

    public void On1PlayerClicked()
    {
        // Salva modo de jogo se necessário e carrega a cena
        //PlayerPrefs.SetInt("GameMode", 1);
		stageManagerScript.playerNum = 1;
        SceneManager.LoadScene("cena teste");
    }

    public void On2PlayersClicked()
    {
       // PlayerPrefs.SetInt("GameMode", 2);
	   stageManagerScript.playerNum = 2;
        SceneManager.LoadScene("cena teste");
    }
	
    public void OnCreditsClicked()
    {
       SceneManager.LoadScene("Creditos");
    }

    public void MainMenuClicked()
    {
       SceneManager.LoadScene("MainMenu");
    }
}