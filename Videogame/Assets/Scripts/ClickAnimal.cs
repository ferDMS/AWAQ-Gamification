using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickAnimal : MonoBehaviour
{
    // M�todo que se ejecutar� cuando se haga clic en el objeto
    public void OnPointerClick()
    {
        Debug.Log("Cambio de Escena");
        // Cambiar a la escena especificada
        GameResultScene();
    }

    public void GameResultScene()
    {
        SceneManager.LoadScene("GameResult");
    }
}
