using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Experiencia : MonoBehaviour
{
    public ApiManager apiManager;
    public Text experienciaString;
    public Text progresoString;
    public int progreso;
    private int lastPuntuacion; // Almacena el �ltimo valor de PuntutacionTotal

    void Start()
    {
        lastPuntuacion = -1; // Inicializar a un valor que nunca ser� v�lido
         
    }

    void Update()
    {
        // Solo actualiza si PuntutacionTotal ha cambiado
        if (lastPuntuacion != GameControlVariables.GetPuntuacionTotalInt())
        {
            experienciaString.text = GameControlVariables.GetPuntuacionTotalString() + " XP";
            progreso = (GameControlVariables.GetPuntuacionTotalInt() * 100) / 100000; // Corregido para evitar errores de c�lculo
            progresoString.text = progreso.ToString() + "%";
            lastPuntuacion = GameControlVariables.GetPuntuacionTotalInt(); // Actualiza el �ltimo valor registrado
        }
    }
}

