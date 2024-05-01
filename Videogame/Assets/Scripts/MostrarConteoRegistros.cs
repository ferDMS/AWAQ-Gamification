using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostrarConteoRegistros : MonoBehaviour
{   
    public ApiManager apiManager;
    //int ConteoBuitre = GameControlVariables.ConteoAnimales["Buitre"]; //ejemplo sin campiar lo de api manager


    public Text TextoConteoTucan;
    public Text TextoConteoOso;
    public Text TextoConteoMono;
    public Text TextoConteoLagarto;
    public Text TextoConteoBuitre;



    public Text TextoConteoAveParaiso;
    public Text TextoConteoOrquidea;
    public Text TextoConteoPalma;
    public Text TextoConteoArbolCacao;
    public Text TextoConteoFrailejones;



    // Start is called before the first frame update
    void Start()
    {
        apiManager = GameObject.Find("APIManager").GetComponent<ApiManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (apiManager.IsDataReady) // Check if data is ready
        {
            UpdateDisplay();
        }
    }

    void UpdateDisplay()
    {
        TextoConteoTucan.text = "X" + apiManager.GetAnimalCount("Tucán Pechiblanco").ToString();
        TextoConteoOso.text = "X" + apiManager.GetAnimalCount("Oso de Anteojos").ToString();
        TextoConteoMono.text = "X" + apiManager.GetAnimalCount("Tití Ornamentado").ToString();
        TextoConteoLagarto.text = "X" + apiManager.GetAnimalCount("Lagarto Punteado").ToString();
        TextoConteoBuitre.text = "X" + apiManager.GetAnimalCount("Cóndor Andino").ToString();
        TextoConteoAveParaiso.text = "X" + apiManager.GetAnimalCount("Ave del Paraíso").ToString();
        TextoConteoOrquidea.text = "X" + apiManager.GetAnimalCount("Orquídea flor de Mayo").ToString();
        TextoConteoPalma.text = "X" + apiManager.GetAnimalCount("Palma de Cera del Quindío").ToString();
        TextoConteoArbolCacao.text = "X" + apiManager.GetAnimalCount("Arbol de Cacao").ToString();
        TextoConteoFrailejones.text = "X" + apiManager.GetAnimalCount("Frailejones").ToString();    
    }

}


