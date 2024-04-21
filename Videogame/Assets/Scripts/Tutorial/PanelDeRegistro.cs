using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelDeRegistro : MonoBehaviour
{
    public GameObject panelRegistro;
    public RegistroController controller;

    // Variables para guardar los estados
    private bool estadoHerramientaCaja;

    //Boton de pantalla
    public GameObject Button;

    // On Click
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoHerramientaCaja && this.gameObject.CompareTag("Mono"))
        {
            if (!panelRegistro.activeSelf)
            {
                panelRegistro.tag = this.gameObject.tag;
                controller.ResetPanel();
                controller.SetAnswers();
                panelRegistro.SetActive(true);
                Button.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    private void Start()
    {
        
    }
    private void Update()
    {
        estadoHerramientaCaja = GameControlVariables.GetToolState("Herramienta_Caja");
    }
}
