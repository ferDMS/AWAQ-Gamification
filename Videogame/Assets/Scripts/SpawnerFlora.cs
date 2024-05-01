using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerFlora : MonoBehaviour
{
    public List<GameObject> objectsToSpawn;
    public List<int> requiredScores; // Lista de puntuaciones requeridas para cada objeto
    public ApiManager apiManager;
    public float timeToSpawnMin;
    public float timeToSpawnMax;
    public float eyeLifetime = 2f;
    public float minHeight = -2f; // Altura mínima
    public float maxHeight = 2f; // Altura máxima
    public float minWidth = -3f; // Anchura mínima
    public float maxWidth = 3f; // Anchura máxima

    IEnumerator SpawnerTimer()
    {
        while (true)
        {
            float cameraHeight = Camera.main.orthographicSize * 2f;
            float cameraWidth = cameraHeight * Camera.main.aspect;

            yield return new WaitForSeconds(Random.Range(timeToSpawnMin, timeToSpawnMax));

            // Obtener el valor de experiencia total del GameControllerVariables
            int totalScore = GameControlVariables.GetPuntuacionTotalInt();

            // Lista de índices de objetos que pueden ser spawnados
            List<int> spawnableIndexes = new List<int>();

            // Determinar qué objetos pueden ser spawnados
            for (int i = 0; i < objectsToSpawn.Count; i++)
            {
                if (totalScore >= requiredScores[i])
                {
                    spawnableIndexes.Add(i);
                }
            }

            if (spawnableIndexes.Count > 0)
            {
                // Seleccionar aleatoriamente un índice de objeto spawnable
                int randomIndex = Random.Range(0, spawnableIndexes.Count);
                int selectedObjectIndex = spawnableIndexes[randomIndex];

                // Seleccionar el prefab a spawnear basado en el índice seleccionado
                GameObject prefabToSpawn = objectsToSpawn[selectedObjectIndex];

                // Generar posiciones aleatorias dentro del rango
                float randomY = Random.Range(minHeight, maxHeight); // Coordenada Y aleatoria dentro del rango
                float randomX = Random.Range(minWidth, maxWidth); // Coordenada X aleatoria dentro del rango

                // Spawnear el objeto seleccionado en la posición aleatoria
                GameObject newObject = Instantiate(prefabToSpawn, new Vector3(randomX, randomY, 0), Quaternion.identity);

                // Destruir el objeto después de un tiempo determinado
                Destroy(newObject, eyeLifetime);
            }
        }
    }

    void Start()
    {
        StartCoroutine(SpawnerTimer());
    }
}

