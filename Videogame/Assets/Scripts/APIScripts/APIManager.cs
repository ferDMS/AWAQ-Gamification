using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Data;
using System.Text;

public class ApiManager : MonoBehaviour
{
    private const string baseUrl = "https://10.22.238.66:7044/QSBGame/";
    public bool IsDataReady = false;

    //private static ApiManager apiManager;
    private Dictionary<string, int> xpAnimales = new Dictionary<string, int>();
    private Dictionary<string, EspeciesModel> valsEspecies = new Dictionary<string, EspeciesModel>();
    private Dictionary<string, int> conteo = new Dictionary<string, int>();
    private int currentUserXP = 0;
    void Start()
    {
        int userId = PlayerPrefs.GetInt("UserID");
        StartCoroutine(FetchAndPopulateSpeciesData((success) =>
        {
            if (success)
            {
                Debug.Log("Successfully fetched and populated species data.");
            }
            else
            {
                Debug.LogError("Failed to fetch and populate species data.");
            }
        }));

        StartCoroutine(GetTotalXP(userId, (xp) =>
        {
            currentUserXP = xp;
            Debug.Log("Current XP WITHIN API MANAGER CONSTRUCTOR: " + currentUserXP.ToString());
        }));

        StartCoroutine(GetCapturasByUserID(userId, (success) => 
        {
            if (success)
            {
                Debug.Log("Animal counts updated successfully. Total captures: " + conteo.Count);
            }
            else
            {
                Debug.LogError("Failed to fetch animal counts.");
            }
        }));

    }

    public int GetAnimalCount(string animalName)
    {
        // Check if the dictionary contains the key
        if (conteo.ContainsKey(animalName))
        {
            //Debug.Log("Especie: " + animalName + " Count: " + conteo[animalName]);
            return conteo[animalName];
        }
        else
        {
            //Debug.LogWarning("Key not found in dictionary: " + animalName);
            return 0; // Return 0 or a default value if the key does not exist
        }
    }


    public int getCurrentXP()
    {
        return currentUserXP;
    }


    // Method to get the XP for a given especie
    public int GetEspecieXP(string especie)
    {
        Debug.Log("Especie: " + especie + " XP: " + xpAnimales[especie]);
        Debug.Log(xpAnimales[especie].GetType());
        return (int)xpAnimales[especie];
    }


    public int GetFuenteID(string especie)
    {
        return valsEspecies[especie].FuenteId;
    }

    public IEnumerator FetchAndPopulateSpeciesData(Action<bool> callback)
    {
        string url = baseUrl + "especies";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.useHttpContinue = true;
            var cert = new ForceAcceptAll();
            webRequest.certificateHandler = cert;
            cert?.Dispose();

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = webRequest.downloadHandler.text;
                List<EspeciesModel> speciesList = JsonConvert.DeserializeObject<List<EspeciesModel>>(jsonResponse);

                PopulateSpeciesDictionaries(speciesList);
                Debug.Log("Successfully fetched species data from API.");
                callback?.Invoke(true);
            }
            else
            {
                Debug.LogError("Error fetching species data from API: " + webRequest.error);
                callback?.Invoke(false);
            }
        }
    }

    // Method to populate dictionaries with species data
    private void PopulateSpeciesDictionaries(List<EspeciesModel> speciesList)
    {
        xpAnimales.Clear();
        valsEspecies.Clear();

        foreach (var especie in speciesList)
        {
            xpAnimales.Add(especie.NombreEspecie, especie.XpExito);
            //Debug.Log("Especie: " + especie.NombreEspecie + " XP: " + especie.XpExito);
            valsEspecies.Add(especie.NombreEspecie, especie);
            //Debug.Log("Valores: " + especie.NombreEspecie + " EspecieID: " + especie.EspecieId + " nombre fancy: " + especie.NombreCientifico );
        }
    }

    public IEnumerator PostXpEvent(int userId, int fuenteId, DateTime fecha, bool isSuccessful, Action<bool> callback)
    {
        string url = baseUrl + "PostXpEvent";
        Debug.Log("PostXpEvent");
        XPEventReq request = new XPEventReq { UserId = userId, FuenteId = fuenteId, Fecha =  fecha, IsSuccessful = isSuccessful };


        string jsonData = JsonUtility.ToJson(request);
        Debug.Log("Sending XP Event with JSON: " + jsonData);

        using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(url, ""))
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
                Debug.Log("XP event posted successfully.");
                callback?.Invoke(true);
            }
            else
            {
                Debug.Log("Error posting XP event: " + webRequest.error);
                Debug.LogError("Error: " + webRequest.error);
                callback?.Invoke(false);
            }
        }
    }

    public IEnumerator GetCapturasByUserID(int userId, Action<bool> callback)
    {
        string url = baseUrl + "capturas/" + userId;
        Debug.Log("Within GetCapturasByUserID");
        Debug.Log(url);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.useHttpContinue = true;
            var cert = new ForceAcceptAll();
            webRequest.certificateHandler = cert;
            cert?.Dispose();

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = webRequest.downloadHandler.text;
                List<Captura> capturasList = JsonConvert.DeserializeObject<List<Captura>>(jsonResponse);

                UpdateCapturasDictionary(capturasList);
                Debug.Log("Successfully fetched and updated capture counts.");
                callback?.Invoke(true);
            }
            else
            {
                Debug.LogError("Error fetching capture data: " + webRequest.error);
                callback?.Invoke(false);
            }
        }
    }

    private void UpdateCapturasDictionary(List<Captura> capturasList)
    {
        Debug.Log("within updatecapturasdictionary");
        conteo.Clear();
        foreach (var captura in capturasList)
        {
            conteo.Add(captura.NombreEspecie, captura.CaptureCount);
        }
        IsDataReady = true; // Indicate that data is ready
    }



    public IEnumerator GetHerramientasInfo(int id, Action<Herramienta> callback)
    {
        string url = baseUrl + "herramienta/" + id;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.useHttpContinue = true;
            var cert = new ForceAcceptAll();
            webRequest.certificateHandler = cert;
            cert?.Dispose();

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = webRequest.downloadHandler.text;
                Herramienta herramienta = JsonConvert.DeserializeObject<Herramienta>(jsonResponse);
                callback?.Invoke(herramienta);
            }
            else
            {
                Debug.LogError("Error: " + webRequest.error);
                callback?.Invoke(null);
            }
        }
    }

    public IEnumerator GetTotalXP(int userID, Action<int> callback)
    {
        string url = baseUrl + "totalXP/" + userID.ToString();
        Debug.Log("URL Get Total XP:" + url);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.useHttpContinue = true;
            var cert = new ForceAcceptAll();
            webRequest.certificateHandler = cert;
            cert?.Dispose();
            Debug.Log("Usando el GetTotalXP");

            yield return webRequest.SendWebRequest();
            Debug.Log("Web request result: " + webRequest.result);

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = webRequest.downloadHandler.text;
                int totalXP = JsonConvert.DeserializeObject<int>(jsonResponse);
                Debug.Log("Successfully fetched total XP.");
                callback?.Invoke(totalXP);
            }
            else
            {
                Debug.LogError("Error: " + webRequest.error);
                callback?.Invoke(0);
            }
        }
    }

    [Serializable]
    public class XPEventReq
    {
        public int UserId;
        public int FuenteId;
        public DateTime Fecha;
        public bool IsSuccessful;

    }

}