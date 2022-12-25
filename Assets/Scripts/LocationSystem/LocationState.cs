///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MobSpawner))]
public class LocationState : MonoBehaviour
{
    [SerializeField]
    private             LocationData                    locationData;
    
    [SerializeField] 
    private             List<AsteroidFieldType>         asteroidFieldsTypes = new List<AsteroidFieldType>();
    [SerializeField] 
    private             MobSpawner                      mobSpawner;



    private             LocationController              controller;
    private             List<Planet>                    planets;

    public              LocationData                    Data => locationData;

    public              LocationState                   Init(LocationController controller,Location location)
    {
        this.controller = controller;
        this.locationData = Managers.Resources.DownloadData(location);
        this.mobSpawner = this.gameObject.GetComponent<MobSpawner>();
        this.planets = locationData.Planets;
        SpawnPlanets();
        SpawnAsteroidFieldIfHave();
        this.mobSpawner.Init(locationData.MobSpawnerType);


        CanvasUI.GlobalMap.AddLocationOnMap(this);

        return this;
    }

    private             void                            SpawnPlanets()
    {
        for (int i = 0; i < planets.Count; ++i)
        {
            int orbitNumber = i + 1;

            var planet = new GameObject($"Planet{planets[i]}");
            planet.AddComponent<PlanetController>().Init(this.transform, planets[i], orbitNumber);
        }
    }

    private             void                            SpawnAsteroidFieldIfHave()
    {
        for(int i = 0; i < asteroidFieldsTypes.Count; i++)
        {
            var type = asteroidFieldsTypes[i];
            if (type == AsteroidFieldType.EmptyField) continue;

            var quarter = new Vector2();
            switch(i)
            {
                case 0: quarter = Vector2.one;
                    break;
                case 1: quarter = Vector2.left + Vector2.up;
                    break;
                case 2: quarter = Vector2.one * (-1);
                    break;
                case 3: quarter = Vector2.right + Vector2.down;
                    break;
            }

            var newAsteroidField = new GameObject($"{type}");
            newAsteroidField.AddComponent<AsteroidFieldController>().Init(controller, type, 12,quarter);
        }
    }
}
