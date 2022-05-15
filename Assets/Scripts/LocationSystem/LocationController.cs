using UnityEngine;



[RequireComponent(typeof(LocationState))]
[RequireComponent(typeof(LocationView))]
public class LocationController : MonoBehaviour
{
    private LocationState state;
    private LocationView view;
    private LocationController locationController;
    [SerializeField] private Location location;

    public LocationState State => state;
    public LocationView View => view;

    private void Start()
    {
        this.locationController = GetComponent<LocationController>();

        this.state = this.gameObject.GetComponent<LocationState>().Init(locationController, location); ;
        this.view = this.gameObject.GetComponent<LocationView>().Init(locationController);
    }
}
