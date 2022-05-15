using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName="Location",fileName ="NewGalaxe",order = 54)]
public class LocationData : ScriptableObject
{
    [SerializeField] private string title;
    [SerializeField] private string description;
    [SerializeField] private Location location;
    [SerializeField] private List<Planet> planets;
    [SerializeField] private MobSpawnerKind mobSpawnerKind;
    [SerializeField] private SunType sunType;



    public string Title => title;
    public List<Planet> Planets => planets;
    public MobSpawnerKind MobSpawnerKind => mobSpawnerKind;
    public SunType SunType => sunType;

    private void OnValidate()
    {
        planets = new List<Planet>();
        switch (location)
        {
            case Location.Krinul:
                title = "Кринул";
                description = "Центр вселенной Иные Звёзды";
                location = Location.Krinul;

                planets.Add(Planet.Arcea);
                planets.Add(Planet.Earth);
                planets.Add(Planet.Mars);

                mobSpawnerKind = MobSpawnerKind.pirateSpawner1;

                sunType = SunType.WhiteSun;
                break;
            case Location.Lambda:
                title = "Лямбда";
                description = "";
                location = Location.Lambda;

                planets.Add(Planet.Arcea);
                planets.Add(Planet.Earth);
                planets.Add(Planet.Mars);

                mobSpawnerKind = MobSpawnerKind.pirateSpawner2;

                sunType = SunType.BlueSun;
                break;

        }
    }

}
