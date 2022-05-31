using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AsteroidFieldState))]
public class AsteroidFieldView : MonoBehaviour
{
    private AsteroidFieldState state;
    private AsteroidFieldController controller;

    private List<GameObject> asteroids;
    private Dictionary<int, AsteroidState> asteroidsDic;
    private int maxNumAsteroids;
    private int currentNumAsteroids;

    [SerializeField] private Vector2 quarter;

    private static GameObject infoWindow = null;
    private static bool isClicked = false;


    public Vector2 Quarter => quarter;
    public AsteroidFieldView Init(AsteroidFieldController controller,Transform locationCoord, AsteroidFieldType type,Vector2 quarter)
    {
        this.controller = controller;
        this.asteroids = new List<GameObject>();
        this.asteroidsDic = new Dictionary<int, AsteroidState>();
        this.maxNumAsteroids = 50;
        this.currentNumAsteroids = 0;

        this.quarter = quarter;

        this.state = gameObject.GetComponent<AsteroidFieldState>();
        this.state.Init(type);

        this.gameObject.GetComponent<SpriteRenderer>().sprite = Managers.Resources.DownloadData(IconType.AsteroidField);

        CreateAsteroid();
        return this;
    }
    public void DestroyAsteroid(int id)
    {
        if (this.asteroidsDic.ContainsKey(id))
        {
            Object.Destroy(asteroidsDic[id].gameObject);
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
           CreateInfoWindow();
        }
    }

    private void OnMouseDown()
    {
        if (isClicked == true)
        {
            CreateInfoWindow();
        }
        else
        {
            isClicked = true;
        }
    }

    private void OnMouseExit()
    {
        if (isClicked == false)
        {
            CloseInfoWindow();
        }
    }



    private void CreateInfoWindow()
    {
        if(infoWindow != null)
        {
            Object.Destroy(infoWindow.gameObject);
            infoWindow = null;
        }

        if(infoWindow == null)
        {
            infoWindow = Instantiate(Managers.Resources.DownloadData(ObjectType.InfoAsteroidFieldWindow));
            Managers.Canvas.AddModule(infoWindow);
            infoWindow.GetComponent<InfoAsteroidFieldWindow>().Init(this.controller);
        }

    }


}
