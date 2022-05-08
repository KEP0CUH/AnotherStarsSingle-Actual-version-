using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AsteroidFieldState))]
public class AsteroidFieldView : MonoBehaviour
{
    [SerializeField] private AsteroidFieldState state;

    private List<GameObject> asteroids;
    private Dictionary<int, AsteroidState> asteroidsDic;
    private int maxNumAsteroids;
    private int currentNumAsteroids;

    [SerializeField] private Vector2 quarter;

    private GameObject infoWindow;
    private bool isClicked;


    public Vector2 Quarter => quarter;
    public void Init(Transform locationCoord, AsteroidFieldType type,Vector2 quarter)
    {
        this.asteroids = new List<GameObject>();
        this.asteroidsDic = new Dictionary<int, AsteroidState>();
        this.maxNumAsteroids = 50;
        this.currentNumAsteroids = 0;

        this.quarter = quarter;

        this.state = gameObject.GetComponent<AsteroidFieldState>();
        this.state.Init(type);

        this.gameObject.GetComponent<SpriteRenderer>().sprite = Managers.Resources.DownloadData(IconType.AsteroidField);

        CreateAsteroid();
    }
    public void DestroyAsteroid(int id)
    {
        if (this.asteroidsDic.ContainsKey(id))
        {
            Object.Destroy(asteroidsDic[id].gameObject);
        }
    }

    private void CreateAsteroid()
    {
        var data = this.state.Data.AsteroidData;

        var newAsteroid = new GameObject($"Asteroid {data.DropKind}", typeof(AsteroidState));
        this.asteroids.Add(newAsteroid);
        newAsteroid.transform.SetParent(this.gameObject.transform, false);
        var asteroidState = newAsteroid.GetComponent<AsteroidState>();
        asteroidState.Init(data);
        newAsteroid.AddComponent<AsteroidController>().Init(this.transform, asteroidState,quarter);

        this.asteroidsDic.Add(asteroidState.Id, asteroidState);
    }

    private void FixedUpdate()
    {
        if (currentNumAsteroids < maxNumAsteroids)
        {
            currentNumAsteroids++;
            CreateAsteroid();
        }
    }

    private void OnMouseEnter()
    {
        if (infoWindow == null)
        {
            infoWindow = new GameObject("InfoWindow");
            infoWindow.AddComponent<InfoPlanetWindow>().Init(this.gameObject.GetComponent<AsteroidFieldView>(),state.Data);
        }
    }

    private void OnMouseDown()
    {
        isClicked = true;
    }

    private void OnMouseExit()
    {
        if (infoWindow != null && !isClicked)
        {
            Destroy(infoWindow.gameObject);
            infoWindow = null;
        }
    }

    public void CloseInfoWindow()
    {
        if (infoWindow != null)
        {
            Destroy(infoWindow.gameObject);
            infoWindow = null;
            isClicked = false;
        }
    }


}
