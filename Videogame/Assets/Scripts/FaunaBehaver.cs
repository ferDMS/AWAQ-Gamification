using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class FaunaBehaver : MonoBehaviour
{
    public ApiManager apiManager;
    // Variables para guardar los estados
    private bool estadoCaja;
    private bool estadoRed;
    private bool estadoLupa;
    private bool estadoLinterna;

    public float velocity;
    public float limitePositivo;
    public float limiteNegativo;
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtener el componente SpriteRenderer
        apiManager = GameObject.Find("APIManager").GetComponent<ApiManager>();
    }

    private void Update()
    {
        // Acceder al estado de la herramienta "Herramienta_Caja"
        estadoCaja = IniciarHerramientas.GetToolState("Herramienta_Caja");
        estadoRed = IniciarHerramientas.GetToolState("Herramienta_Red");
        estadoLupa = IniciarHerramientas.GetToolState("Herramienta_Lupa");
        estadoLinterna = IniciarHerramientas.GetToolState("Herramienta_Linterna");

        // Cambiar la direcci�n de movimiento si flipX es true
        if (spriteRenderer != null && spriteRenderer.flipX)
        {
            this.transform.position += Vector3.right * Time.deltaTime * velocity;
        }
        else
        {
            this.transform.position += Vector3.left * Time.deltaTime * velocity;
        }

        // Destruir el objeto si sale del l�mite
        if (transform.position.x < limiteNegativo || transform.position.x > limitePositivo)
        {
            GameObject.Destroy(this.gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoRed && this.gameObject.CompareTag("Buitre"))
        { 
           
            Debug.Log(apiManager.GetEspecieXP("Cóndor Andino"));
            GameControlVariables.AddXP((int)apiManager.GetEspecieXP("Cóndor Andino"));

            StartCoroutine(apiManager.PostXpEvent(PlayerPrefs.GetInt("UserID"), apiManager.GetFuenteID("Cóndor Andino"), DateTime.Now, true, (isSuccess) => { // Define the callback
                if (isSuccess)
                {
                    // Handle success, if needed
                    Debug.Log("XP event posted successfully.");
                }
                else
                {
                    // Handle failure, if needed
                    Debug.LogError("Failed to post XP event.");
                }
            }));
            GameObject.Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoRed && this.gameObject.CompareTag("Tucan"))
        {
            Debug.Log(apiManager.GetEspecieXP("Tucán Pechiblanco"));
            GameControlVariables.AddXP((int)apiManager.GetEspecieXP("Tucán Pechiblanco"));


            StartCoroutine(apiManager.PostXpEvent(PlayerPrefs.GetInt("UserID"), apiManager.GetFuenteID("Tucán Pechiblanco"), DateTime.Now, true, (isSuccess) => { // Define the callback
                if (isSuccess)
                {
                    // Handle success, if needed
                    Debug.Log("XP event posted successfully.");
                }
                else
                {
                    // Handle failure, if needed
                    Debug.LogError("Failed to post XP event.");
                }
            }));
            GameObject.Destroy(this.gameObject);

        }
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoCaja && this.gameObject.CompareTag("Lagarto"))
        {

            Debug.Log(apiManager.GetEspecieXP("Lagarto Punteado"));
            GameControlVariables.AddXP((int)apiManager.GetEspecieXP("Lagarto Punteado"));

            StartCoroutine(apiManager.PostXpEvent(PlayerPrefs.GetInt("UserID"), apiManager.GetFuenteID("Lagarto Punteado"), DateTime.Now, true, (isSuccess) => { // Define the callback
                if (isSuccess)
                {
                    // Handle success, if needed
                    Debug.Log("XP event posted successfully.");
                }
                else
                {
                    // Handle failure, if needed
                    Debug.LogError("Failed to post XP event.");
                }
            }));
            GameObject.Destroy(this.gameObject);

        }
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoCaja && this.gameObject.CompareTag("Oso"))
        {
            Debug.Log(apiManager.GetEspecieXP("Oso de Anteojos"));
            GameControlVariables.AddXP((int)apiManager.GetEspecieXP("Oso de Anteojos"));

            StartCoroutine(apiManager.PostXpEvent(PlayerPrefs.GetInt("UserID"), apiManager.GetFuenteID("Oso de Anteojos"), DateTime.Now, true, (isSuccess) => { // Define the callback
                if (isSuccess)
                {
                    // Handle success, if needed
                    Debug.Log("XP event posted successfully.");
                }
                else
                {
                    // Handle failure, if needed
                    Debug.LogError("Failed to post XP event.");
                }
            }));
            GameObject.Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoCaja && this.gameObject.CompareTag("Mono"))
        {

            Debug.Log(apiManager.GetEspecieXP("Tití Ornamentado"));
            GameControlVariables.AddXP((int)apiManager.GetEspecieXP("Tití Ornamentado"));


            StartCoroutine(apiManager.PostXpEvent(PlayerPrefs.GetInt("UserID"), apiManager.GetFuenteID("Tití Ornamentado"), DateTime.Now, true, (isSuccess) => { // Define the callback
                if (isSuccess)
                {
                    // Handle success, if needed
                    Debug.Log("XP event posted successfully.");
                }
                else
                {
                    // Handle failure, if needed
                    Debug.LogError("Failed to post XP event.");
                }
            }));
            GameObject.Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoLupa && this.gameObject.CompareTag("Ave_del_Paraiso"))
        {


            Debug.Log(apiManager.GetEspecieXP("Ave del Paraíso"));
            GameControlVariables.AddXP((int)apiManager.GetEspecieXP("Ave del Paraíso"));


            StartCoroutine(apiManager.PostXpEvent(PlayerPrefs.GetInt("UserID"), apiManager.GetFuenteID("Ave del Paraíso"), DateTime.Now, true, (isSuccess) => { // Define the callback
                if (isSuccess)
                {
                    // Handle success, if needed
                    Debug.Log("XP event posted successfully.");
                }
                else
                {
                    // Handle failure, if needed
                    Debug.LogError("Failed to post XP event.");
                }
            }));
            GameObject.Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoLupa && this.gameObject.CompareTag("Orquidea"))
        {

            Debug.Log(apiManager.GetEspecieXP("Orquídea flor de Mayo"));
            GameControlVariables.AddXP((int)apiManager.GetEspecieXP("Orquídea flor de Mayo"));

            StartCoroutine(apiManager.PostXpEvent(PlayerPrefs.GetInt("UserID"), apiManager.GetFuenteID("Orquídea flor de Mayo"), DateTime.Now, true, (isSuccess) => { // Define the callback
                if (isSuccess)
                {
                    // Handle success, if needed
                    Debug.Log("XP event posted successfully.");
                }
                else
                {
                    // Handle failure, if needed
                    Debug.LogError("Failed to post XP event.");
                }
            }));
            GameObject.Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoLupa && this.gameObject.CompareTag("Palma"))
        {
            Debug.Log(apiManager.GetEspecieXP("Palma de Cera del Quindío"));
            GameControlVariables.AddXP((int)apiManager.GetEspecieXP("Palma de Cera del Quindío"));

            StartCoroutine(apiManager.PostXpEvent(PlayerPrefs.GetInt("UserID"), apiManager.GetFuenteID("Palma de Cera del Quindío"), DateTime.Now, true, (isSuccess) => { // Define the callback
                if (isSuccess)
                {
                    // Handle success, if needed
                    Debug.Log("XP event posted successfully.");
                }
                else
                {
                    // Handle failure, if needed
                    Debug.LogError("Failed to post XP event.");
                }
            }));
            GameObject.Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoLupa && this.gameObject.CompareTag("PALMAchica"))
        {
            Debug.Log(apiManager.GetEspecieXP("Frailejones"));
            GameControlVariables.AddXP((int)apiManager.GetEspecieXP("Frailejones"));

            StartCoroutine(apiManager.PostXpEvent(PlayerPrefs.GetInt("UserID"), apiManager.GetFuenteID("Frailejones"), DateTime.Now, true, (isSuccess) => { // Define the callback
                if (isSuccess)
                {
                    // Handle success, if needed
                    Debug.Log("XP event posted successfully.");
                }
                else
                {
                    // Handle failure, if needed
                    Debug.LogError("Failed to post XP event.");
                }
            }));
            GameObject.Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoLupa && this.gameObject.CompareTag("arbolCacao"))
        {
            Debug.Log(apiManager.GetEspecieXP("Arbol de Cacao"));
            GameControlVariables.AddXP((int)apiManager.GetEspecieXP("Arbol de Cacao"));


            StartCoroutine(apiManager.PostXpEvent(PlayerPrefs.GetInt("UserID"), apiManager.GetFuenteID("Arbol de Cacao"), DateTime.Now, true, (isSuccess) => { // Define the callback
                if (isSuccess)
                {
                    // Handle success, if needed
                    Debug.Log("XP event posted successfully.");
                }
                else
                {
                    // Handle failure, if needed
                    Debug.LogError("Failed to post XP event.");
                }
            }));
            GameObject.Destroy(this.gameObject);
        }
    
    }
}
