using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;

public class FaunaBehaver : MonoBehaviour
{
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
        /*
        GameControl.OnToolStateChanged += UpdateToolState;
        UpdateToolState("Herramienta_Caja", GameControl.instance.objectStatus.Find(variable => variable.name == "Herramienta_Caja").state);
        UpdateToolState("Herramienta_Red", GameControl.instance.objectStatus.Find(variable => variable.name == "Herramienta_Red").state);
        UpdateToolState("Herramienta_Lupa", GameControl.instance.objectStatus.Find(variable => variable.name == "Herramienta_Lupa").state);
        UpdateToolState("Herramienta_Linterna", GameControl.instance.objectStatus.Find(variable => variable.name == "Herramienta_Linterna").state);
        */
    }

    /*
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
    */
    private void Update()
    {
        // Acceder al estado de la herramienta "Herramienta_Caja"
        estadoCaja = GameControlVariables.GetToolState("Herramienta_Caja");
        estadoRed = GameControlVariables.GetToolState("Herramienta_Red");
        estadoLupa = GameControlVariables.GetToolState("Herramienta_Lupa");
        estadoLinterna = GameControlVariables.GetToolState("Herramienta_Linterna");

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
            GameControlVariables.PuntutacionTotal += GameControlVariables.Bruite;
            GameObject.Destroy(this.gameObject);
            GameControlVariables.ConteoAnimales["Buitre"] += 1;

        }
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoRed && this.gameObject.CompareTag("Tucan"))
        {
            GameControlVariables.PuntutacionTotal += GameControlVariables.Tucan;
            GameObject.Destroy(this.gameObject);
            GameControlVariables.ConteoAnimales["Tucan"] += 1;

        }
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoCaja && this.gameObject.CompareTag("Lagarto"))
        {
            GameControlVariables.PuntutacionTotal += GameControlVariables.Lagarto;
            GameObject.Destroy(this.gameObject);
            GameControlVariables.ConteoAnimales["Lagarto"] += 1;

        }
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoCaja && this.gameObject.CompareTag("Oso"))
        {
            GameControlVariables.PuntutacionTotal += GameControlVariables.Oso;
            GameObject.Destroy(this.gameObject);
            GameControlVariables.ConteoAnimales["Oso"] += 1;

        }
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoCaja && this.gameObject.CompareTag("Mono"))
        {
            GameControlVariables.PuntutacionTotal += GameControlVariables.Mono;
            GameObject.Destroy(this.gameObject);
            GameControlVariables.ConteoAnimales["Mono"] += 1;

        }
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoLupa && this.gameObject.CompareTag("Ave_del_Paraiso"))
        {
            GameControlVariables.PuntutacionTotal += GameControlVariables.Ave_del_Paraiso;
            GameObject.Destroy(this.gameObject);
            GameControlVariables.ConteoAnimales["Ave_del_Paraiso"] += 1;

        }
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoLupa && this.gameObject.CompareTag("Orquidea"))
        {
            GameControlVariables.PuntutacionTotal += GameControlVariables.Orquidea;
            GameObject.Destroy(this.gameObject);
            GameControlVariables.ConteoAnimales["Orquidea"] += 1;

        }
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoLupa && this.gameObject.CompareTag("Palma"))
        {
            GameControlVariables.PuntutacionTotal += GameControlVariables.Palma;
            GameObject.Destroy(this.gameObject);
            GameControlVariables.ConteoAnimales["Palma"] += 1;

        }
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoLupa && this.gameObject.CompareTag("PALMAchica"))
        {
            GameControlVariables.PuntutacionTotal += GameControlVariables.PALMAchica;
            GameObject.Destroy(this.gameObject);
            GameControlVariables.ConteoAnimales["PALMAchica"] += 1;

        }
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoLupa && this.gameObject.CompareTag("arbolCacao"))
        {
            GameControlVariables.PuntutacionTotal += GameControlVariables.arbolCacao;
            GameObject.Destroy(this.gameObject);
            GameControlVariables.ConteoAnimales["arbolCacao"] += 1;

        }
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoLupa && this.gameObject.CompareTag("Arbusto"))
        {
            GameControlVariables.PuntutacionTotal += GameControlVariables.Arbusto;
            GameObject.Destroy(this.gameObject);
            GameControlVariables.ConteoAnimales["Arbusto"] += 1;

        }
    }
}
