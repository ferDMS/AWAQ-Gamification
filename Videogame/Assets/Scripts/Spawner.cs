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

        float spawnX = UnityEngine.Random.Range(0f,1f) > 0.5f ? spawnPositionX : transform.position.x;
        
        GameObject selectedAnimal = animalGameObjects[UnityEngine.Random.Range(0, animalGameObjects.Count)];
        GameObject spawnedAnimal = Instantiate(selectedAnimal, new Vector3(spawnX, transform.position.y + UnityEngine.Random.Range(minHeight, maxHeight), 0), Quaternion.identity);

        SpriteRenderer spriteRenderer = spawnedAnimal.GetComponent<SpriteRenderer>();
        if (flipOnSpawnPosition && spawnX == spawnPositionX && spriteRenderer != null)
        {
            // Cambiar el flipX del SpriteRenderer
            spriteRenderer.flipX = true;
        }

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
