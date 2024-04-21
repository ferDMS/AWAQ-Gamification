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
    }

    private void Update()
    {
        // Acceder al estado de la herramienta "Herramienta_Caja"
        estadoCaja = GameControlVariables.GetToolState("Herramienta_Caja");
        estadoRed = GameControlVariables.GetToolState("Herramienta_Red");
        estadoLupa = GameControlVariables.GetToolState("Herramienta_Lupa");
        estadoLinterna = GameControlVariables.GetToolState("Herramienta_Linterna");

        if (GameControlVariables.PuntutacionTotal >= 17000 && GameControlVariables.Desafio2Finished)
        {
            Panel.SetActive(true);
        } else if (GameControlVariables.PuntutacionTotal <= 17000 && !GameControlVariables.Desafio2Finished)
        {
            Panel.SetActive(false);
        }
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
        
        if(GameControlVariables.PuntutacionTotal >= 6000 && !GameControlVariables.Desafio1Finished)
        {
            desafioController.StartDesafio(3);
            GameControlVariables.Desafio1Finished = true;

        }
        else if (GameControlVariables.PuntutacionTotal >= 17000 && !GameControlVariables.Desafio2Finished)
        {
            desafioController.StartDesafio(5);
            GameControlVariables.Desafio1Finished = true;

        }
        else if (GameControlVariables.PuntutacionTotal >= 42000 && !GameControlVariables.Desafio3Finished)
        {
            desafioController.StartDesafio(7);
            GameControlVariables.Desafio1Finished = true;

        }
        else if (GameControlVariables.PuntutacionTotal >= 85000 && !GameControlVariables.DesafioFinal)
        {
            desafioController.StartDesafio(10);
            GameControlVariables.Desafio1Finished = true;

        }
        else
        {
            if (estadoCaja == true && collision.gameObject.tag == "Mono" && !GameControlVariables.animalesRegistrados.Contains("Mono"))
            {
                GameControlVariables.Registrar("Mono");
                ActivarPanel(collision.gameObject.tag);
            }
            if (estadoCaja == true && collision.gameObject.tag == "Oso" && !GameControlVariables.animalesRegistrados.Contains("Oso"))
            {
                GameControlVariables.Registrar("Oso");
                ActivarPanel(collision.gameObject.tag);

            }
            if (estadoRed == true && collision.gameObject.tag == "Buitre" && !GameControlVariables.animalesRegistrados.Contains("Buitre"))
            {
                GameControlVariables.Registrar("Buitre");
                ActivarPanel(collision.gameObject.tag);

            }
            if (estadoCaja == true && collision.gameObject.tag == "Lagarto" && !GameControlVariables.animalesRegistrados.Contains("Lagarto"))
            {
                GameControlVariables.Registrar("Lagarto");
                ActivarPanel(collision.gameObject.tag);

            }
            if (estadoRed == true && collision.gameObject.tag == "Tucan" && !GameControlVariables.animalesRegistrados.Contains("Tucan"))
            {
                GameControlVariables.Registrar("Tucan");
                ActivarPanel(collision.gameObject.tag);

            }
            if (estadoLupa == true && collision.gameObject.tag == "Ave_del_Paraiso" && !GameControlVariables.animalesRegistrados.Contains("Ave_del_Paraiso"))
            {
                GameControlVariables.Registrar("Ave_del_Paraiso");
                ActivarPanel(collision.gameObject.tag);

            }
            if (estadoLupa == true && collision.gameObject.tag == "Orquidea" && !GameControlVariables.animalesRegistrados.Contains("Orquidea"))
            {
                GameControlVariables.Registrar("Oequidea");
                ActivarPanel(collision.gameObject.tag);

            }
            if (estadoLupa == true && collision.gameObject.tag == "Palma" && !GameControlVariables.animalesRegistrados.Contains("Palma"))
            {
                GameControlVariables.Registrar("Palma");
                ActivarPanel(collision.gameObject.tag);

            }
            if (estadoLupa == true && collision.gameObject.tag == "PALMAchica" && !GameControlVariables.animalesRegistrados.Contains("PALMAchica"))
            {
                GameControlVariables.Registrar("PALMAchica");
                ActivarPanel(collision.gameObject.tag);

            }
            if (estadoLupa == true && collision.gameObject.tag == "arbolCacao" && !GameControlVariables.animalesRegistrados.Contains("arbolCacao"))
            {
                GameControlVariables.Registrar("arbolCacao");
                ActivarPanel(collision.gameObject.tag);

            }
            if (estadoLupa == true && collision.gameObject.tag == "Arbusto" && !GameControlVariables.animalesRegistrados.Contains("Arbusto"))
            {
                GameControlVariables.Registrar("Arbusto");
                ActivarPanel(collision.gameObject.tag);

            }
        }

        

    }

}
