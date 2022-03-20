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
            GameObject newAsteroid = new GameObject("Asteroid" + type);
            newAsteroid.transform.parent = this.transform;

            var data = Resources.Load<BaseAsteroidData>("ScriptableObjects/Asteroids/" + type.ToString());
            data.Init(type);



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

    private AsteroidType GetRandomType()
    {
        int value = Random.Range(0, asteroids.Count);
        return asteroids[value];
    }

}
