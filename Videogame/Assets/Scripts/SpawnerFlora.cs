using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerFlora : MonoBehaviour
{
    public List<GameObject> objectsToSpawn;
    public float timeToSpawnMin;
    public float timeToSpawnMax;
    public float eyeLifetime = 2f;
    public float minHeight = -2f; // Altura m�nima
    public float maxHeight = 2f; // Altura m�xima
    public float minWidth = -3f; // Anchura m�nima
    public float maxWidth = 3f; // Anchura m�xima

    IEnumerator SpawnerTimer()
    {
        while (true)
        {
            float cameraHeight = Camera.main.orthographicSize * 2f;
            float cameraWidth = cameraHeight * Camera.main.aspect;

            yield return new WaitForSeconds(Random.Range(timeToSpawnMin, timeToSpawnMax));

            // Obtener el valor de experiencia total del GameControllerVariables
            int experienciaTotal = GameControlVariables.PuntutacionTotal;

            // Seleccionar el �ndice del objeto a spawnear basado en la experiencia total
            int selectedIndex = 0; // Por defecto, el primer objeto de la lista
            if (experienciaTotal >= 0)
            {
                selectedIndex = 0; // Por ejemplo, el segundo objeto de la lista
            } else if (experienciaTotal >= 2500)
            {
                selectedIndex = 1;
            } else if (experienciaTotal >= 7500)
            {
                selectedIndex = 2;
            } else if (experienciaTotal >= 20000)
            {
                selectedIndex = 3;
            } else if (experienciaTotal >= 35000)
            {
                selectedIndex = 4;
            }
            // Agregar m�s condiciones seg�n sea necesario para seleccionar los objetos de la lista basados en la experiencia

            // Asegurar que el �ndice seleccionado est� dentro del rango v�lido
            selectedIndex = Mathf.Clamp(selectedIndex, 0, objectsToSpawn.Count - 1);

            // Seleccionar el prefab a spawnear basado en el �ndice seleccionado
            GameObject prefabToSpawn = objectsToSpawn[selectedIndex];

            // Generar posiciones aleatorias dentro del rango
            float randomY = Random.Range(minHeight, maxHeight); // Coordenada Y aleatoria dentro del rango
            float randomX = Random.Range(minWidth, maxWidth); // Coordenada X aleatoria dentro del rango

            // Spawnear el objeto seleccionado en la posici�n aleatoria
            GameObject newObject = Instantiate(prefabToSpawn, new Vector3(randomX, randomY, 0), Quaternion.identity);

            // Destruir el objeto despu�s de un tiempo determinado
            Destroy(newObject, eyeLifetime);
        }
    }

    void Start()
    {
        StartCoroutine(SpawnerTimer());
    }
}
