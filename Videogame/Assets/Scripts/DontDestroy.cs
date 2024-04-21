using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[System.AttributeUsage(System.AttributeTargets.Class)]
public class ExtensionOfNativeClass : System.Attribute { }

[ExtensionOfNativeClass]
public static class GameControlVariables
{
    // Declarar el diccionario de conteo de animales e inicializarlo con los valores deseados
    public static Dictionary<string, int> ConteoAnimales = new Dictionary<string, int>
    {
        {"Buitre", 0},
        {"Oso", 0},
        {"Lagarto", 0},
        {"Tucan", 0},
        {"Mono", 0},
        {"Ave_del_Paraio", 0},
        {"Orquidra", 0},
        {"Palma", 0},
        {"PALMAchica", 0},
        {"arbolCacao", 0},
        {"Arbusto", 0}
    };

    //Lista de valores voleanos de los estados de las herramientas
    private static List<BooleanVariable> objectStatus = new List<BooleanVariable>();

    public delegate void ToolStateChangedEventHandler(string toolName, bool newState);
    public static event ToolStateChangedEventHandler OnToolStateChanged;

    //Puntacion
    public static int PuntutacionTotal = 0;
    public static int PuntuacionMaxima = 85000;

    //Valor de los animales
    public static int Bruite = 600, Oso = 750, Lagarto = 950, Tucan = 1000, Mono = 2000, Ave_del_Paraiso = 500, Orquidea = 600, Arbusto = 750, Palma = 1000, PALMAchica = 1500, arbolCacao = 2000;


    public static bool GetToolState(string toolName)
    {
        BooleanVariable tool = objectStatus.Find(variable => variable.name == toolName);
        return tool != null ? tool.state : false;
    }

    public static void UpdateToolState(string toolName, bool newState)
    {
        BooleanVariable tool = objectStatus.Find(variable => variable.name == toolName);
        if (tool != null)
        {
            tool.state = newState;
            OnToolStateChanged?.Invoke(toolName, newState);
        }
    }

    public static void Initialize()
    {
        objectStatus.Add(new BooleanVariable(false, "Herramienta_Caja"));
        objectStatus.Add(new BooleanVariable(false, "Herramienta_Red"));
        objectStatus.Add(new BooleanVariable(false, "Herramienta_Lupa"));
        objectStatus.Add(new BooleanVariable(false, "Herramienta_Linterna"));
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

        // Resto del código de inicialización aquí
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
