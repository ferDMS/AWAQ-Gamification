using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ChangeScene : MonoBehaviour
{
    

    public GameObject panel_Flora;
    public GameObject panel_Fauna;
    public Sprite Active;
    public Sprite Inactive;
    public Button Flora;
    public Button Fauna;

    public void LogIn()
    {
        SceneManager.LoadScene("LogIn");
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("MenuJuego");
    }

    public void InformacionScene()
    {
        SceneManager.LoadScene("PanelInformacion");
    }

    public void BodegaScene()
    {
        SceneManager.LoadScene("Bodega");
    }

    public void ExteriorScene()
    {
        SceneManager.LoadScene("Exterior");
    }

    public void TutotialScene()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void GameResultScene()
    {
        SceneManager.LoadScene("GameResult");
    }

    public void ActivarFauna()
    {
        Image fauna = Fauna.GetComponent<Image>();
        fauna.sprite = Active;
        Image flora = Flora.GetComponent<Image>();
        flora.sprite = Inactive;
        panel_Fauna.SetActive(true);
        panel_Flora.SetActive(false);
    }

    public void ActivarFlora()
    {
        Image fauna = Fauna.GetComponent<Image>();
        fauna.sprite = Inactive;
        Image flora = Flora.GetComponent<Image>();
        flora.sprite = Active;
        panel_Fauna.SetActive(false);
        panel_Flora.SetActive(true);
    }

    public void FinalizarJuego()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
          
    }
}
