using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static ApiManager;
using System.Data;


[System.AttributeUsage(System.AttributeTargets.Class)]
public class ExtensionOfNativeClass : System.Attribute { }

[ExtensionOfNativeClass]
public static class GameControlVariables
{
    public static ApiManager apiManager;
    //puntuaciones
    public static int PuntuacionTotal;

    //public static Dictionary<string, int> ConteoAnimales = new Dictionary<string, int>();

    public static int GetPuntuacionTotalInt()
    {
        return PuntuacionTotal;
    }
    public static void setPuntuacionTotal(int puntuacion)
    {
        PuntuacionTotal = puntuacion;
    }

    public static string GetPuntuacionTotalString ()
    {
        return PuntuacionTotal.ToString();
    }
    public static void AddXP(int xp)
    {
        PuntuacionTotal += xp;
    }  

    //Desafios Terminados
    public static bool Desafio1Finished;
    public static bool Desafio2Finished;
    public static bool Desafio3Finished;
    public static bool DesafioFinal;

    //Animales Registrados
    public static List<string> animalesRegistrados = new List<string>();

    public static string[] GetRegistrados()
    {
        return animalesRegistrados.ToArray();
    }

    public static void Registrar(string Tag)
    {
        animalesRegistrados.Add(Tag);
    }
}

public class DontDestroy : MonoBehaviour
{
    private static DontDestroy instance;

    private void Awake()
    {
        // Si ya hay una instancia creada, destruir este objeto
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Este objeto será la única instancia en todas las escenas
        instance = this;
        DontDestroyOnLoad(gameObject);
        
     }
}
