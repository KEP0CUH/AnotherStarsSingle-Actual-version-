using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlanetView : MonoBehaviour
{
    private PlanetController controller;

    private static GameObject   infoWindow;
    private static bool         isClicked = false;
    
    public PlanetView Init(PlanetController controller)
    {
        this.controller = controller;
        this.gameObject.name                            = this.controller.State.Data.Title;
        this.GetComponent<SpriteRenderer>().sprite      = this.controller.State.Data.Icon;


        return this;
    }

    public void CloseInfoWindow()
    {
        if (infoWindow != null)
        {
            Object.Destroy(infoWindow.gameObject);
            infoWindow = null;
            isClicked = false;
        }
    }

    private void OnMouseDown()
    {
        isClicked = true;
        OpenInfoWindow();
    }

    private void OpenInfoWindow()
    {
        if(infoWindow != null)
        {
            Object.Destroy(infoWindow.gameObject);
            infoWindow = null;
        }

        if (infoWindow == null)
        {
            var infoWindowPrefab = Managers.Resources.DownloadData(ObjectType.PlanetWindow);

            infoWindow = Instantiate(infoWindowPrefab);
            Managers.Canvas.AddModule(infoWindow);
            infoWindow.GetComponent<PlanetWindow>().Init(controller);
        }
    }
}
