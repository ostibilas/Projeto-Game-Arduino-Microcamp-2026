using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mudaCorTexo : MonoBehaviour {
	[Header("Lista de Cores")]
    [SerializeField] private Color[] colors = new Color[]
    {
        new Color(0.1f, 0.4f, 1.0f), // Azul
        new Color(0.5f, 0.1f, 1.0f), // Roxo
        new Color(0.8f, 0.1f, 0.9f), // Magenta / Violeta
        new Color(0.1f, 0.7f, 1.0f)  // Ciano / Azul Claro
    };

    [Header("Velocidade de Transição")]
    [SerializeField] private float speed = 1.0f;

    private Text uiText;

    private void Awake()
    {
        uiText = GetComponent<Text>();
    }

    private void Update()
    {
        if (colors == null || colors.Length < 2) return;

        // Calcula o avanço suave ida e volta (0 a 1) ao longo da lista
        float pingPong = Mathf.PingPong(Time.time * speed, 1f);
        float scaledTime = pingPong * (colors.Length - 1);

        // Define os índices da cor atual e da próxima cor
        int currentIndex = Mathf.FloorToInt(scaledTime);
        int nextIndex = Mathf.Min(currentIndex + 1, colors.Length - 1);

        // Interpola apenas entre o par atual
        float t = scaledTime - currentIndex;
        uiText.color = Color.Lerp(colors[currentIndex], colors[nextIndex], t);
    }
}

