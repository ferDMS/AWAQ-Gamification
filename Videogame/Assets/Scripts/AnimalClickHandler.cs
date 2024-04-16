using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimalClickHandler : MonoBehaviour
{
    // Variables para guardar los estados
    private bool estadoHerramientaCaja;
    private bool estadoHerramientaRed;
    private bool estadoHerramientaLupa;
    private bool estadoHerramientaLinterna;

    public GameObject panelRegistro;
    public RegistroController controller;

    public GameObject desafioRegistro;
    public DesafioController desafioController;

    public Experiencia exp;

    string[] animalesRegistrados;

    private void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(0.5f);
        GameControl.OnToolStateChanged += UpdateToolState;
        UpdateToolState("Herramienta_Caja", GameControl.instance.objectStatus.Find(variable => variable.name == "Herramienta_Caja").state);
        UpdateToolState("Herramienta_Red", GameControl.instance.objectStatus.Find(variable => variable.name == "Herramienta_Red").state);
        UpdateToolState("Herramienta_Lupa", GameControl.instance.objectStatus.Find(variable => variable.name == "Herramienta_Lupa").state);
        UpdateToolState("Herramienta_Linterna", GameControl.instance.objectStatus.Find(variable => variable.name == "Herramienta_Linterna").state);

    }

    private void UpdateToolState(string toolName, bool newState)
    {
        switch (toolName)
        {
            case "Herramienta_Caja":
                estadoHerramientaCaja = newState;
                break;
            case "Herramienta_Red":
                estadoHerramientaRed = newState;
                break;
            case "Herramienta_Lupa":
                estadoHerramientaLupa = newState;
                break;
            case "Herramienta_Linterna":
                estadoHerramientaLinterna = newState;
                break;
        }
    }


    private void ActivarPanel(string tag)
    {
        if (!panelRegistro.activeSelf)
        {
            panelRegistro.tag = tag;
            controller.ResetPanel();
            controller.SetAnswers();
            panelRegistro.SetActive(true);
            Time.timeScale = 0;
        }
    }

    //On Collision
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(exp.progreso >= 25 && !GameControl.instance.Desafio1Finished)
        {
            desafioController.StartDesafio(3);
        }else if(exp.progreso >= 50 && !GameControl.instance.Desafio2Finished)
        {
            desafioController.StartDesafio(10);
        }
        else if (exp.progreso >= 85 && !GameControl.instance.Desafio3Finished)
        {
            desafioController.StartDesafio(15);
        }
        else
        {
            RunRegistro(collision);
        }
    }

    public void RunRegistro(Collision2D collision)
    {
        animalesRegistrados = GameControl.instance.GetRegistrados();
        if (estadoHerramientaCaja && collision.gameObject.tag == "Mono" && !animalesRegistrados.Contains("Mono"))
        {
            GameControl.instance.Registrar("Mono");
            ActivarPanel(collision.gameObject.tag);
        }
        if (estadoHerramientaCaja && collision.gameObject.tag == "Oso" && !animalesRegistrados.Contains("Oso"))
        {
            GameControl.instance.Registrar("Oso");
            ActivarPanel(collision.gameObject.tag);
        }
        if (estadoHerramientaRed && collision.gameObject.tag == "Buitre" && !animalesRegistrados.Contains("Buitre"))
        {
            GameControl.instance.Registrar("Buitre");
            ActivarPanel(collision.gameObject.tag);
        }
        if (estadoHerramientaCaja && collision.gameObject.tag == "Lagarto" && !animalesRegistrados.Contains("Lagarto"))
        {
            GameControl.instance.Registrar("Lagarto");
            ActivarPanel(collision.gameObject.tag);
        }
        if (estadoHerramientaRed && collision.gameObject.tag == "Tucan" && !animalesRegistrados.Contains("Tucan"))
        {
            GameControl.instance.Registrar("Tucan");
            ActivarPanel(collision.gameObject.tag);
        }
    }
}
