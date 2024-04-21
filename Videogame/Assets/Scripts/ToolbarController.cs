using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarController : MonoBehaviour
{
    public GameObject toolbar;
    
    public Renderer caja;
    public Collider2D colliderCaja;
    public Renderer red;
    public Collider2D colliderRed;
    public Renderer lupa;
    public Collider2D colliderLupa;
    public Renderer linterna;
    public Collider2D colliderLinterna;

    public void Start()
    {
        colliderLupa.enabled = false;
        colliderLinterna.enabled = false;

        SetTransparency(lupa, 0.2f);
        SetTransparency(linterna, 0.2f);
    }

    public void EnableToolbarItems()
    {
        colliderLupa.enabled = true;
        colliderLinterna.enabled = true;

        SetTransparency(lupa, 1f);
        SetTransparency(linterna, 1f);
    }

    public void SetTransparency(Renderer objeto,  float transparency)
    {
        Color color = objeto.material.color;
        color.a = transparency;
        objeto.material.color = color;
    }
}
