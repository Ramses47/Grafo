using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows = 5; // Número de filas
    public int columns = 5; // Número de columnas
    public float nodeSpacing = 1.5f; // Espaciado entre nodos
    public GameObject nodePrefab; // Prefab para los nodos
    public GameObject actor; // Actor que se moverá en la matriz

    private Vector3[,] gridPositions; // Almacena las posiciones de los nodos

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        gridPositions = new Vector3[rows, columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // Calcula la posición del nodo
                Vector3 position = new Vector3(i * nodeSpacing, 0, j * nodeSpacing);
                gridPositions[i, j] = position;

                // Crea el nodo en el mundo
                if (nodePrefab != null)
                {
                    Instantiate(nodePrefab, position, Quaternion.identity);
                }
            }
        }
    }

    public IEnumerator MoveActor(Vector2Int start, Vector2Int end)
    {
        List<Vector3> path = CalculatePath(start, end);
        foreach (Vector3 position in path)
        {
            actor.transform.position = position;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private List<Vector3> CalculatePath(Vector2Int start, Vector2Int end)
    {
        // Genera una lista de posiciones entre el inicio y el final (mueve en línea recta para simplificar)
        List<Vector3> path = new List<Vector3>();

        int xDir = start.x < end.x ? 1 : -1;
        int zDir = start.y < end.y ? 1 : -1;

        for (int x = start.x; x != end.x + xDir; x += xDir)
        {
            path.Add(gridPositions[x, start.y]);
        }

        for (int z = start.y; z != end.y + zDir; z += zDir)
        {
            path.Add(gridPositions[end.x, z]);
        }

        return path;
    }
}
