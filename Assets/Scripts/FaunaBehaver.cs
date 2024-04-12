using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;

public class FaunaBehaver : MonoBehaviour
{
    // Variables para guardar los estados
    public bool estadoHerramientaCaja;
    public bool estadoHerramientaRed;
    public bool estadoHerramientaLupa;
    public bool estadoHerramientaLinterna;

    public float velocity;
    public float limitePositivo;
    public float limiteNegativo;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtener el componente SpriteRenderer

        GameControl.OnToolStateChanged += UpdateToolState;
        UpdateToolState("Herramienta_Caja", GameControl.instance.objectStatus.Find(variable => variable.name == "Herramienta_Caja").state);
        UpdateToolState("Herramienta_Red", GameControl.instance.objectStatus.Find(variable => variable.name == "Herramienta_Red").state);
        UpdateToolState("Herramienta_Lupa", GameControl.instance.objectStatus.Find(variable => variable.name == "Herramienta_Lupa").state);
        UpdateToolState("Herramienta_Linterna", GameControl.instance.objectStatus.Find(variable => variable.name == "Herramienta_Linterna").state);

    }
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

    private void Update()
    {

        // Cambiar la dirección de movimiento si flipX es true
        if (spriteRenderer != null && spriteRenderer.flipX)
        {
            this.transform.position += Vector3.right * Time.deltaTime * velocity;
        }
        else
        {
            this.transform.position += Vector3.left * Time.deltaTime * velocity;
        }

        // Destruir el objeto si sale del límite
        if (transform.position.x < limiteNegativo || transform.position.x > limitePositivo)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoHerramientaRed && this.gameObject.CompareTag("Buitre"))
        {
            GameControl.instance.PuntutacionTotal += GameControl.instance.Bruite;
            GameObject.Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoHerramientaRed && this.gameObject.CompareTag("Tucan"))
        {
            GameControl.instance.PuntutacionTotal += GameControl.instance.Tucan;
            GameObject.Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoHerramientaCaja && this.gameObject.CompareTag("Lagarto"))
        {
            GameControl.instance.PuntutacionTotal += GameControl.instance.Lagarto;
            GameObject.Destroy(this.gameObject);    
        }
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoHerramientaCaja && this.gameObject.CompareTag("Oso"))
        {
            GameControl.instance.PuntutacionTotal += GameControl.instance.Oso;
            GameObject.Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && estadoHerramientaCaja && this.gameObject.CompareTag("Mono"))
        {
            GameControl.instance.PuntutacionTotal += GameControl.instance.Mono;
            GameObject.Destroy(this.gameObject);
        }
    }
}
