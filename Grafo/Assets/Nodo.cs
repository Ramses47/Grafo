using UnityEngine;
using UnityEngine.UI;

public class NodoUI : MonoBehaviour
{
    public int fila;
    public int columna;

    public void Inicializar(int fila, int columna)
    {
        this.fila = fila;
        this.columna = columna;
    }
}

public class MatrizNodosUI : MonoBehaviour
{
    public GameObject prefabBoton; // Prefab de un botón (asignado desde el inspector)
    public int filas = 5;
    public int columnas = 5;

    public RectTransform panelContenedor; // Panel donde se colocarán los botones
    private Button[,] matrizBotones; // Matriz para almacenar los botones

    private NodoUI nodoInicio;
    private NodoUI nodoMeta;

    // Propiedad pública para exponer la matriz de botones
    public Button[,] MatrizBotones => matrizBotones;

    void Start()
    {
        matrizBotones = new Button[filas, columnas];

        for (int i = 0; i < filas; i++)
        {
            for (int j = 0; j < columnas; j++)
            {
                // Instanciar un botón en el panel
                GameObject nuevoBoton = Instantiate(prefabBoton, panelContenedor);
                nuevoBoton.name = $"Boton ({i}, {j})";

                // Configurar el texto del botón
                Text textoBoton = nuevoBoton.GetComponentInChildren<Text>();
                if (textoBoton != null)
                {
                    textoBoton.text = $"{i}, {j}";
                }

                // Agregar NodoUI para manejar la lógica del nodo
                NodoUI nodo = nuevoBoton.AddComponent<NodoUI>();
                nodo.Inicializar(i, j);

                // Guardar el botón en la matriz
                matrizBotones[i, j] = nuevoBoton.GetComponent<Button>();

                // Agregar un listener para cuando se haga clic en el botón
                matrizBotones[i, j].onClick.AddListener(() => OnBotonClick(nodo));
            }
        }
    }

    private void OnBotonClick(NodoUI nodo)
    {
        if (nodoInicio == null)
        {
            nodoInicio = nodo;
            Debug.Log($"Nodo de inicio seleccionado: ({nodo.fila}, {nodo.columna})");
            CambiarColorBoton(matrizBotones[nodo.fila, nodo.columna], Color.green);
        }
        else if (nodoMeta == null)
        {
            nodoMeta = nodo;
            Debug.Log($"Nodo meta seleccionado: ({nodo.fila}, {nodo.columna})");
            CambiarColorBoton(matrizBotones[nodo.fila, nodo.columna], Color.red);

            // Aquí puedes iniciar el movimiento o realizar lógica adicional
            IniciarMovimiento(nodoInicio, nodoMeta);
        }
        else
        {
            Debug.Log("Ambos nodos ya han sido seleccionados.");
        }
    }

    private void CambiarColorBoton(Button boton, Color color)
    {
        Image imagenBoton = boton.GetComponent<Image>();
        if (imagenBoton != null)
        {
            imagenBoton.color = color;
        }
    }

    private void IniciarMovimiento(NodoUI inicio, NodoUI meta)
    {
        Debug.Log($"Iniciando movimiento desde ({inicio.fila}, {inicio.columna}) hasta ({meta.fila}, {meta.columna}).");
        // Lógica de movimiento entre nodos (puedes implementarla aquí)
    }
}
