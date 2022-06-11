using UnityEngine;

[RequireComponent(typeof(AsteroidState))]
[RequireComponent(typeof(AsteroidView))]
public class AsteroidController : MonoBehaviour
{
    private GameObject spawner;

    private AsteroidState asteroidState;
    private AsteroidView asteroidView;

    private static GameObject infoWindow = null;
    private static bool isClicked = false;

    public event System.Action OnDamagedAsteroid;

    public AsteroidState State => asteroidState;
    public AsteroidView View => asteroidView;

    public AsteroidController Init(Transform spawner, AsteroidType type, Vector2 quarter)
    {
        this.spawner = spawner.gameObject;
        this.asteroidState = this.gameObject.GetComponent<AsteroidState>().Init(type);
        this.asteroidView = this.gameObject.GetComponent<AsteroidView>().Init(asteroidState, spawner, quarter);

        return this;
    }

    public void CloseInfoWindow()
    {
        if(infoWindow != null)
        {
            Object.Destroy(infoWindow.gameObject);
            infoWindow = null;
            isClicked = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<AmmoController>())
        {
            var bullet = other.GetComponent<AmmoController>();
            this.asteroidState.ChangeHealth(-bullet.State.Data.BaseDamage);
            OnDamagedAsteroid?.Invoke();
            Destroy(other.gameObject);
        }
    }

    private void OnMouseDown()
    {
        isClicked = true;
        if (infoWindow == null)
        {
            infoWindow = Instantiate(Managers.Resources.DownloadData(ObjectType.AsteroidWindow));
            Managers.Canvas.AddModule(infoWindow);
            infoWindow.GetComponent<AsteroidWindow>().Init(this);
        }
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

            if (infoWindow != null)
            {
                Destroy(infoWindow.gameObject);
                infoWindow = null;
            }
        }
    }
}

