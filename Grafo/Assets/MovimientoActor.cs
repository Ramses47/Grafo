using System.Collections;
using UnityEngine;

public class MovimientoActorUI : MonoBehaviour
{
    public RectTransform actor; // El actor que se moverá (asignar desde el Inspector)

    public void MoverActor(RectTransform[,] matriz, int inicioX, int inicioY, int finX, int finY)
    {
        StartCoroutine(MoverActorCoroutine(matriz, inicioX, inicioY, finX, finY));
    }

    private IEnumerator MoverActorCoroutine(RectTransform[,] matriz, int inicioX, int inicioY, int finX, int finY)
    {
        Vector2 posicionInicial = matriz[inicioX, inicioY].anchoredPosition;
        Vector2 posicionFinal = matriz[finX, finY].anchoredPosition;

        actor.anchoredPosition = posicionInicial;
        yield return new WaitForSeconds(0.5f);

        while (actor.anchoredPosition != posicionFinal)
        {
            actor.anchoredPosition = Vector2.MoveTowards(actor.anchoredPosition, posicionFinal, 50f * Time.deltaTime);
            yield return null;
        }

        Debug.Log("Movimiento completado");
    }
}