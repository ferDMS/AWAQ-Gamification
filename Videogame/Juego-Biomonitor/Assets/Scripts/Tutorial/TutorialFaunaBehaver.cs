using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialFaunaBehaver : MonoBehaviour
{
    private bool estadoCaja;

    public float velocity;
    public float limitePositivo;
    public float limiteNegativo;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        estadoCaja = GameControlVariablesTutorial.GetToolState("Herramienta_Caja");

        if (spriteRenderer != null && spriteRenderer.flipX)
        {
            this.transform.position += Vector3.right * Time.deltaTime * velocity;
        }
        else
        {
            this.transform.position += Vector3.left * Time.deltaTime * velocity;
        }
        if (transform.position.x < limiteNegativo || transform.position.x > limitePositivo)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ObjectColliderForTouch") && this.gameObject.CompareTag("Mono"))
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
