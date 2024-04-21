using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> animalGameObjects;
    public float maxHeight;
    public float minHeight;
    public float timeToSpawnMax;
    public float timeToSpawnMin;
    public float spawnPositionX;
    public bool flipOnSpawnPosition = true;

    IEnumerator SpawnerTiemr()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(timeToSpawnMin, timeToSpawnMax));

        // Obtener el valor de experiencia total del GameControllerVariables
        int experienciaTotal = GameControlVariables.PuntutacionTotal;

        // Verificar el valor de experiencia total para determinar qué animal spawnear
        GameObject selectedAnimal = null;

        if (experienciaTotal >= 0)
        {
            selectedAnimal = animalGameObjects[0]; // Por ejemplo, el primer objeto de la lista
        }
        else if (experienciaTotal >= 7500)
        {
            selectedAnimal = animalGameObjects[1]; // Por ejemplo, el segundo objeto de la lista
        }
        else if (experienciaTotal >= 50000)
        {
            selectedAnimal = animalGameObjects[2];
        }
        // Agregar más condiciones según sea necesario para seleccionar los objetos de la lista basados en la experiencia

        if (selectedAnimal != null)
        {
            // Determinar la posición de spawn X
            float spawnX = UnityEngine.Random.Range(0f, 1f) > 0.5f ? spawnPositionX : transform.position.x;

            // Spawnear el animal seleccionado
            GameObject spawnedAnimal = Instantiate(selectedAnimal, new Vector3(spawnX, transform.position.y + UnityEngine.Random.Range(minHeight, maxHeight), 0), Quaternion.identity);

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
