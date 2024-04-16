using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        if (lastPuntuacionTotal != GameControl.instance.PuntutacionTotal)
        {
            experienciaString.text = GameControl.instance.PuntutacionTotal.ToString() + " XP";
            progreso = (GameControl.instance.PuntutacionTotal * 100) / GameControl.instance.PuntuacionMaxima; // Corregido para evitar errores de c�lculo
            progresoString.text = progreso.ToString() + "%";
            lastPuntuacionTotal = GameControl.instance.PuntutacionTotal; // Actualiza el �ltimo valor registrado
        }
    }
}
