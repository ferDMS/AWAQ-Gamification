using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectorDeClick : MonoBehaviour
{
    void Update()
    {
        // Verificar si se hace clic
        if (Input.GetMouseButtonDown(0))
        {
            // Obtener la posici�n del clic
            Vector3 clicPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Realizar un raycast desde la posici�n del clic
            RaycastHit2D hit = Physics2D.Raycast(clicPosition, Vector2.zero);

            // Verificar si se ha encontrado un objeto
            if (hit.collider != null)
            {
                // Si se ha encontrado un objeto, imprimir su nombre en la consola
                Debug.Log("Se hizo clic en: " + hit.collider.name);
                if (hit.collider.name == "Animales_5")
                {
                    GameResultScene();
                }
            }
            else
            {
                // Si no se ha encontrado ning�n objeto, imprimir un mensaje en la consola
                Debug.Log("No se encontr� ning�n objeto frente al clic.");
            }
        }
    }

    public void GameResultScene()
    {
        SceneManager.LoadScene("GameResult");
    }
}
