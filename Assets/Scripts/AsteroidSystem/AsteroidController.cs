using UnityEngine;

[RequireComponent(typeof(AsteroidState))]
[RequireComponent(typeof(AsteroidView))]
public class AsteroidController : MonoBehaviour
{
    private GameObject spawner;

    private AsteroidState asteroidState;
    private AsteroidView asteroidView;

    private GameObject infoWindow = null;
    private bool isClicked = false;

    public AsteroidState State => asteroidState;
    public AsteroidView View => asteroidView;

    public AsteroidController Init(Transform spawner, AsteroidType type, Vector2 quarter)
    {
        this.spawner = spawner.gameObject;
        this.asteroidState = this.gameObject.GetComponent<AsteroidState>().Init(type);
        this.asteroidView = this.gameObject.GetComponent<AsteroidView>().Init(asteroidState, spawner, quarter);

        return this;
    }

    public void Init(Transform spawner,AsteroidState asteroidState,Vector2 quarter)
    {
        this.spawner = spawner.gameObject;
        this.asteroidState = asteroidState;

        this.asteroidView = GetComponent<AsteroidView>().Init(asteroidState,spawner,quarter);
    }

    public void CloseInfoWindow()
    {
        if(infoWindow != null)
        {
            Destroy(infoWindow.gameObject);
            infoWindow = null;
            isClicked = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<AmmoController>())
        {
            var bullet = other.GetComponent<AmmoController>();
            this.gameObject.GetComponent<AsteroidState>().ChangeHealth(-bullet.State.Data.BaseDamage);
            Destroy(other.gameObject);
        }
    }



    private void OnMouseEnter()
    {
        var data = this.gameObject.GetComponent<AsteroidState>();
        Debug.Log($"Это объект: {data.Data.Title} {data.Data.Description} {data.Health}");

        if(infoWindow == null)
        {
            infoWindow = new GameObject("InfoWindow");
            infoWindow.AddComponent<InfoWindow>().Init(this.gameObject.GetComponent<AsteroidController>());
        }
    }

    private void OnMouseDown()
    {
        isClicked = true;
    }

    private void OnMouseExit()
    {
        if(infoWindow != null && !isClicked)
        {
            Destroy(infoWindow.gameObject);
            infoWindow = null;
        }
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            spawner.gameObject.GetComponent<AsteroidFieldView>().DestroyAsteroid(this.gameObject.GetComponent<AsteroidState>().Id);

            if (infoWindow != null && !isClicked)
            {
                Destroy(infoWindow.gameObject);
                infoWindow = null;
            }
        }
    }
}

