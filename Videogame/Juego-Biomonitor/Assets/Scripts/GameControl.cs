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

public class IniciarHerramientas
{
    //Lista de valores voleanos de los estados de las herramientas
    private static readonly List<BooleanVariable> objectStatus = new();

    public delegate void ToolStateChangedEventHandler(string toolName, bool newState);
    public static event ToolStateChangedEventHandler OnToolStateChanged;

    public static bool GetToolState(string toolName)
    {
        BooleanVariable tool = objectStatus.Find(variable => variable.name == toolName);
        return tool != null && tool.state;
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
}

public class GameControl : MonoBehaviour
{
    [SerializeField] public ApiManager apiManager;  // Drag your ApiManager component here in the Inspector

    //Button
    public GameObject button;

    //Toolbar
    public ToolbarController toolbarController;

    // Animadores para las herramientas, inicialmente no asignados.
    public Animator animatorObjetoCaja, animatorObjetoRed, animatorObjetoLupa, animatorObjetoLinterna;

    // Declarar una lista de los Tags de los animales Aéreos e inicializarla con los valores deseados
    List<string> animalAereos = new List<string> { "Buitre", "Tucan" };

    // Declarar una lista de los Tags de los animales Terrestres e inicializarla con los valores deseados
    List<string> animalesTerrestres = new List<string> { "Mono", "Lagarto", "Oso" };

    // Declarar una lista de los Tags de las plantas e inicializarla con los valores deseados
    List<string> plantasFlora = new List<string> { "Ave_del_Paraiso", "Orquidea", "Palma", "PALMAchica", "arbolCacao", "Arbusto" };

    private void Awake()
    {
        if (apiManager == null)
        {
            Debug.Log("ApiManager is not set on GameControl");
            return;
        }

        //GameControlVariables.Initialize(apiManager);
        StartFetchingSpeciesData();
        
        
        IniciarHerramientas.Initialize();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void StartFetchingSpeciesData()
    {
        StartCoroutine(apiManager.FetchAndPopulateSpeciesData(OnSpeciesDataFetched));
    }

    private void OnSpeciesDataFetched(bool success)
    {
        if (success)
        {
            Debug.Log("Datos de especies obtenidos y poblados correctamente.");
            // Aquí podrías realizar cualquier acción adicional después de obtener y poblar los datos de especies
        }
        else
        {
            Debug.LogError("Error al obtener y poblar los datos de especies.");
            // Manejar el error de alguna manera, por ejemplo, mostrando un mensaje al usuario o intentando nuevamente la solicitud
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
    
    private void TryAssignAnimators()
    {
        
        GameObject objetoConAnimatorCaja = GameObject.Find("Herramientas_Caja");
        GameObject objetoConAnimatorRed = GameObject.Find("Herramientas_Red");
        GameObject objetoConAnimatorLupa = GameObject.Find("Herramientas_Lupa");
        GameObject objetoConAnimatorLinterna = GameObject.Find("Herramientas_Linterna");
     
        if (objetoConAnimatorCaja != null)
        {
            animatorObjetoCaja = objetoConAnimatorCaja.GetComponent<Animator>();
        }
        if (objetoConAnimatorRed != null)
        {
            animatorObjetoRed = objetoConAnimatorRed.GetComponent<Animator>();
        }
        if (objetoConAnimatorLupa != null)
        {
            animatorObjetoLupa = objetoConAnimatorLupa.GetComponent<Animator>();
        }
        if (objetoConAnimatorLinterna != null)
        {
            animatorObjetoLinterna = objetoConAnimatorLinterna.GetComponent<Animator>();
        }
    }

    void Update()
    {
        AnimationOfHerramientas();
        //Debug.Log(PuntutacionTotal);
        if (GameControlVariables.PuntuacionTotal >= 100000 && GameControlVariables.DesafioFinal == true)
        {
            GameResultScene();
        }

        if (GameControlVariables.PuntuacionTotal >= 20000 && GameControlVariables.Desafio2Finished == true)
        {
            toolbarController.EnableToolbarItems();
        } 
        if (GameControlVariables.PuntuacionTotal >= 50000 && GameControlVariables.Desafio3Finished == true)
        {
            button.SetActive(true);
        }
 
    }

    //HACER CLICK A LAS HERRAMIENTAS
    public void UsarHerramientaCaja()
    {
        IniciarHerramientas.UpdateToolState("Herramienta_Caja", true);
        IniciarHerramientas.UpdateToolState("Herramienta_Red", false);
        IniciarHerramientas.UpdateToolState("Herramienta_Lupa", false);
        IniciarHerramientas.UpdateToolState("Herramienta_Linterna", false);
    }

    public void UsarHerramientaRed()
    {
        IniciarHerramientas.UpdateToolState("Herramienta_Caja", false);
        IniciarHerramientas.UpdateToolState("Herramienta_Red", true);
        IniciarHerramientas.UpdateToolState("Herramienta_Lupa", false);
        IniciarHerramientas.UpdateToolState("Herramienta_Linterna", false);
    }

    public void UsarHerramientaLupa()
    {
        IniciarHerramientas.UpdateToolState("Herramienta_Caja", false);
        IniciarHerramientas.UpdateToolState("Herramienta_Red", false);
        IniciarHerramientas.UpdateToolState("Herramienta_Lupa", true);
        IniciarHerramientas.UpdateToolState("Herramienta_Linterna", false);
    }

    public void UsarHerramientaLinterna()
    {
        IniciarHerramientas.UpdateToolState("Herramienta_Caja", false);
        IniciarHerramientas.UpdateToolState("Herramienta_Red", false);
        IniciarHerramientas.UpdateToolState("Herramienta_Lupa", false);
        IniciarHerramientas.UpdateToolState("Herramienta_Linterna", true);
    }

    public void AnimationOfHerramientas()
    {
        // Verifica si el estado de la herramienta caja es verdadero y si el Animator no es null
        if (IniciarHerramientas.GetToolState("Herramienta_Caja") == true)
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
            if (animatorObjetoLupa != null)
            {
                animatorObjetoLupa.SetTrigger("LupaDeselected");
            }
            else
            {
                Debug.LogWarning("Animator de Objeto Red es null.");
            }
            if (animatorObjetoLinterna != null)
            {
                animatorObjetoLinterna.SetTrigger("LinternaDeselected");
            }
            else
            {
                Debug.LogWarning("Animator de Objeto Linterna es null.");
            }
        }
        else if (IniciarHerramientas.GetToolState("Herramienta_Red") == true)
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
            if (animatorObjetoLupa != null)
            {
                animatorObjetoLupa.SetTrigger("LupaDeselected");
            }
            else
            {
                Debug.LogWarning("Animator de Objeto Red es null.");
            }
            if (animatorObjetoLinterna != null)
            {
                animatorObjetoLinterna.SetTrigger("LinternaDeselected");
            }
            else
            {
                Debug.LogWarning("Animator de Objeto Red es null.");
            }
        }
        else if (IniciarHerramientas.GetToolState("Herramienta_Lupa") == true)
        {
            if (animatorObjetoLupa != null)
            {
                animatorObjetoLupa.SetTrigger("LupaSelected");
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
            if (animatorObjetoRed != null)
            {
                animatorObjetoRed.SetTrigger("RedDeselected");
            }
            else
            {
                Debug.LogWarning("Animator de Objeto Red es null.");
            }
            if (animatorObjetoLinterna != null)
            {
                animatorObjetoLinterna.SetTrigger("LinternaDeselected");
            }
            else
            {
                Debug.LogWarning("Animator de Objeto Red es null.");
            }
        }
        else if (IniciarHerramientas.GetToolState("Herramienta_Linterna") == true)
        {
            if (animatorObjetoLinterna != null)
            {
                animatorObjetoLinterna.SetTrigger("LinternaSelected");
            }
            else
            {
                Debug.LogWarning("Animator de Objeto Red es null.");
            }

            if (animatorObjetoLupa != null)
            {
                animatorObjetoLupa.SetTrigger("LupaDeselected");
            }
            else
            {
                Debug.LogWarning("Animator de Objeto Caja es null.");
            }
            if (animatorObjetoCaja != null)
            {
                animatorObjetoCaja.SetTrigger("CajaDeselected");
            }
            else
            {
                Debug.LogWarning("Animator de Objeto Red es null.");
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
    }

    public void GameResultScene()
    {
        SceneManager.LoadScene("GameResult");
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("MenuJuego");
    }
}
