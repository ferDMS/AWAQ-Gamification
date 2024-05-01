using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DesafioController : MonoBehaviour
{
    public ApiManager apiManager;
    public GameObject panelDesafio;

    public Dropdown NombreDropdown;
    public Dropdown TamanoDropdown;
    public Dropdown TipoDropdown;
    public Dropdown ConteoDropdown;
    public Dropdown ColorDropdown;

    public Image animalImagen;

    public Sprite tucanSprite;
    public Sprite monoSprite;
    public Sprite osoSprite;
    public Sprite buitreSprite;
    public Sprite lagartoSprite;

    public Sprite aveParaisoSprite;
    public Sprite mayoSprite;
    public Sprite mermeladaSprite;
    public Sprite palmaSprite;
    public Sprite frailejonSprite;
    public Sprite cacaoSprite;

    public Text progressText;

    public ToolbarController toolbarController;

    int registros;
    int registrosTotal;

    int desafio1Fail = 1500;
    int desafio2Fail = 2000;
    int desafio3Fail = 7000;
    int desafioFinalFail = 10000;

    int desafio1Success = 1500;
    int desafio2Success = 3000;
    int desafio3Success = 8000;
    int desafioFinalSuccess = 15000;
    

    String[] answers = new String[4];

    public void Start()
    {
        ResetPanel();
        SetAnswers();
        apiManager = GameObject.Find("APIManager").GetComponent<ApiManager>();
    }
    
    public void StartDesafio(int Registros)
    {
        Time.timeScale = 0;
        progressText.text = "0/"+Registros+" Registrados";
        panelDesafio.SetActive(true);
        registros = Registros;
        registrosTotal = Registros;

    }
    public void SetAnswers()
    {
        string[] animals = { "Mono", "Tucan", "Oso", "Lagarto", "Buitre", "Ave_del_Paraiso", "Orquidea", "Palma", "PALMAchica", "arbolCacao" };
        panelDesafio.tag = animals[UnityEngine.Random.Range(0, animals.Length)];

        switch (panelDesafio.tag)
        {
            case "Tucan":
                answers = new string[5] { "Tucán Pechiblanco", "20 - 30cm", "Ave", "Anillo de registro", "Negro" };
                animalImagen.sprite = tucanSprite;

                SetDropdownValues(NombreDropdown, new string[4] { answers[0], "Oso Polar", "Titi Ornamentado", "Colibrí" });
                SetDropdownValues(TamanoDropdown, new string[4] { answers[1], "10- 20cm", "5 - 10cm", "30 - 40cm" });
                SetDropdownValues(TipoDropdown, new string[4] { answers[2], "Mamífero", "Reptil", "Insecto" });
                SetDropdownValues(ConteoDropdown, new string[4] { answers[3], "Cámara", "Libreta", "Pala" });
                SetDropdownValues(ColorDropdown, new string[4] { answers[4], "Rojo", "Morado", "Azul" });
                break;

            case "Mono":
                answers = new string[5] { "Tití Ornamentado", "50 - 60cm", "Mamifero", "Cámara", "Rojo" };
                animalImagen.sprite = monoSprite;

                SetDropdownValues(NombreDropdown, new string[4] { answers[0], "Oso Polar", "Tucán Pechiblanco", "Colibrí" });
                SetDropdownValues(TamanoDropdown, new string[4] { answers[1], "10 - 20cm", "5 - 10cm", "30 - 40cm" });
                SetDropdownValues(TipoDropdown, new string[4] { answers[2], "Ave", "Reptil", "Insecto" });
                SetDropdownValues(ConteoDropdown, new string[4] { answers[3], "Linterna", "Libreta", "Pala" });
                SetDropdownValues(ColorDropdown, new string[4] { answers[4], "Gris", "Rosa", "Negro" });
                break;
            case "Oso":
                answers = new string[5] { "Oso de Anteojos", "1.5m", "Mamífero", "Cámara", "Negro" };
                animalImagen.sprite = osoSprite;

                SetDropdownValues(NombreDropdown, new string[4] { answers[0], "Oso Polar", "Lagarto", "Tití Ornamentado" });
                SetDropdownValues(TamanoDropdown, new string[4] { answers[1], "10 - 20cm", "5 - 10m", "3m" });
                SetDropdownValues(TipoDropdown, new string[4] { answers[2], "Ave", "Reptil", "Insecto" });
                SetDropdownValues(ConteoDropdown, new string[4] { answers[3], "Linterna", "Libreta", "Pala" });
                SetDropdownValues(ColorDropdown, new string[4] { answers[4], "Gris", "Azul", "Rojo" });
                break;
            case "Buitre":
                answers = new string[5] { "Cóndor Andino", "1m", "Ave", "Anillo de registro", "Negro" };
                animalImagen.sprite = buitreSprite;

                SetDropdownValues(NombreDropdown, new string[4] { answers[0], "Oso Polar", "Lagarto", "Tití Ornamentado" });
                SetDropdownValues(TamanoDropdown, new string[4] { answers[1], "10 - 20cm", "5 - 10m", "3m" });
                SetDropdownValues(TipoDropdown, new string[4] { answers[2], "Mamifero", "Reptil", "Insecto" });
                SetDropdownValues(ConteoDropdown, new string[4] { answers[3], "Dardos", "Caña de Pescar", "Pala" });
                SetDropdownValues(ColorDropdown, new string[4] { answers[4], "Amarillo", "Azul", "Rojo" });
                break;
            case "Lagarto":
                answers = new string[5] { "Lagarto Punteado", "20 - 30 cm", "Reptil", "Cámara", "Amarillo" };
                animalImagen.sprite = lagartoSprite;

                SetDropdownValues(NombreDropdown, new string[4] { answers[0], "Salamandra", "Cóndor Andino", "Oso Ornamentado" });
                SetDropdownValues(TamanoDropdown, new string[4] { answers[1], "10 - 20cm", "5 - 10m", "3m" });
                SetDropdownValues(TipoDropdown, new string[4] { answers[2], "Ave", "Planta", "Insecto" });
                SetDropdownValues(ConteoDropdown, new string[4] { answers[3], "Linterna", "Libreta", "Pala" });
                SetDropdownValues(ColorDropdown, new string[4] { answers[4], "Gris", "Azul", "Rojo" });
                break;
            case "Ave_del_Paraiso":
                answers = new string[5] { "Ave del Paraiso", "20 - 30cm", "Planta", "Lupa", "Naranja" };
                animalImagen.sprite = aveParaisoSprite;

                SetDropdownValues(NombreDropdown, new string[4] { answers[0], "Orquídea flor de Mayo", "Palma de Cera del Quindío", "Arbusto de la mermelada" });
                SetDropdownValues(TamanoDropdown, new string[4] { answers[1], "5 - 10cm", "10m", "1m" });
                SetDropdownValues(TipoDropdown, new string[4] { answers[2], "Árbol", "Pasto", "Palma" });
                SetDropdownValues(ConteoDropdown, new string[4] { answers[3], "Linterna", "Pala", "Libreta" });
                SetDropdownValues(ColorDropdown, new string[4] { answers[4], "Blanco", "Amarillo", "Negro" });
                break;
            case "Orquidea":
                answers = new string[5] { "Orquidea Flor de Mayo", "5 - 10cm", "Flor", "Lupa", "Morado" };
                animalImagen.sprite = mayoSprite;

                SetDropdownValues(NombreDropdown, new string[4] { answers[0], "Ave del Paraíso", "Frailejones", "Árbol de Cacao" });
                SetDropdownValues(TamanoDropdown, new string[4] { answers[1], "20 - 30cm", "1m", "4m" });
                SetDropdownValues(TipoDropdown, new string[4] { answers[2], "Palma", "Arbusto", "Árbol" });
                SetDropdownValues(ConteoDropdown, new string[4] { answers[3], "Dardos", "Caña de Pescar", "Linterna" });
                SetDropdownValues(ColorDropdown, new string[4] { answers[4], "Gris", "Café", "Rojo" });
                break;
            case "Palma":
                answers = new string[5] { "Palma de Cera del Quindío", "10m", "Palma", "Lupa", "Verde" };
                animalImagen.sprite = palmaSprite;

                SetDropdownValues(NombreDropdown, new string[4] { answers[0], "Árbol de Cacao", "Arbusto de la mermelada", "Orquídeas flor de Mayo" });
                SetDropdownValues(TamanoDropdown, new string[4] { answers[1], "1m", "5 - 10cm", "2m" });
                SetDropdownValues(TipoDropdown, new string[4] { answers[2], "Pasto", "Arbusto", "Flor" });
                SetDropdownValues(ConteoDropdown, new string[4] { answers[3], "Cámara", "Red de Niebla", "Pala" });
                SetDropdownValues(ColorDropdown, new string[4] { answers[4], "Azul", "Amarillo", "Naranja" });
                break;
            case "PALMAchica":
                answers = new string[5] { "Frailejones", "1 m", "Palma", "Lupa", "Verde" };
                animalImagen.sprite = frailejonSprite;

                SetDropdownValues(NombreDropdown, new string[4] { answers[0], "Ave del Paraíso", "Árbol de Cacao", "Palma de Cera" });
                SetDropdownValues(TamanoDropdown, new string[4] { answers[1], "10m", "3 - 6cm", "20 - 30cm" });
                SetDropdownValues(TipoDropdown, new string[4] { answers[2], "Pasto", "Árbol", "Flor" });
                SetDropdownValues(ConteoDropdown, new string[4] { answers[3], "Cámara", "Red de Niebla", "Pala" });
                SetDropdownValues(ColorDropdown, new string[4] { answers[4], "Amarillo", "Azul", "Rojo" });
                break;
            case "arbolCacao":
                answers = new string[5] { "Árbol de Cacao", "4m", "Árbol", "Lupa", "Verde" };
                animalImagen.sprite = cacaoSprite;

                SetDropdownValues(NombreDropdown, new string[4] { answers[0], "Palma de Cera", "Arbusto de la Mermelada", "Orquídeas flor de Mayo" });
                SetDropdownValues(TamanoDropdown, new string[4] { answers[1], "1m", "10m", "5 - 10cm" });
                SetDropdownValues(TipoDropdown, new string[4] { answers[2], "Palma", "Flor", "Arbusto" });
                SetDropdownValues(ConteoDropdown, new string[4] { answers[3], "Linterna", "Libreta", "Pala" });
                SetDropdownValues(ColorDropdown, new string[4] { answers[4], "Morado", "Amarillo", "Rosa" });
                break;
                //case "Arbusto":
                //    answers = new string[5] { "Arbusto de la Mermelada", "2m", "Planta", "Lupa", "Naranja" };
                //    animalImagen.sprite = mermeladaSprite;

                //    SetDropdownValues(NombreDropdown, new string[4] { answers[0], "Ave del Paraíso", "Frailejones", "Árbol de Cacao" });
                //    SetDropdownValues(TamanoDropdown, new string[4] { answers[1], "4m", "15 - 20cm", "2m" });
                //    SetDropdownValues(TipoDropdown, new string[4] { answers[2], "Palma", "Árbol", "Flor" });
                //    SetDropdownValues(ConteoDropdown, new string[4] { answers[3], "Cámara", "Red de niebla", "Caña de Pescar" });
                //    SetDropdownValues(ColorDropdown, new string[4] { answers[4], "Gris", "Azul", "Rojo" });
                //    break;
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
            if (NombreDropdown.options[NombreDropdown.value].text == answers[0])
            { NombreDropdown.captionText.color = Color.green; }
            else { NombreDropdown.captionText.color = Color.red; }
            if (TamanoDropdown.options[TamanoDropdown.value].text == answers[1])
            { TamanoDropdown.captionText.color = Color.green; }
            else { TamanoDropdown.captionText.color = Color.red; }
            if (TipoDropdown.options[TipoDropdown.value].text == answers[2])
            { TipoDropdown.captionText.color = Color.green; }
            else { TipoDropdown.captionText.color = Color.red; }
            if (ConteoDropdown.options[ConteoDropdown.value].text == answers[3])
            { ConteoDropdown.captionText.color = Color.green; }
            else { ConteoDropdown.captionText.color = Color.red; }
            if (ColorDropdown.options[ColorDropdown.value].text == answers[4])
            { ColorDropdown.captionText.color = Color.green; }
            else { ColorDropdown.captionText.color = Color.red; }
            return false;
        }
    }

    public void Register() // Al presionar el botón registrar
    {
        if (CheckAnswers())
        {
            registros--;
            if (registros == 0)
            {
                //Desafio Ganadoo
                if (registrosTotal == 3)
                {
                    GameControlVariables.Desafio1Finished = true;
                    StartCoroutine(apiManager.PostXpEvent(PlayerPrefs.GetInt("UserID"), 14, DateTime.Now, true, (isSuccess) => { // Define the callback
                        if (isSuccess)
                        {
                            // Handle success, if needed
                            Debug.Log("XP event desafio posted successfully.");
                        }
                        else
                        {
                            // Handle failure, if needed
                            Debug.LogError("Failed to post XP event desafio.");
                        }
                    }));
                    GameControlVariables.PuntuacionTotal += desafio1Success;
                }
                else if (registrosTotal == 5)
                {
                    GameControlVariables.Desafio2Finished = true;
                    StartCoroutine(apiManager.PostXpEvent(PlayerPrefs.GetInt("UserID"), 15, DateTime.Now, true, (isSuccess) => { // Define the callback
                        if (isSuccess)
                        {
                            // Handle success, if needed
                            Debug.Log("XP event desafio posted successfully.");
                        }
                        else
                        {
                            // Handle failure, if needed
                            Debug.LogError("Failed to post XP event desafio.");
                        }
                    }));
                    GameControlVariables.PuntuacionTotal += desafio2Success;
                }
                else if (registrosTotal == 7)
                {
                    GameControlVariables.Desafio3Finished = true;
                    StartCoroutine(apiManager.PostXpEvent(PlayerPrefs.GetInt("UserID"), 16, DateTime.Now, true, (isSuccess) => { // Define the callback
                        if (isSuccess)
                        {
                            // Handle success, if needed
                            Debug.Log("XP event desafio posted successfully.");
                        }
                        else
                        {
                            // Handle failure, if needed
                            Debug.LogError("Failed to post XP event desafio.");
                        }
                    }));
                    GameControlVariables.PuntuacionTotal += desafio3Success;
                }
                else if (registrosTotal == 10)
                {
                    GameControlVariables.DesafioFinal = true;
                    StartCoroutine(apiManager.PostXpEvent(PlayerPrefs.GetInt("UserID"), 17, DateTime.Now, true, (isSuccess) => { // Define the callback
                        if (isSuccess)
                        {
                            // Handle success, if needed
                            Debug.Log("XP event desafio posted successfully.");
                        }
                        else
                        {
                            // Handle failure, if needed
                            Debug.LogError("Failed to post XP event desafio.");
                        }
                    }));
                    GameControlVariables.PuntuacionTotal += desafioFinalSuccess;
                }

                //Desafio Ganado
                
                Time.timeScale = 1.0f; //Reanudar Juego
                panelDesafio.SetActive(false); //Quitar Panel

                Debug.Log("Desafio Ganado");
                
            }
            else
            {
                //Siguiente Fase del desafio
                ResetPanel();
                SetAnswers();
                progressText.text = (registrosTotal - registros).ToString() + "/" + (registrosTotal).ToString() + " REGISTRADOS";
            }
        }
        else
        {
            //Desafio Perdido
            if (registrosTotal == 3)
            {
                GameControlVariables.Desafio1Finished = false;

                StartCoroutine(apiManager.PostXpEvent(PlayerPrefs.GetInt("UserID"),14, DateTime.Now, false, (isSuccess) => { // Define the callback
                    if (isSuccess)
                    {
                        // Handle success, if needed
                        Debug.Log("-XP event posted successfully desafio.");
                    }
                    else
                    {
                        // Handle failure, if needed
                        Debug.LogError("Failed to post XP event.");
                    }
                }));
                GameControlVariables.PuntuacionTotal -= desafio1Fail;

            } else if (registrosTotal == 5)
            {
                GameControlVariables.Desafio2Finished = false;
                StartCoroutine(apiManager.PostXpEvent(PlayerPrefs.GetInt("UserID"), 15, DateTime.Now, false, (isSuccess) => { // Define the callback
                    if (isSuccess)
                    {
                        // Handle success, if needed
                        Debug.Log("-XP event posted successfully desafio.");
                    }
                    else
                    {
                        // Handle failure, if needed
                        Debug.LogError("Failed to post XP event.");
                    }
                }));
                GameControlVariables.PuntuacionTotal -= desafio2Fail;
            } else if (registrosTotal == 7)
            {
                GameControlVariables.Desafio3Finished = false;
                StartCoroutine(apiManager.PostXpEvent(PlayerPrefs.GetInt("UserID"), 16, DateTime.Now, false, (isSuccess) => { // Define the callback
                    if (isSuccess)
                    {
                        // Handle success, if needed
                        Debug.Log("-XP event posted successfully desafio.");
                    }
                    else
                    {
                        // Handle failure, if needed
                        Debug.LogError("Failed to post XP event.");
                    }
                }));
                GameControlVariables.PuntuacionTotal -= desafio3Fail;
            } else if (registrosTotal == 10)
            {
                GameControlVariables.DesafioFinal = false;
                StartCoroutine(apiManager.PostXpEvent(PlayerPrefs.GetInt("UserID"), 17, DateTime.Now, false, (isSuccess) => { // Define the callback
                    if (isSuccess)
                    {
                        // Handle success, if needed
                        Debug.Log("-XP event posted successfully desafio.");
                    }
                    else
                    {
                        // Handle failure, if needed
                        Debug.LogError("Failed to post XP event.");
                    }
                }));
                GameControlVariables.PuntuacionTotal -= desafioFinalFail;
            }


            Time.timeScale = 1.0f;
            panelDesafio.SetActive(false);
            Debug.Log("Desafio Perdido");
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
