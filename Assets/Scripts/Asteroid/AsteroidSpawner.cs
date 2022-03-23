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
        asteroids.Add(AsteroidType.GoldAsteroid);
        asteroids.Add(AsteroidType.FerrumAsteroid);
        asteroids.Add(AsteroidType.NickelAsteroid);

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
            var data = GetRandomAsteroidData();

            GameObject newAsteroid = new GameObject(data.Title);
            newAsteroid.transform.parent = this.transform;

            var state = newAsteroid.AddComponent<BaseAsteroidState>();
            state.Init(data);
            newAsteroid.AddComponent<AsteroidController>().Init(this.transform, state);
            currentExistNum++;
        }
    }

    public void RemoveAsteroid()
    {
        currentExistNum--;
    }

    private BaseAsteroidData GetRandomAsteroidData()
    {
        var type = asteroids[Random.Range(0, asteroids.Count)];
        var data = Managers.Resources.DownloadData(type);
        return data;
    }

}
