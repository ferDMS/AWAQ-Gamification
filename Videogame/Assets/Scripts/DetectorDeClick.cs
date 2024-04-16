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
            // Obtener la posición del clic
            Vector3 clicPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Realizar un raycast desde la posición del clic
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
                // Si no se ha encontrado ningún objeto, imprimir un mensaje en la consola
                Debug.Log("No se encontró ningún objeto frente al clic.");
            }
        }
    }

    public void GameResultScene()
    {
        SceneManager.LoadScene("GameResult");
    }
}
