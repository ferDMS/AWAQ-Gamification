using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Experiencia : MonoBehaviour
{
    public Text experienciaString;
    public Text progresoString;
    public int progreso;
    private int lastPuntuacionTotal; // Almacena el �ltimo valor de PuntutacionTotal

    void Start()
    {
        lastPuntuacionTotal = -1; // Inicializar a un valor que nunca ser� v�lido
    }

    void Update()
    {
        // Solo actualiza si PuntutacionTotal ha cambiado
        if (lastPuntuacionTotal != GameControlVariables.PuntutacionTotal)
        {
            experienciaString.text = GameControlVariables.PuntutacionTotal.ToString() + " XP";
            progreso = (GameControlVariables.PuntutacionTotal * 100) / GameControlVariables.PuntuacionMaxima; // Corregido para evitar errores de c�lculo
            progresoString.text = progreso.ToString() + "%";
            lastPuntuacionTotal = GameControlVariables.PuntutacionTotal; // Actualiza el �ltimo valor registrado
        }
    }
}
