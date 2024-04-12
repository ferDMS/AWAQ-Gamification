using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Definir una clase que tenga una variable booleana
public class BooleanVariable
{
    public string name;
    public bool state;

    // Constructor que permite establecer el estado inicial de la variable booleana
    public BooleanVariable(bool initialState, string VariableName)
    {
        state = initialState;
        name = VariableName;
    }
}

public class GameControl : MonoBehaviour
{
    //Valor de los animales
    public int Bruite = 600, Oso = 750, Lagarto = 950, Tucan = 1000, Mono = 2000;

    //Puntacion
    public int PuntutacionTotal = 0;
    public int PuntuacionMaxima = 0;

    // Animadores para las herramientas, inicialmente no asignados.
    private Animator animatorObjetoCaja, animatorObjetoRed;

    public static GameControl instance;

    // Declarar una lista de los Tags de los animales Areos
    List<string> animalAereos = new List<string>();
    // Declarar una lista de los Tags de los animales Terrestres
    List<string> animalesTerrestres = new List<string>();

    public delegate void ToolStateChangedEventHandler(string toolName, bool newState);
    public static event ToolStateChangedEventHandler OnToolStateChanged;

    [SerializeField]
    //Lista de variables con propiedades booleanas
    public List<BooleanVariable> objectStatus = new List<BooleanVariable>();
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Intenta encontrar y asignar los animadores cada vez que se carga una escena.
        TryAssignAnimators();
    }

    // Start is called before the first frame update
    
    void Start()
    {

        // Agregar instancias de BooleanVariable a la lista
        objectStatus.Add(new BooleanVariable(false, "Herramienta_Caja"));
        objectStatus.Add(new BooleanVariable(false, "Herramienta_Red"));
        objectStatus.Add(new BooleanVariable(false, "Herramienta_Lupa"));
        objectStatus.Add(new BooleanVariable(false, "Herramienta_Linterna"));

        //Agregar los Tags de los animales a la lista
        animalAereos.Add("Buitre");
        animalesTerrestres.Add("Mono");
        animalesTerrestres.Add("Lagarto");
        animalAereos.Add("Tucan");
        animalesTerrestres.Add("Oso");
    }

    private void TryAssignAnimators()
    {
        GameObject objetoConAnimatorCaja = GameObject.Find("Herramientas_Caja");
        GameObject objetoConAnimatorRed = GameObject.Find("Herramientas_Red");
        if (objetoConAnimatorCaja != null)
        {
            animatorObjetoCaja = objetoConAnimatorCaja.GetComponent<Animator>();
        }
        if (objetoConAnimatorRed != null)
        {
            animatorObjetoRed = objetoConAnimatorRed.GetComponent<Animator>();
        }
    }

    void Update()
    {
        AnimationOfHerramientas();
        //Debug.Log(PuntutacionTotal);
        if (PuntutacionTotal >= PuntuacionMaxima)
        {
            MenuScene();
        }
    }

    //HACER CLICK A LAS HERRAMIENTAS
    public void UsarHerramientaCaja()
    {
        UpdateToolState("Herramienta_Caja", true);
        UpdateToolState("Herramienta_Red", false);
        UpdateToolState("Herramienta_Lupa", false);
        UpdateToolState("Herramienta_Linterna", false);
    }

    public void UsarHerramientaRed()
    {
        UpdateToolState("Herramienta_Caja", false);
        UpdateToolState("Herramienta_Red", true);
        UpdateToolState("Herramienta_Lupa", false);
        UpdateToolState("Herramienta_Linterna", false);
    }

    public void UsarHerramientaLupa()
    {
        UpdateToolState("Herramienta_Caja", false);
        UpdateToolState("Herramienta_Red", false);
        UpdateToolState("Herramienta_Lupa", true);
        UpdateToolState("Herramienta_Linterna", false);
    }

    public void UsarHerramientaLinterna()
    {
        UpdateToolState("Herramienta_Caja", false);
        UpdateToolState("Herramienta_Red", false);
        UpdateToolState("Herramienta_Lupa", false);
        UpdateToolState("Herramienta_Linterna", true);
    }

    public void AnimationOfHerramientas()
    {
        // Verifica si el estado de la herramienta caja es verdadero y si el Animator no es null
        if (objectStatus[0].state == true)
        {
            if (animatorObjetoCaja != null)
            {
                animatorObjetoCaja.SetTrigger("CajaSelected");
            }
            else
            {
                Debug.LogWarning("Animator de Objeto Caja es null.");
            }

            if (animatorObjetoRed != null)
            {
                animatorObjetoRed.SetTrigger("RedDeselected");
            }
            else
            {
                Debug.LogWarning("Animator de Objeto Red es null.");
            }
        }
        else if (objectStatus[1].state == true)
        {
            if (animatorObjetoRed != null)
            {
                animatorObjetoRed.SetTrigger("RedSelected");
            }
            else
            {
                Debug.LogWarning("Animator de Objeto Red es null.");
            }

            if (animatorObjetoCaja != null)
            {
                animatorObjetoCaja.SetTrigger("CajaDeselected");
            }
            else
            {
                Debug.LogWarning("Animator de Objeto Caja es null.");
            }
        }
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("MenuJuego");
        Destroy(this.gameObject);
    }
    private void UpdateToolState(string toolName, bool newState)
    {
        BooleanVariable tool = objectStatus.Find(variable => variable.name == toolName);
        if (tool != null)
        {
            tool.state = newState;
        }

        OnToolStateChanged?.Invoke(toolName, newState);
    }
}
