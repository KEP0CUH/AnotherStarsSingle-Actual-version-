using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(RectTransform))]
public class InfoPlanetWindow : MonoBehaviour
{
    private PlanetState planetState;
    private GameObject controller;

    [SerializeField] private GameObject icon;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject buttonClose;
    [SerializeField] private GameObject buttonLand;
    private GameObject description;



    public void Init(PlanetController controller)
    {
        this.controller = controller.gameObject;
        planetState = controller.State;
        SettingIcon(planetState.Data.Icon);
        SettingTitle(planetState.Data.Title);
        //CreateDescription();
        SettingButtonClose();
        SettingButtonLand();
    }

    public void Init(AsteroidFieldView controller, AsteroidFieldData data)
    {
        this.controller = controller.gameObject;
        CreateWindow();
        SettingIcon(data.Icon);
        SettingTitle(data.Title);
        SettingButtonClose();
        SettingButtonLand();
    }


    private void CreateWindow()
    {
        Managers.Canvas.AddModule(this.gameObject);

        var image = gameObject.AddComponent<Image>();
        image.color = new UnityEngine.Color(195f, 195f, 195f, 138) / 256f;

        var rect = gameObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(1, 0);
        rect.anchorMax = new Vector2(1, 0);
        rect.pivot = new Vector2(1, 1);

        rect.offsetMin = new Vector2(-200, 10);
        rect.offsetMax = new Vector2(-10, 220);

    }

    private void SettingIcon(Sprite content)
    {
        icon.GetComponent<Image>().sprite = content;
    }

    private void SettingTitle(string content)
    {
        var text = title.GetComponent<Text>().text = content;
    }

    private void SettingButtonClose()
    {
        var buttonComponent = buttonClose.GetComponent<Button>();
        buttonComponent.onClick.AddListener(Close);
    }

    private void SettingButtonLand()
    {
        var image = buttonLand.GetComponent<Image>();
        image.sprite = Managers.Resources.DownloadData(IconType.Land);

        var button = buttonLand.GetComponent<Button>();
        button.onClick.AddListener(OnLand);
    }

    private void OnLand()
    {
        if (controller.GetComponent<AsteroidFieldView>())
        {
            Managers.Player.LandOnAsteroidField(controller.transform,controller.GetComponent<AsteroidFieldView>().Quarter);

            var asteroidField = new GameObject("AsteroidFieldInside", typeof(RectTransform));
            asteroidField.GetComponent<RectTransform>().SetAsFirstSibling();
            asteroidField.AddComponent<AsteroidFieldWindow>().Init();

            Close();
        }
        else if (controller.GetComponent<PlanetController>())
        {
            Managers.Player.Land(controller.transform);

            var planet = new GameObject("PlanetInside", typeof(RectTransform));
            planet.AddComponent<PlanetWindow>().Init(planetState);
            planet.GetComponent<RectTransform>().SetAsFirstSibling();

            //controller.GetComponent<PlanetController>().CreateItemShop();

            Close();
        }

    }

    private void Close()
    {
        if (controller.GetComponent<AsteroidFieldView>())
        {
            controller.GetComponent<AsteroidFieldView>().CloseInfoWindow();
        }
        else if (controller.GetComponent<PlanetController>())
        {
            controller.GetComponent<PlanetController>().RemoveInfoWindow();
        }

    }
}
