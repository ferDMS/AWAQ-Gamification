using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostrarConteoRegistros : MonoBehaviour
{
    //int ConteoBuitre = GameControlVariables.ConteoAnimales["Buitre"];
    int ConteoOso = GameControlVariables.ConteoAnimales["Oso"];
    int ConteoLagarto = GameControlVariables.ConteoAnimales["Lagarto"];
    int ConteoTucan = GameControlVariables.ConteoAnimales["Tucan"];
    int ConteoMono = GameControlVariables.ConteoAnimales["Mono"];
    //int ConteoAve_del_Paraiso = GameControlVariables.ConteoAnimales["Ave_del_Paraio"];
    int ConteoOrquidea = GameControlVariables.ConteoAnimales["Orquidra"];
    int ConteoPalma = GameControlVariables.ConteoAnimales["Palma"];
    //int ConteoPALMAchica = GameControlVariables.ConteoAnimales["PALMAchica"];
    int ConteoArbolCacao = GameControlVariables.ConteoAnimales["arbolCacao"];
    int ConteoArbusto = GameControlVariables.ConteoAnimales["Arbusto"];

    //public Text TextoConteoBuitre;
    public Text TextoConteoOso;
    public Text TextoConteoLagarto;
    public Text TextoConteoTucan;
    public Text TextoConteoMono;
    public Text TextoConteoOrquidea;
    public Text TextoConteoPalma;
    //public Text TextoConteoPALMAchica;
    public Text TextoConteoArbolCacao;
    public Text TextoConteoArbusto;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //TextoConteoBuitre.text = "X" + ConteoBuitre.ToString();
        TextoConteoOso.text = "X" + ConteoOso.ToString();
        TextoConteoLagarto.text = "X" + ConteoLagarto.ToString();
        TextoConteoTucan.text = "X" + ConteoTucan.ToString();
        TextoConteoMono.text = "X" + ConteoMono.ToString();
        TextoConteoPalma.text = "X" + ConteoPalma.ToString();
       // TextoConteoPALMAchica.text = "X" + ConteoBuitre.ToString();
        TextoConteoArbolCacao.text = "X" + ConteoArbolCacao.ToString();
        TextoConteoArbusto.text = "X" + ConteoArbusto.ToString();
        TextoConteoOrquidea.text = "X" + ConteoOrquidea.ToString();
    }
}
