using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(RectTransform))]
public class InfoPlanetWindow : MonoBehaviour
{
    private PlanetState planetState;
    private GameObject controller;

    private GameObject icon;
    private GameObject title;
    private GameObject description;



    public void Init(PlanetController controller)
    {
        this.controller = controller.gameObject;
        planetState = controller.State;
        CreateWindow();
        CreateIcon(planetState.Data.Icon);
        CreateTitle(planetState.Data.Title);
        //CreateDescription();
        CreateButtonClose();
        CreateButtonLand();
    }

    public void Init(AsteroidFieldView controller, AsteroidFieldData data)
    {
        this.controller = controller.gameObject;
        CreateWindow();
        CreateIcon(data.Icon);
        CreateTitle(data.Title);
        CreateButtonClose();
        CreateButtonLand();
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

    private void CreateIcon(Sprite content)
    {
        icon = new GameObject("Icon", typeof(RectTransform), typeof(Image));

        var rect = icon.GetComponent<RectTransform>();
        rect.SetParent(transform, false);

        icon.GetComponent<Image>().sprite = content;
        rect.anchorMin = new Vector2(0.5f, 1);
        rect.anchorMax = new Vector2(0.5f, 1);
        rect.pivot = new Vector2(1, 1);

        rect.offsetMin = new Vector2(-32, -74);
        rect.offsetMax = new Vector2(32, -10);
        Debug.Log("InfoIcon created.");
    }

    private void CreateTitle(string content)
    {
        title = new GameObject("Title", typeof(RectTransform), typeof(Text));

        var rect = title.GetComponent<RectTransform>();
        rect.SetParent(icon.transform, false);

        rect.anchorMin = new Vector2(0.5f, 0);
        rect.anchorMax = new Vector2(0.5f, 0);
        rect.pivot = new Vector2(0.5f, 1);

        rect.offsetMin = new Vector2(-80, -30);
        rect.offsetMax = new Vector2(80, 0);

        var text = title.GetComponent<Text>();
        Font font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        text.font = font;
        text.fontSize = 18;

        text.text = content;
    }

    private void CreateDescription()
    {
        description = new GameObject("Title", typeof(RectTransform), typeof(Text));

        var rect = description.GetComponent<RectTransform>();
        rect.SetParent(title.transform, false);

        rect.anchorMin = new Vector2(0.5f, 0);
        rect.anchorMax = new Vector2(0.5f, 0);
        rect.pivot = new Vector2(0.5f, 1);

        rect.offsetMin = new Vector2(-80, -120);
        rect.offsetMax = new Vector2(80, 0);

        var text = description.GetComponent<Text>();
        Font font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        text.font = font;
        text.fontSize = 16;

        //text.text = data.Data.
    }

    private void CreateButtonClose()
    {
        var buttonClose = new GameObject("CloseWindow", typeof(Image), typeof(Button));
        var rect = buttonClose.GetComponent<RectTransform>();
        rect.SetParent(this.gameObject.transform, false);
        rect.anchorMin = new Vector2(1, 1);
        rect.anchorMax = new Vector2(1, 1);
        rect.offsetMin = new Vector2(-24, -24);
        rect.offsetMax = new Vector2(-8, -8);
        buttonClose.GetComponent<Image>().color = new UnityEngine.Color(255, 0, 0, 140) / 256f;
        var buttonDrop = buttonClose.GetComponent<Button>();
        buttonDrop.onClick.AddListener(Close);
    }

    private void CreateButtonLand()
    {
        var buttonLand = new GameObject("Land", typeof(Image), typeof(Button));
        var rect = buttonLand.GetComponent<RectTransform>();
        rect.SetParent(this.gameObject.transform, false);
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(0, 0);
        rect.pivot = new Vector2(1, 0);
        rect.offsetMin = new Vector2(-45, 0);
        rect.offsetMax = new Vector2(-5, 45);

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
