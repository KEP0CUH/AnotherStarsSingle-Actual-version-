using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(TargetLight))]
public class PlanetView : MonoBehaviour
{
    private PlanetController controller;

    private static GameObject   infoWindow;
    private static TargetLight targetLight = null;

    public PlanetView Init(PlanetController controller)
    {
        this.controller = controller;
        this.gameObject.name                            = this.controller.State.Data.Title;
        this.GetComponent<SpriteRenderer>().sprite      = this.controller.State.Data.Icon;
        this.gameObject.AddComponent<LandPlanetOnDoubleClick>().Init(controller);

        return this;
    }

    public void CloseInfoWindow()
    {
        if (infoWindow != null)
        {
            Object.Destroy(infoWindow.gameObject);
            infoWindow = null;
        }

        if (targetLight != null)
        {
            Destroy(targetLight.gameObject);
            targetLight = null;
        }
    }

    private void OnMouseDown()
    {
        OpenInfoWindow();
        CreateTargetLight();
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

    private void CreateTargetLight()
    {
        if (targetLight != null)
        {
            Destroy(targetLight.gameObject);
            targetLight = null;
        }

        var radius = this.GetComponent<SphereCollider>().radius;
        targetLight = new GameObject("TargetLight").AddComponent<TargetLight>().Init(this.transform,4.5f * radius);
    }
}
