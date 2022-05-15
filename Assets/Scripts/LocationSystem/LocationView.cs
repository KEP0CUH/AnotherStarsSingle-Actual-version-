using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LocationView : MonoBehaviour
{
    private LocationController locationController;

    public LocationView Init(LocationController controller)
    {
        this.locationController = controller;


        this.gameObject.name = this.locationController.State.Data.Title;
        this.GetComponent<SpriteRenderer>().sprite = Managers.Resources.DownloadData(locationController.State.Data.SunType);

        return this;
    }
}
