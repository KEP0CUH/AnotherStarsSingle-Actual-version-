using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlanetView : MonoBehaviour
{
    private PlanetController planetController;
    private PlanetState planetState;

    private GameObject infoPlanetWindowPrefab;
    private static GameObject infoPlanetWindowObject;
    private bool isClicked = false;
    public PlanetView Init(PlanetController controller,PlanetState state)
    {
        this.planetController = controller;
        this.planetState = state;

        this.gameObject.name                            = planetState.Data.Title;
        this.GetComponent<SpriteRenderer>().sprite      = planetState.Data.Icon;

        this.infoPlanetWindowPrefab = Managers.Resources.DownloadData(ObjectType.InfoPlanetWindow);

        return this;
    }

    public void BreakInfoPlanetWindow()
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
        CreateInfoPlanetWindow();
    }
    private void OnMouseDown()
    {
        isClicked = true;
    }
    private void OnMouseExit()
    {
        if(isClicked == false)
        {
            BreakInfoPlanetWindow();
        }
    }
    private void CreateInfoPlanetWindow()
    {
        if(infoPlanetWindowObject != null)
        {
            Object.Destroy(infoPlanetWindowObject.gameObject);
            infoPlanetWindowObject = null;
        }

        if (infoPlanetWindowObject == null)
        {
            infoPlanetWindowObject = new GameObject("InfoPlanetWindow", typeof(InfoPlanetWindow));
            infoPlanetWindowObject.GetComponent<InfoPlanetWindow>().Init(planetController);

            // Сверху скрипт сам создает окошко, а снизу спавн через префаб. Через префаб большая гибкость, но через скрипт "каменнее".

            /*            infoPlanetWindowObject = Instantiate(infoPlanetWindowPrefab, planetController.transform);
                        infoPlanetWindowObject.GetComponent<InfoPlanetWindow>().Init(planetController);*/
        }
    }
}
