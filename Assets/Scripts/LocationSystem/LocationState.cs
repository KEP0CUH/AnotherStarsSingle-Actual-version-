using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LocationState : MonoBehaviour
{
    [SerializeField] private LocationData data;
    [SerializeField] private List<Planet> planets;
    [SerializeField] private List<AsteroidFieldType> asteroidFieldsTypes = new List<AsteroidFieldType>();
    [SerializeField] private SunType sunType;

    private LocationController controller;


    public LocationData Data => data;

    public void Init(LocationController controller,Location location)
    {
        this.controller = controller;
        this.data = Managers.Resources.DownloadData(location);
        SpawnPlanets();
        SpawnAsteroidFieldIfHave();

        this.GetComponent<SpriteRenderer>().sprite = Managers.Resources.DownloadData(sunType);

        CanvasUI.GlobalMap.AddLocationOnMap(this);
    }

    private void SpawnPlanets()
    {
        for (int i = 0; i < planets.Count; i++)
        {
            var planet = new GameObject($"Planet{planets[i]}");
            planet.AddComponent<PlanetController>().Init(controller, planets[i], i * 3);
        }
    }

    private void SpawnAsteroidFieldIfHave()
    {
        for(int i = 0; i < asteroidFieldsTypes.Count; i++)
        {
            var type = asteroidFieldsTypes[i];
            if (asteroidFieldsTypes[i] == AsteroidFieldType.EmptyField) continue;

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

            var newAsteroidField = new GameObject($"{asteroidFieldsTypes}");
            newAsteroidField.AddComponent<AsteroidFieldController>().Init(controller, type, 3,quarter);
        }
    }
}
