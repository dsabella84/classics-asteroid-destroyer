using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnableObjects;
    [SerializeField] int activeObjectCount;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {        
        mainCamera = Camera.main;

        SpawnObjects(4);
    }

    public void  SpawnObjects(int objectsToSpawn)
    {

        for (int i = 0; i < objectsToSpawn; i++)
        {
            float randomX = UnityEngine.Random.Range(0.0f, 1f);
            float randomY = UnityEngine.Random.Range(0.0f, 1f);

            Vector3 instancePosition = mainCamera.ViewportToWorldPoint(new Vector3(randomX, randomY, 0));

            GameObject asteroid = Instantiate<GameObject>(spawnableObjects[0], new Vector3(instancePosition.x, instancePosition.y, 0), Quaternion.identity, this.transform);

            activeObjectCount++;
        }

    }

    public int GetAsteroidCount()
    {
        return activeObjectCount;
    }

    public void AddAsteroidCount()
    {
        activeObjectCount++;
    }

    public void DecreaseAsteroidCount()
    {
        activeObjectCount--;
    }
}

