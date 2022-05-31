using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName="ScriptableObjects/Asteroids/NewAsteroid",fileName ="New Asteroid",order = 52)]
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
        icon = Resources.Load<Sprite>("Images/Asteroids/" + "Asteroid");
        switch (type)
        {
            case AsteroidType.GoldAsteroid:
                title = "GoldAsteroid";
                description = "Астероид, содержащий в себе до 5% золота.";
                dropKind = ItemKind.GoldOre;
                break;
            case AsteroidType.FerrumAsteroid:
                title = "FerrumAsteroid";
                description = "Астероид с примесями железа.";
                dropKind = ItemKind.FerrumOre;
                break;
            case AsteroidType.EmptyAsteroid:
                title = "EmptyAsteroid";
                description = "";
                break;
            case AsteroidType.MineralAsteroid:
                title = "MineralAsteroid";
                description = "Asteroid from mineral";
                dropKind = ItemKind.MineralOre;
                break;
            case AsteroidType.OrganicAsteroid:
                title = "OrganicAsteroid";
                description = "Asteroid from organic";
                dropKind = ItemKind.OrganicOre;
                break;
            case AsteroidType.TitanAsteroid:
                title = "TitanAsteroid";
                description = "Asteroid from titan";
                dropKind = ItemKind.TitanOre;
                break;
            case AsteroidType.OsmiumAsteroid:
                title = "OsmiumAsteroid";
                description = "Asteroid from osmium";
                dropKind = ItemKind.OsmiumOre;
                break;
        }
    }
}
