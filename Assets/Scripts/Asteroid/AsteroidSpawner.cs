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
        maxExistNum = 5;
    }

    public void Update()
    {
        SpawnAsteroid(AsteroidType.AsteroidGold);
    }

    private void SpawnAsteroid(AsteroidType type)
    {
        if (currentExistNum < maxExistNum)
        {
            GameObject newAsteroid = new GameObject("Asteroid" + type, typeof(AsteroidController), typeof(AsteroidData));
            var data = newAsteroid.GetComponent<AsteroidData>();
            data.Init(type);
            currentExistNum++;
        }
    }
}
