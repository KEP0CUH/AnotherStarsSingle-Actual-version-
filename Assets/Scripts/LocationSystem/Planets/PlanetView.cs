using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlanetView : MonoBehaviour
{
    private PlanetController controller;

    private GameObject infoPlanetWindowPrefab;
    private static GameObject infoPlanetWindowObject;
    private bool isClicked = false;
    public PlanetView Init(PlanetController controller)
    {
        this.controller = controller;

        this.gameObject.name                            = this.controller.State.Data.Title;
        this.GetComponent<SpriteRenderer>().sprite      = this.controller.State.Data.Icon;

        this.infoPlanetWindowPrefab = Managers.Resources.DownloadData(ObjectType.InfoPlanetWindow);

        return this;
    }

    public void CloseInfoPlanetWindow()
    {
        if (infoPlanetWindowObject != null)
        {
            Object.Destroy(infoPlanetWindowObject.gameObject);
            infoPlanetWindowObject = null;
            isClicked = false;
        }
    }

    private void OnMouseEnter()
    {
        OpenInfoPlanetWindow();
    }
    private void OnMouseDown()
    {
        isClicked = true;
    }
    private void OnMouseExit()
    {
        if(isClicked == false)
        {
            CloseInfoPlanetWindow();
        }
    }
    private void OpenInfoPlanetWindow()
    {
        if(infoPlanetWindowObject != null)
        {
            Object.Destroy(infoPlanetWindowObject.gameObject);
            infoPlanetWindowObject = null;
        }

        if (infoPlanetWindowObject == null)
        {
            var infoPlanetPrefab = Managers.Resources.DownloadData(ObjectType.InfoPlanetWindow);

            infoPlanetWindowObject = Instantiate(infoPlanetPrefab);
            Managers.Canvas.AddModule(infoPlanetWindowObject);
            infoPlanetWindowObject.GetComponent<InfoPlanetWindow>().Init(controller);

        }
    }
}
