using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalClickHandler : MonoBehaviour
{
    public GameObject panelRegistro;
    public RegistroController controller;

    // On Click
    private void OnMouseDown()
    {
        if(!panelRegistro.activeSelf)
        {
            panelRegistro.tag = this.gameObject.tag;
            controller.ResetPanel();
            controller.SetAnswers();
            panelRegistro.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
