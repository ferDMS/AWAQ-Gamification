using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public List<GameObject> animalGameObjects;
    public List<int> requiredScores; // Lista de puntuaciones requeridas para cada animal
    public ApiManager apiManager;
    public float maxHeight;
    public float minHeight;
    public float timeToSpawnMax;
    public float timeToSpawnMin;
    public float spawnPositionX;
    public bool flipOnSpawnPosition = true;

    IEnumerator SpawnerTiemr()
    {
        yield return new WaitForSeconds(Random.Range(timeToSpawnMin, timeToSpawnMax));

        // Obtener la puntuación total
        int totalScore = GameControlVariables.GetPuntuacionTotalInt();

        // Lista de índices de animales que pueden ser spawnados
        List<int> spawnableIndexes = new List<int>();

        // Determinar qué animales pueden ser spawnados
        for (int i = 0; i < animalGameObjects.Count; i++)
        {
            if (totalScore >= requiredScores[i])
            {
                spawnableIndexes.Add(i);
            }
        }

        if (spawnableIndexes.Count > 0)
        {
            // Seleccionar aleatoriamente un índice de animal spawnable
            int randomIndex = Random.Range(0, spawnableIndexes.Count);
            int selectedAnimalIndex = spawnableIndexes[randomIndex];

            // Determinar la posición de spawn X
            float spawnX = Random.Range(0f, 1f) > 0.5f ? spawnPositionX : transform.position.x;

            // Spawnear el animal seleccionado
            GameObject spawnedAnimal = Instantiate(animalGameObjects[selectedAnimalIndex],
                new Vector3(spawnX, transform.position.y + Random.Range(minHeight, maxHeight), 0),
                Quaternion.identity);

            // Voltear el sprite si es necesario
            SpriteRenderer spriteRenderer = spawnedAnimal.GetComponent<SpriteRenderer>();
            if (flipOnSpawnPosition && spawnX == spawnPositionX && spriteRenderer != null)
            {
                spriteRenderer.flipX = true;
            }
        }

        // Reiniciar la corrutina para el siguiente spawn
        StartCoroutine(SpawnerTiemr());
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnerTiemr());
    }

    // Update is called once per frame
    void Update()
    {
  
    }
}
