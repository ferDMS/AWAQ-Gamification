using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public ApiManager apiManager;
    int currentxp = 0;
    int maxPuntacion = 100000;
    [SerializeField] private InputField emailInputField;
    [SerializeField] private InputField passwordInputField;
    [SerializeField] private Text responseText; // componente UI para hacer display de la respuesta del login
    private const string baseUrl = "https://10.22.238.66:7044/QSB/login";


    public void AttemptLogin()
    {
        StartCoroutine(Login(emailInputField.text, passwordInputField.text));
    }

    private void goTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    private void goMenu()
    {
        SceneManager.LoadScene("MenuJuego");

    }

    private void goEnd()
    {
        SceneManager.LoadScene("GameResult");
    }

    private IEnumerator Login(string email, string password)
    {
        LoginRequest loginRequest = new LoginRequest { Email = email, Password = password };
        string jsonData = JsonUtility.ToJson(loginRequest);
        Debug.Log("URL: " + baseUrl);
        using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(baseUrl, ""))
        {
            byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);
            webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.useHttpContinue = true;
            var cert = new ForceAcceptAll();
            webRequest.certificateHandler = cert;
            cert?.Dispose();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                // Deserialize and handle the response if the request was successful
                LoginResponse response = JsonUtility.FromJson<LoginResponse>(webRequest.downloadHandler.text);
                responseText.text = "Login Válido";
                PlayerPrefs.SetInt("UserID", response.userId);
                PlayerPrefs.Save();
                Debug.Log("User ID saved: " + response.userId);



                Initialize(apiManager);
                StartCoroutine(GetUserXP(response.userId));
            }
            else
            {
                if (webRequest.result == UnityWebRequest.Result.ProtocolError && webRequest.responseCode == 404)
                {
                    // Handle specific 404 error response
                    ErrorResponse errorResponse = JsonUtility.FromJson<ErrorResponse>(webRequest.downloadHandler.text);
                    responseText.text = $"Error: {errorResponse.error}";
                }
                else
                {
                    // General error handling
                    responseText.text = "Login Inválido: " + webRequest.error;
                }
            }
        }
    }

    private IEnumerator GetUserXP(int userId)
    {
        Debug.Log("getting user xp within coroutine");
        yield return StartCoroutine(apiManager.GetTotalXP(userId, (xp) =>
        {
            currentxp = xp;
            PlayerPrefs.SetInt("InitXP", currentxp);
            PlayerPrefs.Save();
            Debug.Log("XP fetched: " + currentxp);
            GameControlVariables.setPuntuacionTotal(currentxp);
            Debug.Log("XP set in GameControlVariables: " + GameControlVariables.GetPuntuacionTotalString());

            // Inicializar estado de desafíos según la cantidad de XP del usuario al loguearse
            if (currentxp >= 7500)
            {
                GameControlVariables.Desafio1Finished = true;
            }
            if (currentxp >= 20000)
            {
                GameControlVariables.Desafio2Finished = true;
            }
            if (currentxp >= 50000)
            {
                GameControlVariables.Desafio3Finished = true;
            }
            if (currentxp >= maxPuntacion)
            {
                GameControlVariables.DesafioFinal = true;
            }

            if (currentxp >= maxPuntacion ) // es 100,000 para ser biomonitor
            {
                goEnd();
            }
            else if(currentxp > 0) {
                goMenu();
            }
            else
            {
                goTutorial();
            }
        }));
    }

    public void Initialize(ApiManager manager)
    {
        Debug.Log("Inicializandooo Api manager desde LoginManager");
        if (manager == null)
        {
            Debug.Log("was called with a null ApiManager.");
            return;
        }

        apiManager = manager;
    }

    [System.Serializable]
    private class LoginRequest
    {
        public string Email;
        public string Password;
    }

    [System.Serializable]
    private class LoginResponse
    {
        public int userId;
        public string role;
    }

    [System.Serializable]
    private class ErrorResponse
    {
        public string error; 
    }
}
