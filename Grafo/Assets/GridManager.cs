using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public GameObject nodePrefab; // Prefab del nodo
    public GameObject actorPrefab; // Prefab del actor
    public Button startButton; // Botón para marcar inicio
    public Button endButton; // Botón para marcar final
    public Button playButton; // Botón para iniciar el movimiento

    public int rows = 4; // Número de filas
    public int columns = 4; // Número de columnas
    private GameObject[,] grid; // Matriz de nodos

    private Vector2Int startPosition; // Coordenadas de inicio
    private Vector2Int endPosition; // Coordenadas de destino
    private GameObject actor; // Actor que se mueve

    void Start()
    {
        CreateGrid();

        // Asignar acciones a los botones
        startButton.onClick.AddListener(MarkStart);
        endButton.onClick.AddListener(MarkEnd);
        playButton.onClick.AddListener(StartMovement);
    }

    void CreateGrid()
    {
        grid = new GameObject[rows, columns];
        float offsetX = 2.0f; // Espaciado entre nodos
        float offsetY = 2.0f;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector3 position = new Vector3(j * offsetX, 0, i * offsetY);
                grid[i, j] = Instantiate(nodePrefab, position, Quaternion.identity);
                grid[i, j].name = $"Node ({i}, {j})";
            }
        }
    }

    void MarkStart()
    {
        // Simula marcar una posición de inicio (ejemplo: 0,0)
        startPosition = new Vector2Int(0, 0);
        Debug.Log($"Inicio marcado en: {startPosition}");
    }

    void MarkEnd()
    {
        // Simula marcar una posición final (ejemplo: última fila, última columna)
        endPosition = new Vector2Int(rows - 1, columns - 1);
        Debug.Log($"Final marcado en: {endPosition}");
    }

    void StartMovement()
    {
        if (actor == null)
        {
            Vector3 startPos = grid[startPosition.x, startPosition.y].transform.position;
            actor = Instantiate(actorPrefab, startPos, Quaternion.identity);
        }

        StartCoroutine(MoveActorRoutine());
    }

    private IEnumerator MoveActorRoutine()
    {
        Vector2Int current = startPosition;

        while (current != endPosition)
        {
            Vector3 nextPos = grid[current.x, current.y].transform.position;
            actor.transform.position = nextPos;
            yield return new WaitForSeconds(0.5f);

            // Lógica para moverse paso a paso
            if (current.x < endPosition.x) current.x++;
            else if (current.x > endPosition.x) current.x--;
            else if (current.y < endPosition.y) current.y++;
            else if (current.y > endPosition.y) current.y--;
        }

        Debug.Log("Movimiento completado");
    }
}
