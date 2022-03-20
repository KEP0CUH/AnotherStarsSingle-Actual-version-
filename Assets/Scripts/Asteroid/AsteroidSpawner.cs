using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private List<AsteroidType> asteroids = new List<AsteroidType>();
    private int currentExistNum = 0;
    [SerializeField] private int maxExistNum = 5;


    private void Awake()
    {
        asteroids.Add(AsteroidType.AsteroidGold);
        asteroids.Add(AsteroidType.AsteroidFerrum);

        currentExistNum = 0;
        maxExistNum = 34;
    }

    public void Update()
    {
        SpawnAsteroid();
    }

    private void SpawnAsteroid()
    {
        if (currentExistNum < maxExistNum)
        {
            var type = GetRandomType();
            GameObject newAsteroid = new GameObject("Asteroid" + type, typeof(AsteroidData));
            newAsteroid.transform.parent = this.transform;
            newAsteroid.AddComponent<AsteroidController>().Init(this.transform);
            var data = newAsteroid.GetComponent<AsteroidData>();
            data.Init(type);
            currentExistNum++;
        }
    }

    public void RemoveAsteroid()
    {
        currentExistNum--;
    }

    private AsteroidType GetRandomType()
    {
        int value = Random.Range(0, asteroids.Count);
        return asteroids[value];
    }

}
