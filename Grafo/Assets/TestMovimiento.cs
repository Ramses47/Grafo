using UnityEngine;

public class TestMovimientoUI : MonoBehaviour
{
    public MovimientoActorUI movimientoActor;
    public MatrizNodosUI matrizNodos;

    void Start()
    {
        // Crear una matriz de RectTransform basada en la matriz de botones
        RectTransform[,] matriz = new RectTransform[matrizNodos.filas, matrizNodos.columnas];

        for (int i = 0; i < matrizNodos.filas; i++)
        {
            for (int j = 0; j < matrizNodos.columnas; j++)
            {
                matriz[i, j] = matrizNodos.MatrizBotones[i, j].GetComponent<RectTransform>();
            }
        }

        // Probar movimiento desde (0, 0) hasta (4, 4)
        movimientoActor.MoverActor(matriz, 0, 0, 4, 4);
    }
}
