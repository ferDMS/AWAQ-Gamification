using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEditorInternal;
using UnityEngine.InputSystem.XR;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    //RegristroController
    public GameObject panelRegistro;
    public GameObject toolBar;
    public GameObject UIExtras;
    public GameObject menuPanel;

    //GameControl
    public GameObject _gameControl;

    //Lo normal
    public GameObject ToucCollider;
    public Text Instrucciones;
    private int _contador = 0;
    public GameObject panel;
    public GameObject Button;
    public static bool GamePause = false;
    private bool detenerMovimiento = false;

    //Animales para hardcodear todo el tutorial.
    public GameObject Tucan;
    public GameObject Buitre;
    public GameObject Mono;
    public GameObject Oso;
    public GameObject Lagarto;
    public GameObject Mono2;

    //Herramientas para harcodear todo el tutorial
    public GameObject herramientaCaja;
    public GameObject herramientaRed;

    //Animaciones Animales
    public Animator animationTucan;
    public Animator animationOso;
    public Animator animationBuitre;
    public Animator animationMono;
    public Animator animationMono2;
    public Animator animationLagarto;

    //Animacion herramientas
    public Animator animacionCaja;
    public Animator animacionRed;

    //Canvas del tutorial.
    public Canvas miCanvas;

    // Start is called before the first frame update
    void Start()
    {
        ToucCollider.SetActive(false);
        contador = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (Lagarto.transform.position.x <= -25)
        {
            detenerMovimiento = true;
            animationTucan.enabled = false;
            animationOso.enabled = false;
            animationBuitre.enabled = false;
            animationMono.enabled = false;
            animationLagarto.enabled = false;
        }
        
        if (panelRegistro.GetComponent<RegistroController>().CheckAnswers() == true && contador == 12)
        {
            contador = 13;
            ActivarInstrucciones();
            animacionCaja.enabled = false;
            animacionRed.enabled = false;
            miCanvas.sortingOrder = 1;
            Button.SetActive(true);
        }
    }

    public int contador
    {
        get => _contador;
        private set
        {
            _contador = value;
            HandleCounterChange(_contador);
        }
    }
    private void HandleCounterChange(int currentCount)
    {
        switch (currentCount)
        {
            case 1:
                Instrucciones.text = "Como puedes notar nos encontramos en lo profundo de la selva, llena de diferentes tipos de flora y fauna,";
                break;
            case 2:
                Instrucciones.text = "Tu tarea principal como biomonitar será hacer un registro de todos los seres vivos que te encuentres.";
                StartCoroutine(AnimalsMove());
                break;
            // Añade más casos según sea necesario.
            case 3:
                Instrucciones.text = "Pero, ¿Como podre hacer esta complicada tarea?";
                animationTucan.enabled = true;
                animationOso.enabled = true;
                animationBuitre.enabled = true;
                animationMono.enabled = true;
                animationLagarto.enabled = true;
                StartCoroutine(continuarMoviemiento());
                break;
            case 4:
                Instrucciones.text = "Ntp, como puedes ver en tu pantalla, en la parte inferior cuentas con una barra con diferentes herramientas para poder capturar a los diferentes animales que te encuentres.";
                animacionCaja.SetTrigger("CajaSelected");
                animacionRed.SetTrigger("RedSelected");
                break;
            case 5:
                Instrucciones.text = "Cada hermienta sirve para diferentes cosas, por ejemplo la caja funciona para la mayoria de los animales terrestres";
                animacionRed.SetTrigger("RedDeselected"); 
                break;
            case 6:
                Instrucciones.text = "Y la red para la mayoria de los animales voladores.";
                animacionCaja.SetTrigger("CajaDeselected");
                animacionRed.SetTrigger("RedSelected");
                Mono2.SetActive(true);
                break;
            case 7:
                Instrucciones.text = "Oh mira parece que se un mono se nos acerco, usa la caja para poder capturarlo";
                animacionRed.SetTrigger("RedDeselected");
                ToucCollider.SetActive(true);
                Button.SetActive(false);
                miCanvas.sortingOrder = 1;
                StartCoroutine(movimientoMono2());
                break;
            case 8:
                Instrucciones.text = "Cada vez que encuentres un animal nuevo te aparecera esta ventana de registro.";
                break;
            case 9:
                Instrucciones.text = "Tiene que llenar todos los datos de manera correcta para poder continuar.";
                break;
            case 10:
                Instrucciones.text = "Ahorita yo te voy a llenar los datos";
                break;
            case 11:
                Instrucciones.text = "Info del mono";
                break;
            case 12:
                QuitarInstrucciones();
                miCanvas.sortingOrder = 20;
                break;
            case 13:
                Instrucciones.text = "Una vez que registres un animal no va ser necesario volver a hacer el registro hasta que te encuentres con algun reto en el futuro.";
                break;
            case 14:
                Instrucciones.text = "Ya terminamos aqui por hoy, volvamos a los headquarters.";
                break;
            case 15:
                toolBar.SetActive(false);
                UIExtras.SetActive(false);
                menuPanel.SetActive(true);
                Instrucciones.text = "Este es el menu del juego donde podras encontrar informacion de las herramientas, flora y fauna del juego";
                break;
            case 16:
                Instrucciones.text = "En la 'Información' te puedes encontrar con la información de las herramientas y cuando se desbloquean";
                break;
            case 17:
                Instrucciones.text = "En el apartado de Bodega puedes buscar información sobre la flora y fauna, como también la cantidad de veces que les haz echo registro";
                break;
            case 18:
                Instrucciones.text = "Bueno, esto es todo por mi parte, si quieres aventurarte a pica al boton de \"Exterior\" para encontrarte con más animales increibles.";
                break;
            case 19:
                Destroy(_gameControl);
                MenuScene();
                break;
            default:
                // Opcionalmente maneja valores de contador inesperados.
                Debug.Log("El contador está en un valor no manejado: " + currentCount);
                break;
        }
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("MenuJuego");
    }
    public void IncrementarContador()
    {
        contador++;
    }

    public void BotonPresionado()
    {
        IncrementarContador();
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

    public void ActivarInstrucciones()
    {
        if (panel != null)
        {
            panel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Intento de acceder a 'panel' que es null.");
        }

        if (Button != null)
        {
            Button.SetActive(true); // O Destroy(Button) si realmente quieres destruirlo.
        }
        else
        {
            Debug.LogWarning("Intento de acceder a 'Button' que es null.");
        }
    }

    IEnumerator AnimalsMove()
    {
        float duration = 1f; // Mover por 1 segundo
        float elapsed = 0f;

        while (elapsed < duration)
        {
            Tucan.transform.position += Vector3.left * Time.deltaTime * 10;
            Mono.transform.position += Vector3.right * Time.deltaTime * 10;
            Buitre.transform.position += Vector3.left * Time.deltaTime * 10;
            Oso.transform.position += Vector3.left * Time.deltaTime * 10;
            Lagarto.transform.position += Vector3.left * Time.deltaTime * 10;

            elapsed += Time.deltaTime;
            yield return null;
        }
        animationTucan.enabled = false;
        animationOso.enabled = false;
        animationBuitre.enabled = false;
        animationMono.enabled = false;
        animationLagarto.enabled = false;
    }

    IEnumerator continuarMoviemiento()
    {
        while (!detenerMovimiento)  // Este bucle infinito continuará hasta que explícitamente detengas la corutina
        {
            Tucan.transform.position += Vector3.left * Time.deltaTime * 10;
            Mono.transform.position += Vector3.right * Time.deltaTime * 10;
            Buitre.transform.position += Vector3.left * Time.deltaTime * 10;
            Oso.transform.position += Vector3.left * Time.deltaTime * 10;
            Lagarto.transform.position += Vector3.left * Time.deltaTime * 10;
            yield return null;  // Esto causará que la corutina espere hasta el próximo frame antes de continuar
        }
    }

    IEnumerator movimientoMono2()
    {
        float duration = 1f; // Duración del movimiento en segundos
        float elapsed = 0f;

        FaunaBehaver faunaBehaver = Mono2.GetComponent<FaunaBehaver>();

        // Guardamos la velocidad original para restaurarla al final
        float velocidadOriginal = faunaBehaver.velocity;

        // Cambiamos la velocidad a 10 durante la animación
        faunaBehaver.velocity = 10;

        // Esperamos durante la duración especificada
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return null; // Esperamos un frame
        }

        // Restauramos la velocidad original
        faunaBehaver.velocity = velocidadOriginal;
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
