using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LocationState : MonoBehaviour
{
    [SerializeField] private LocationData data;
    [SerializeField] private List<Planet> planets;
    [SerializeField] private AsteroidFieldType asteroidFieldType;
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
            planet.AddComponent<PlanetController>().    Init(controller, planets[i], i * 3);
        }
    }

    private void SpawnAsteroidFieldIfHave()
    {
        if (asteroidFieldType == AsteroidFieldType.EmptyField)
        {

        }
        else
        {
            var asteroidField = new GameObject($"{asteroidFieldType}");
            asteroidField.AddComponent<AsteroidFieldController>().Init(controller, asteroidFieldType, 3);
        }
    }
}
