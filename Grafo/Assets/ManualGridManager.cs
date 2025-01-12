using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManualGridManager : MonoBehaviour
{
    public GameObject gridPanel; // Panel para contener los botones
    public Button buttonPrefab; // Prefab de botón
    public Button startButton, endButton, playButton; // Botones de control

    private Button[,] grid; // Matriz para almacenar los botones
    private int rows = 4; // Número de filas
    private int columns = 4; // Número de columnas
    private Vector2Int startPosition = new Vector2Int(-1, -1); // Coordenadas de inicio
    private Vector2Int endPosition = new Vector2Int(-1, -1);   // Coordenadas de final

    private bool selectingStart = false; // Bandera para marcar inicio
    private bool selectingEnd = false;   // Bandera para marcar final

    void Start()
    {
        CreateGrid();

        // Asignar funciones a los botones de control
        startButton.onClick.AddListener(() => { selectingStart = true; selectingEnd = false; });
        endButton.onClick.AddListener(() => { selectingEnd = true; selectingStart = false; });
        playButton.onClick.AddListener(StartMovement);
    }

    void CreateGrid()
    {
        grid = new Button[rows, columns];

        float buttonSize = 200f; // Tamaño de cada botón
        float spacing = 100f; // Espaciado entre botones

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // Crear un nuevo botón
                Button newButton = Instantiate(buttonPrefab, gridPanel.transform);
                newButton.name = $"Button ({i}, {j})";

                // Configurar tamaño y posición
                RectTransform rectTransform = newButton.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(buttonSize, buttonSize);
                rectTransform.anchoredPosition = new Vector2(j * (buttonSize + spacing), -i * (buttonSize + spacing));

                // Asignar evento al botón
                int x = i, y = j; // Guardar las coordenadas del botón
                newButton.onClick.AddListener(() => OnButtonClick(x, y));

                grid[i, j] = newButton;
            }
        }
    }

    void OnButtonClick(int x, int y)
    {
        if (selectingStart)
        {
            // Configurar posición inicial
            if (startPosition != new Vector2Int(-1, -1))
            {
                HighlightButton(startPosition, Color.white); // Limpia el color anterior
            }
            startPosition = new Vector2Int(x, y);
            HighlightButton(startPosition, Color.green); // Resalta como inicio
            selectingStart = false;
            Debug.Log($"Inicio marcado en: {startPosition}");
        }
        else if (selectingEnd)
        {
            // Configurar posición final
            if (endPosition != new Vector2Int(-1, -1))
            {
                HighlightButton(endPosition, Color.white); // Limpia el color anterior
            }
            endPosition = new Vector2Int(x, y);
            HighlightButton(endPosition, Color.red); // Resalta como final
            selectingEnd = false;
            Debug.Log($"Final marcado en: {endPosition}");
        }
    }

    void StartMovement()
    {
        if (startPosition == new Vector2Int(-1, -1) || endPosition == new Vector2Int(-1, -1))
        {
            Debug.LogError("Por favor, selecciona un inicio y un final antes de iniciar.");
            return;
        }

        StartCoroutine(MoveActorRoutine());
    }

    private IEnumerator MoveActorRoutine()
    {
        Vector2Int current = startPosition;

        while (current != endPosition)
        {
            HighlightButton(current, Color.white); // Limpia el botón actual

            // Lógica para moverse paso a paso
            if (current.x < endPosition.x) current.x++;
            else if (current.x > endPosition.x) current.x--;
            else if (current.y < endPosition.y) current.y++;
            else if (current.y > endPosition.y) current.y--;

            HighlightButton(current, Color.blue); // Resalta el botón actual
            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("Movimiento completado");
    }

    void HighlightButton(Vector2Int position, Color color)
    {
        Button button = grid[position.x, position.y];
        ColorBlock colors = button.colors;
        colors.normalColor = color;
        button.colors = colors;
    }
}
