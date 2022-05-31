using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(RectTransform))]
public class InfoPlanetWindow : MonoBehaviour
{
    private PlanetState state;
    private PlanetController controller;

    [SerializeField] private GameObject icon;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject buttonClose;
    [SerializeField] private GameObject buttonLand;
    [SerializeField] private GameObject description;

    public void Init(PlanetController controller)
    {
        this.controller = controller;
        state = controller.State;

        icon.GetComponent<Image>().sprite       = state.Data.Icon;
        title.GetComponent<Text>().text         = state.Data.Title;

        buttonClose.GetComponent<Button>().onClick.AddListener(CloseInfoWindow);
        InitButtonLand();
    }

    private void InitButtonLand()
    {
        var image = buttonLand.GetComponent<Image>();
        image.sprite = Managers.Resources.DownloadData(IconType.Land);

        var button = buttonLand.GetComponent<Button>();
        button.onClick.AddListener(Land);
    }

    private void Land()
    {
        if (controller.GetComponent<PlanetController>())
        {
            Managers.Player.Land(controller.transform);

            var planet = new GameObject("PlanetInside", typeof(RectTransform));
            planet.AddComponent<PlanetWindow>().Init(state);

            //controller.GetComponent<PlanetController>().CreateItemShop();

            CloseInfoWindow();
        }
    }

    private void CloseInfoWindow()
    {
        controller.CloseInfoWindow();
    }
}
