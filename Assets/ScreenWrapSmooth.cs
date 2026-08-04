using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapSmooth : MonoBehaviour {

private Camera mainCamera;
    private SpriteRenderer spriteRenderer;

    // Margem extra caso o sprite tenha efeitos (ex: rastro de partículas ou luz)
    [SerializeField] private float padding = 0.05f;

    void Start()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 pos = transform.position;

        // Converte a posição do objeto para o espaço de Viewport (0 a 1)
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(pos);

        // Calcula metade da largura e altura do Sprite em unidades da tela (0 a 1)
        Vector3 objectSizeWorld = spriteRenderer.bounds.extents;
        Vector3 objectSizeViewport = mainCamera.WorldToViewportPoint(transform.position + objectSizeWorld) - viewportPos;

        float offsetX = objectSizeViewport.x + padding;
        float offsetY = objectSizeViewport.y + padding;

        bool wrapped = false;

        // Saiu totalmente pela Esquerda -> Surge na Direita
        if (viewportPos.x < -offsetX)
        {
            viewportPos.x = 1 + offsetX;
            wrapped = true;
        }
        // Saiu totalmente pela Direita -> Surge na Esquerda
        else if (viewportPos.x > 1 + offsetX)
        {
            viewportPos.x = -offsetX;
            wrapped = true;
        }

        // Saiu totalmente por Baixo -> Surge no Topo
        if (viewportPos.y < -offsetY)
        {
            viewportPos.y = 1 + offsetY;
            wrapped = true;
        }
        // Saiu totalmente pelo Topo -> Surge em Baixo
        else if (viewportPos.y > 1 + offsetY)
        {
            viewportPos.y = -offsetY;
            wrapped = true;
        }

        // Aplica a nova posição no mundo mantendo o Z original
        if (wrapped)
        {
            Vector3 worldPos = mainCamera.ViewportToWorldPoint(viewportPos);
            worldPos.z = pos.z;
            transform.position = worldPos;
        }
    }
}
