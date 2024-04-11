using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RegistroController : MonoBehaviour
{
    public GameObject panelRegistro;

    public Dropdown NombreDropdown;
    public Dropdown TamanoDropdown;
    public Dropdown TipoDropdown;
    public Dropdown ConteoDropdown;
    public Dropdown ColorDropdown;



    String[] answers = new String[5];

    public void Start()
    {
        ResetPanel();
        SetAnswers();
    }

    public void SetAnswers()
    {
        switch (panelRegistro.tag)
        {
            case "TucanPechiblanco":
                answers = new string[5] { "Tucán Pechiblanco", "20-30 cm", "Ave", "Red de Niebla", "Negro" };

                SetDropdownValues(NombreDropdown, new string[4] { answers[0], "Oso Polar", "Titi Ornamentado", "Colibrí" });
                SetDropdownValues(TamanoDropdown, new string[4] { answers[1], "10-20 cm", "5-10 cm", "30-40 cm" });
                SetDropdownValues(TipoDropdown, new string[4] { answers[2], "Mamífero", "Reptil", "Insecto"});
                SetDropdownValues(ConteoDropdown, new string[4] { answers[3], "Cámara", "Libreta", "Pala" });
                SetDropdownValues(ColorDropdown, new string[4] { answers[4], "Rojo", "Morado", "Azul" });
                break;

            case "TitiOrnamentado":
                answers = new string[5] { "Tití Ornamentado", "50-60 cm", "Mamifero", "Cámara", "Café" };

                SetDropdownValues(NombreDropdown, new string[4] { answers[0], "Oso Polar", "Tucán Pechiblanco", "Colibrí" });
                SetDropdownValues(TamanoDropdown, new string[4] { answers[1], "10-20 cm", "5-10 cm", "30-40 cm" });
                SetDropdownValues(TipoDropdown, new string[4] { answers[2], "Ave", "Reptil", "Insecto" });
                SetDropdownValues(ConteoDropdown, new string[4] { answers[3], "Linterna", "Libreta", "Pala" });
                SetDropdownValues(ColorDropdown, new string[4] { answers[4], "Gris", "Rosa", "Negro" });
                break;
        }
    }

    //Agrega las opciones al Dropdown de forma Aleatoria
    public void SetDropdownValues(Dropdown dropdown, string[] ops)
    {
        ShuffleArray(ops);
        dropdown.options[0].text = ops[0];
        dropdown.options[1].text = ops[1];
        dropdown.options[2].text = ops[2];
        dropdown.options[3].text = ops[3];
    }

    public bool CheckAnswers()
    {
        if (NombreDropdown.options[NombreDropdown.value].text == answers[0] && //Checar que todas las respuestas sean correctas
            TamanoDropdown.options[TamanoDropdown.value].text == answers[1] &&
            TipoDropdown.options[TipoDropdown.value].text == answers[2] &&
            ConteoDropdown.options[ConteoDropdown.value].text == answers[3] &&
            ColorDropdown.options[ColorDropdown.value].text == answers[4])
        {
            return true;
        }
        else //Volver de rojo las incorrectas y verde las correctas
        { 
            if(NombreDropdown.options[NombreDropdown.value].text == answers[0])
            {NombreDropdown.captionText.color = Color.green;}else{ NombreDropdown.captionText.color = Color.red;}
            if(TamanoDropdown.options[TamanoDropdown.value].text == answers[1])
            { TamanoDropdown.captionText.color = Color.green;}else{ TamanoDropdown.captionText.color = Color.red;}
            if(TipoDropdown.options[TipoDropdown.value].text == answers[2])
            { TipoDropdown.captionText.color = Color.green;}else{ TipoDropdown.captionText.color = Color.red;}
            if(ConteoDropdown.options[ConteoDropdown.value].text == answers[3])
            { ConteoDropdown.captionText.color = Color.green;}else{ ConteoDropdown.captionText.color = Color.red;}
            if(ColorDropdown.options[ColorDropdown.value].text == answers[4])
            { ColorDropdown.captionText.color = Color.green;}else{ ColorDropdown.captionText.color = Color.red;}
            return false;
        }
    }

    public void Register() // Al presionar el botón registrar
    {
        if(CheckAnswers())
        {
            Debug.Log(panelRegistro.tag); //Imprimir tipo de animal
            Time.timeScale = 1.0f; //Reanudar Juego
            panelRegistro.SetActive(false); //Quitar Panel
        }
    }

    void ShuffleArray<T>(T[] array)
    {
        System.Random rng = new System.Random();
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
    public void ResetDropdownColor(Dropdown dropdown)
    {
        Color newColor;
        UnityEngine.ColorUtility.TryParseHtmlString("#323232", out newColor);
        dropdown.captionText.color = newColor;
    }

    public void ResetPanel()
    {
        NombreDropdown.value = 4;
        ResetDropdownColor(NombreDropdown);
        TamanoDropdown.value = 4;
        ResetDropdownColor(TamanoDropdown);
        TipoDropdown.value = 4;
        ResetDropdownColor(TipoDropdown);
        ConteoDropdown.value = 4;
        ResetDropdownColor(ConteoDropdown);
        ColorDropdown.value = 4;
        ResetDropdownColor(ColorDropdown);
    }
}
