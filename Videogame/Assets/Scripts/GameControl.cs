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
        GameControlVariables.Initialize();
        SceneManager.sceneLoaded += OnSceneLoaded;
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
        if (GameControlVariables.PuntutacionTotal >= GameControlVariables.PuntuacionMaxima && GameControlVariables.DesafioFinal == true)
        {
            GameResultScene();
        }
    }

    //HACER CLICK A LAS HERRAMIENTAS
    public void UsarHerramientaCaja()
    {
        GameControlVariables.UpdateToolState("Herramienta_Caja", true);
        GameControlVariables.UpdateToolState("Herramienta_Red", false);
        GameControlVariables.UpdateToolState("Herramienta_Lupa", false);
        GameControlVariables.UpdateToolState("Herramienta_Linterna", false);
    }

    public void UsarHerramientaRed()
    {
        GameControlVariables.UpdateToolState("Herramienta_Caja", false);
        GameControlVariables.UpdateToolState("Herramienta_Red", true);
        GameControlVariables.UpdateToolState("Herramienta_Lupa", false);
        GameControlVariables.UpdateToolState("Herramienta_Linterna", false);
    }

    public void UsarHerramientaLupa()
    {
        GameControlVariables.UpdateToolState("Herramienta_Caja", false);
        GameControlVariables.UpdateToolState("Herramienta_Red", false);
        GameControlVariables.UpdateToolState("Herramienta_Lupa", true);
        GameControlVariables.UpdateToolState("Herramienta_Linterna", false);
    }

    public void UsarHerramientaLinterna()
    {
        GameControlVariables.UpdateToolState("Herramienta_Caja", false);
        GameControlVariables.UpdateToolState("Herramienta_Red", false);
        GameControlVariables.UpdateToolState("Herramienta_Lupa", false);
        GameControlVariables.UpdateToolState("Herramienta_Linterna", true);
    }

    public void AnimationOfHerramientas()
    {
        // Verifica si el estado de la herramienta caja es verdadero y si el Animator no es null
        if (GameControlVariables.GetToolState("Herramienta_Caja") == true)
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
        else if (GameControlVariables.GetToolState("Herramienta_Red") == true)
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
        else if (GameControlVariables.GetToolState("Herramienta_Lupa") == true)
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
        else if (GameControlVariables.GetToolState("Herramienta_Linterna") == true)
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
