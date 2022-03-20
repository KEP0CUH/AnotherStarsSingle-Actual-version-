using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour, IGameManager
{
    private Dictionary<BaseAsteroidData, int> asteroids = new Dictionary<BaseAsteroidData, int>();

    public ManagerStatus Status { get; private set; }

    public void Startup()
    {
        Debug.Log("AsteroidManager starting...");

        Status = ManagerStatus.Started;

        Debug.Log("AsteroidManager started.".SetColor(Color.Green));
    }

    public void AddAsteroid(BaseAsteroidData data,int count)
    {
        // if contains asteroids.Add()
    }

    public Dictionary<BaseAsteroidData,int> GetAsteroidList()
    {
        Dictionary<BaseAsteroidData, int> asteroids = new Dictionary<BaseAsteroidData, int>(this.asteroids);
        return asteroids;
    }

    public void RemoveAsteroid(BaseAsteroidData data)
    {
        if(asteroids.ContainsKey(data))
        {
            asteroids.Remove(data);
        }
    }
}
