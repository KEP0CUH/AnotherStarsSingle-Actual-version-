using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName="Asteroids",fileName ="New Asteroid",order = 52)]
public class BaseAsteroidData : ScriptableObject
{
    [SerializeField] private string title;
    [SerializeField] private string description;
    [SerializeField] private AsteroidType type;
    [SerializeField] private Sprite icon;
    [SerializeField] private string dropName;

    public string Title => title;
    public string Description => description;
    public AsteroidType Type => type;
    public Sprite Icon => icon;
    public string DropName => dropName;

    public void Init(AsteroidType asteroidType)
    {
        type = asteroidType;
        OnValidate();
    }

    private void OnValidate()
    {
        switch (type)
        {
            case AsteroidType.GoldAsteroid:
                title = "GoldAsteroid";
                description = "Астероид, содержащий в себе до 5% золота.";
                icon = Resources.Load<Sprite>("Images/Asteroids/" + AsteroidType.GoldAsteroid.ToString());
                dropName = "Gold";
                break;
            case AsteroidType.FerrumAsteroid:
                title = "FerrumAsteroid";
                description = "Астероид с примесями железа.";
                icon = Resources.Load<Sprite>("Images/Asteroids/" + AsteroidType.FerrumAsteroid.ToString());
                dropName = "Ferrum";
                break;

        }
    }
}
