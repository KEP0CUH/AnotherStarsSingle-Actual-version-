using UnityEngine;


[RequireComponent(typeof(PlanetState))]
[RequireComponent(typeof(PlanetView))]



public class PlanetController : MonoBehaviour
{
    private PlanetState state;
    private PlanetView view;
    private LocationController locationController;
    private int offset;


    public PlanetState State => state;

    public void Init(LocationController controller,Planet kind,int offset)
    {
        this.locationController = controller;
        this.state = this.GetComponent<PlanetState>().Init(this,kind);
        this.view = this.GetComponent<PlanetView>().Init(this);
        this.offset = offset;
    }

    public void CloseInfoWindow()
    {
        view.CloseInfoWindow();
    }

    private void Start()
    {
        this.gameObject.AddComponent<SphereCollider>();
        this.gameObject.transform.SetParent(locationController.transform, true);
        this.gameObject.transform.localPosition = new Vector3(Random.Range(2,offset + 2), Random.Range(2, offset + 2), 0);
     
        SetRandomPositionAroundSun();
    }

    private void FixedUpdate()
    {
        SaveRotationAboutSun();
    }

    private void SetRandomPositionAroundSun()
    {
        this.gameObject.transform.RotateAround(locationController.transform.position, locationController.transform.forward, Random.Range(0, 360));
    }

    private void SaveRotationAboutSun()
    {
        transform.RotateAround(locationController.transform.position, locationController.transform.forward, 4.0f * Time.fixedDeltaTime);
    }
}
