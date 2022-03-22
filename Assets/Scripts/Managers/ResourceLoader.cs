using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader : MonoBehaviour, IGameManager
{
    private Dictionary<ItemKind,BaseItemData> items;
    private Dictionary<AsteroidType, BaseAsteroidData> asteroids;


    public Dictionary<ItemKind, BaseItemData> Items => items;
    public Dictionary<AsteroidType, BaseAsteroidData> Asteroids => asteroids;
    public ManagerStatus Status { get; private set; }


    public void Startup()
    {
        Debug.Log("ResourceLoader starting...".SetColor(Color.Yellow));
        items = new Dictionary<ItemKind, BaseItemData>();
        asteroids = new Dictionary<AsteroidType,BaseAsteroidData>();

        LoadAllResources();

        Status = ManagerStatus.Started;
        Debug.Log("ResourceLoader started.".SetColor(Color.Green));
    }

    public BaseItemData DownloadData(ItemKind kind)
    {
        if(items.ContainsKey(kind))
        {
            return items[kind];
        }
        Debug.Log("Critical warning!!! No all resources were founded.".SetColor(Color.Red));
        return null;
    }

    public BaseAsteroidData DownloadData(AsteroidType type)
    {
        if(asteroids.ContainsKey(type))
        {
            return asteroids[type];
        }
        Debug.Log("Critical warning!!! No all resources were founded.".SetColor(Color.Red));
        return null;
    }

    private void LoadAllResources()
    {
        items.Add(ItemKind.rudaFerrum, Resources.Load<BaseItemData>($"ScriptableObjects/Ferrum"));
        items.Add(ItemKind.rudaGold, Resources.Load<BaseItemData>($"ScriptableObjects/Gold"));
        items.Add(ItemKind.rudaNickel, Resources.Load<BaseItemData>($""));
        items.Add(ItemKind.rudaTitan, Resources.Load<BaseItemData>($""));

        asteroids.Add(AsteroidType.GoldAsteroid, Resources.Load<BaseAsteroidData>("ScriptableObjects/Asteroids/GoldAsteroid"));
        asteroids.Add(AsteroidType.FerrumAsteroid, Resources.Load<BaseAsteroidData>("ScriptableObjects/Asteroids/FerrumAsteroid"));
        asteroids.Add(AsteroidType.NickelAsteroid, Resources.Load<BaseAsteroidData>($""));
        asteroids.Add(AsteroidType.TitanAsteroid, Resources.Load<BaseAsteroidData>($""));
    }
}
