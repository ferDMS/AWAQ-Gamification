using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;  

public class AnimalClickHandler : MonoBehaviour
{
    /*
    // Variables para guardar los estados
    private bool estadoHerramientaCaja;
    private bool estadoHerramientaRed;
    private bool estadoHerramientaLupa;
    private bool estadoHerramientaLinterna;
    */
    private bool estadoCaja;
    private bool estadoRed;
    private bool estadoLupa;
    private bool estadoLinterna;
    public ApiManager apiManager;
    public GameObject panelRegistro;
    public RegistroController controller;

    public DesafioController desafioController;

    public GameObject Panel;
    //public List<string> animalesRegistrados = new List<string>();

    //string[] animalesRegistrados;

    private void Start()
    {
        //animalesRegistrados = new List<string>();
        //StartCoroutine(DelayedStart());
        apiManager = GameObject.Find("APIManager").GetComponent<ApiManager>();
    }

    private void Update()
    {
        // Acceder al estado de la herramienta "Herramienta_Caja"
        estadoCaja = IniciarHerramientas.GetToolState("Herramienta_Caja");
        estadoRed = IniciarHerramientas.GetToolState("Herramienta_Red");
        estadoLupa = IniciarHerramientas.GetToolState("Herramienta_Lupa");
        estadoLinterna = IniciarHerramientas.GetToolState("Herramienta_Linterna");
    }

    private void ActivarPanel(string tag)
    {
        if(!panelRegistro.activeSelf)
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
        
        if(GameControlVariables.PuntuacionTotal >= 6000 && !GameControlVariables.Desafio1Finished)
        {
            desafioController.StartDesafio(3);
        }
        else if (GameControlVariables.PuntuacionTotal >= 17000 && !GameControlVariables.Desafio2Finished)
        {
            desafioController.StartDesafio(5);

        }
        else if (GameControlVariables.PuntuacionTotal >= 42000 && !GameControlVariables.Desafio3Finished)
        {
            desafioController.StartDesafio(7);
        }
        else if (GameControlVariables.PuntuacionTotal>= 85000 && !GameControlVariables.DesafioFinal)
        {
            desafioController.StartDesafio(10);
        }
        else
        {
            if (estadoCaja == true && collision.gameObject.tag == "Mono" && !GameControlVariables.animalesRegistrados.Contains("Mono") && apiManager.GetAnimalCount("Tití Ornamentado") < 1)
            {
                GameControlVariables.Registrar("Mono");
                ActivarPanel(collision.gameObject.tag);
            }
            if (estadoCaja == true && collision.gameObject.tag == "Oso" && !GameControlVariables.animalesRegistrados.Contains("Oso") && apiManager.GetAnimalCount("Oso de Anteojos") < 1)
            {
                GameControlVariables.Registrar("Oso");
                ActivarPanel(collision.gameObject.tag);

            }
            if (estadoRed == true && collision.gameObject.tag == "Buitre" && !GameControlVariables.animalesRegistrados.Contains("Buitre") && apiManager.GetAnimalCount("Cóndor Andino") < 1)
            {
                GameControlVariables.Registrar("Buitre");
                ActivarPanel(collision.gameObject.tag);

            }
            if (estadoCaja == true && collision.gameObject.tag == "Lagarto" && !GameControlVariables.animalesRegistrados.Contains("Lagarto") && apiManager.GetAnimalCount("Lagarto Punteado") < 1)
            {
                GameControlVariables.Registrar("Lagarto");
                ActivarPanel(collision.gameObject.tag);

            }
            if (estadoRed == true && collision.gameObject.tag == "Tucan" && !GameControlVariables.animalesRegistrados.Contains("Tucan") && apiManager.GetAnimalCount("Tucán Pechiblanco") < 1)
            {
                GameControlVariables.Registrar("Tucan");
                ActivarPanel(collision.gameObject.tag);

            }
            if (estadoLupa == true && collision.gameObject.tag == "Ave_del_Paraiso" && !GameControlVariables.animalesRegistrados.Contains("Ave_del_Paraiso") && apiManager.GetAnimalCount("Ave del Paraíso") < 1)
            {
                GameControlVariables.Registrar("Ave_del_Paraiso");
                ActivarPanel(collision.gameObject.tag);

            }
            if (estadoLupa == true && collision.gameObject.tag == "Orquidea" && !GameControlVariables.animalesRegistrados.Contains("Orquidea") && apiManager.GetAnimalCount("Orquidea Flor de Mayo") < 1)
            {
                GameControlVariables.Registrar("Orquidea");
                ActivarPanel(collision.gameObject.tag);

            }
            if (estadoLupa == true && collision.gameObject.tag == "Palma" && !GameControlVariables.animalesRegistrados.Contains("Palma") && apiManager.GetAnimalCount("Palma de Cera del Quindío") < 1)
            {
                GameControlVariables.Registrar("Palma");
                ActivarPanel(collision.gameObject.tag);

            }
            if (estadoLupa == true && collision.gameObject.tag == "PALMAchica" && !GameControlVariables.animalesRegistrados.Contains("PALMAchica") && apiManager.GetAnimalCount("Frailejones") < 1)
            {
                GameControlVariables.Registrar("PALMAchica");
                ActivarPanel(collision.gameObject.tag);

            }
            if (estadoLupa == true && collision.gameObject.tag == "arbolCacao" && !GameControlVariables.animalesRegistrados.Contains("arbolCacao") && apiManager.GetAnimalCount("Arbol de Cacao") < 1)
            {
                GameControlVariables.Registrar("arbolCacao");
                ActivarPanel(collision.gameObject.tag);

            }
 
        }

    }

}
