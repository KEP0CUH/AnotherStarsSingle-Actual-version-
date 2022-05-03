using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName="Asteroids",fileName ="New Asteroid",order = 52)]
public class AsteroidData : ScriptableObject
{
    [SerializeField] private string title;
    [SerializeField] private string description;
    [SerializeField] private AsteroidType type;
    [SerializeField] private Sprite icon;
    [SerializeField] private ItemKind dropKind;

    public string Title => title;
    public string Description => description;
    public AsteroidType Type => type;
    public Sprite Icon => icon;
    public ItemKind DropKind => dropKind;

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
                dropKind = ItemKind.rudaGold;
                break;
            case AsteroidType.FerrumAsteroid:
                title = "FerrumAsteroid";
                description = "Астероид с примесями железа.";
                icon = Resources.Load<Sprite>("Images/Asteroids/" + AsteroidType.FerrumAsteroid.ToString());
                dropKind = ItemKind.rudaFerrum;
                break;
            case AsteroidType.NickelAsteroid:
                title = "NickelAsteroid";
                description = "Asteroid from nickel.";
                icon = Resources.Load<Sprite>("Images/Asteroids/" + AsteroidType.FerrumAsteroid.ToString());
                dropKind = ItemKind.rudaNickel;
                break;
            case AsteroidType.EmptyAsteroid:
                title = "EmptyAsteroid";
                description = "";
                break;
        }
    }
}
