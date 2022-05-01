using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LocationState : MonoBehaviour
{
    [SerializeField] private LocationData data;
    [SerializeField] private List<Planet> planets;
    [SerializeField] private AsteroidFieldType type;


    public LocationData Data => data;

    public void Init(LocationController controller,Location location)
    {
        this.data = Managers.Resources.DownloadData(location);

        for(int i = 0; i < planets.Count; i++)
        {
            var planet = new GameObject("Planet");
            planet.AddComponent<PlanetController>().Init(controller, i * 3);
        }

        var asteroidField = new GameObject("AsteroidField");
        asteroidField.AddComponent<AsteroidFieldController>().Init(controller,type, 3);

        CanvasUI.GlobalMap.AddLocationOnMap(this);
    }
}
