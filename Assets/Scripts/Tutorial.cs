using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour
{
    public GameObject ToucCollider;
    public Text Instruciones;
    private int contador;
    public GameObject panel;
    public GameObject Button;
    public static bool GamePause = false;
    
    // Start is called before the first frame update
    void Start()
    {
        ToucCollider.SetActive(false);
        Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (contador == 1)
        {
            Instruciones.text = " w";
        }
        else if (contador == 2)
        {
            ToucCollider.SetActive(true);
            QuitarInstrucciones();
            Resume();
            StartCoroutine(DestroyAfterDelay());
        }
    }

    public void IncrementarContador()
    {
        contador++;
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); // Espera medio segundo
        Destroy(Button);
        Destroy(gameObject);
    }

    public void QuitarInstrucciones()
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Intento de acceder a 'panel' que es null.");
        }

        if (Button != null)
        {
            Button.SetActive(false); // O Destroy(Button) si realmente quieres destruirlo.
        }
        else
        {
            Debug.LogWarning("Intento de acceder a 'Button' que es null.");
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GamePause = false;
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        GamePause = true;
    }
}
