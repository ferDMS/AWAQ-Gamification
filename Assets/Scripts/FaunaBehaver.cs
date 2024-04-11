using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;

public class FaunaBehaver : MonoBehaviour
{
    //Llamamos al GameControl
    public GameControl GameContoler;

    // Variables para guardar los estados
    private bool estadoHerramientaCaja;
    private bool estadoHerramientaRed;
    private bool estadoHerramientaLupa;
    private bool estadoHerramientaLinterna;

    public float velocity;
    public float limitePositivo;
    public float limiteNegativo;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtener el componente SpriteRenderer

        // Acceder a la variable específica por su nombre
        BooleanVariable HerramientaCaja = GameControl.instance.objectStatus.Find(variable => variable.name == "Herramienta_Caja");
        BooleanVariable HerramientaRed = GameControl.instance.objectStatus.Find(variable => variable.name == "Herramienta_Red");
        BooleanVariable HerramientaLupa = GameControl.instance.objectStatus.Find(variable => variable.name == "Herramienta_Lupa");
        BooleanVariable HerramientaLinterna = GameControl.instance.objectStatus.Find(variable => variable.name == "Herramienta_Linterna");

        // Guardar el estado de cada variable
        estadoHerramientaCaja = HerramientaCaja != null ? HerramientaCaja.state : false;
        estadoHerramientaRed = HerramientaRed != null ? HerramientaRed.state : false;
        estadoHerramientaLupa = HerramientaLupa != null ? HerramientaLupa.state : false;
        estadoHerramientaLinterna = HerramientaLinterna != null ? HerramientaLinterna.state : false;

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
