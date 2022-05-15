using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName="Location",fileName ="NewGalaxe",order = 54)]
public class LocationData : ScriptableObject
{
    [SerializeField] private string                     title;
    [SerializeField] private string                     description;
    [SerializeField] private Location                   location;
    [SerializeField] private List<Planet>               planets;
    [SerializeField] private MobSpawnerType             mobSpawnerType;
    [SerializeField] private SunType                    sunType;



    public string                                       Title => title;
    public List<Planet>                                 Planets => planets;
    public MobSpawnerType                               MobSpawnerType => mobSpawnerType;
    public SunType                                      SunType => sunType;

    private void OnValidate()
    {
        planets = new List<Planet>();
        description = "";
        switch (location)
        {
            case Location.Krinul:
                title = "Кринул";
                description = "Центр вселенной Иные Звёзды";
                location = Location.Krinul;

                planets.Add(Planet.Arcea);
                planets.Add(Planet.Earth);
                planets.Add(Planet.Mars);

                mobSpawnerType = MobSpawnerType.pirateSpawner1;

                sunType = SunType.WhiteSun;
                break;
            case Location.Lambda:
                title = "Лямбда";
                location = Location.Lambda;

                planets.Add(Planet.Arcea);
                planets.Add(Planet.Earth);
                planets.Add(Planet.Mars);

                mobSpawnerType = MobSpawnerType.pirateSpawner2;

                sunType = SunType.BlueSun;
                break;

        }
    }

}
