using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BooleanVariableTutorial
{
    public string name;
    public bool state;

    // Constructor que permite establecer el estado inicial de la variable booleana
    public BooleanVariableTutorial(bool initialState, string VariableName)
    {
        state = initialState;
        name = VariableName;
    }
}

public static class GameControlVariablesTutorial
{
    //Lista de valores voleanos de los estados de las herramientas
    private static readonly List<BooleanVariableTutorial> objectStatus = new();

    public delegate void ToolStateChangedEventHandler(string toolName, bool newState);
    public static event ToolStateChangedEventHandler OnToolStateChanged;

    public static bool GetToolState(string toolName)
    {
        BooleanVariableTutorial tool = objectStatus.Find(variable => variable.name == toolName);
        return tool != null && tool.state;
    }

    public static void UpdateToolState(string toolName, bool newState)
    {
        BooleanVariableTutorial tool = objectStatus.Find(variable => variable.name == toolName);
        if (tool != null)
        {
            tool.state = newState;
            OnToolStateChanged?.Invoke(toolName, newState);
        }
    }

    public static void Initialize()
    {
        objectStatus.Add(new BooleanVariableTutorial(false, "Herramienta_Caja"));
        objectStatus.Add(new BooleanVariableTutorial(false, "Herramienta_Red"));
    }
}

    public class GameControlTutorial : MonoBehaviour
    {

        // Animadores para las herramientas, inicialmente no asignados.
        public Animator animatorObjetoCaja, animatorObjetoRed;

        // Declarar una lista de los Tags de los animales Aéreos e inicializarla con los valores deseados
        //List<string> animalAereos = new List<string> { "Buitre", "Tucan" };

        // Declarar una lista de los Tags de los animales Terrestres e inicializarla con los valores deseados
        //List<string> animalesTerrestres = new List<string> { "Mono", "Lagarto", "Oso" };

        // Declarar una lista de los Tags de las plantas e inicializarla con los valores deseados
        //List<string> plantasFlora = new List<string> { "Ave_del_Paraiso", "Orquidea", "Palma", "PALMAchica", "arbolCacao", "Arbusto" };

        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            GameControlVariablesTutorial.Initialize();
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
        }

        //HACER CLICK A LAS HERRAMIENTAS
        public void UsarHerramientaCaja()
        {
            GameControlVariablesTutorial.UpdateToolState("Herramienta_Caja", true);
            GameControlVariablesTutorial.UpdateToolState("Herramienta_Red", false);
            GameControlVariablesTutorial.UpdateToolState("Herramienta_Lupa", false);
            GameControlVariablesTutorial.UpdateToolState("Herramienta_Linterna", false);
        }

        public void UsarHerramientaRed()
        {
            GameControlVariablesTutorial.UpdateToolState("Herramienta_Caja", false);
            GameControlVariablesTutorial.UpdateToolState("Herramienta_Red", true);
            GameControlVariablesTutorial.UpdateToolState("Herramienta_Lupa", false);
            GameControlVariablesTutorial.UpdateToolState("Herramienta_Linterna", false);
        }

        public void AnimationOfHerramientas()
        {
            // Verifica si el estado de la herramienta caja es verdadero y si el Animator no es null
            if (GameControlVariablesTutorial.GetToolState("Herramienta_Caja") == true)
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
            else if (GameControlVariablesTutorial.GetToolState("Herramienta_Red") == true)
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
}
