using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour, IGameManager
{
    private Dictionary<AsteroidData, int> asteroids = new Dictionary<AsteroidData, int>();

    public ManagerStatus Status { get; private set; }

    public void Startup()
    {
        Debug.Log("AsteroidManager starting...");

        Status = ManagerStatus.Started;

        Debug.Log("AsteroidManager started.".SetColor(Color.Green));
    }

    public void AddAsteroid(AsteroidData data,int count)
    {
        // if contains asteroids.Add()
    }

    public Dictionary<AsteroidData,int> GetAsteroidList()
    {
        Dictionary<AsteroidData, int> asteroids = new Dictionary<AsteroidData, int>(this.asteroids);
        return asteroids;
    }

    public void RemoveAsteroid(AsteroidData data)
    {
        if(asteroids.ContainsKey(data))
        {
            asteroids.Remove(data);
        }
    }
}
