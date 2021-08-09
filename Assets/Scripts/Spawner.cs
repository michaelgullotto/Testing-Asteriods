using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    public float spawnrate = 1.0f;
    public int spawnAmount = 1;
    public float trajectoryVariance = 15.0f;
    public Asteroid asteroidPrefab;
    public float spawnDistance = 15.0f;
    public GameObject testasteriod;

   
    void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnrate, this.spawnrate);
    }

    public void Spawn()
    {
        for (int i = 0; i < this.spawnAmount; i++)
        {
            testAsteriod();
        }
        
    }

    public GameObject testAsteriod()
    {
        Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
        Vector3 spawnpoint = this.transform.position + spawnDirection;
        float variance = Random.Range(trajectoryVariance, -trajectoryVariance);
        Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
        Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnpoint, rotation);
        asteroid.size = Random.Range(asteroid.maxSize, asteroid.minSize);
        asteroid.SetTrajectort(rotation * -spawnDirection);


        testasteriod = asteroid.SpawnedAsteriod;
        
        return testasteriod;
    }
    
}
