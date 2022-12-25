///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PlanetInside : MonoBehaviour
{
    private             PlanetController            planetController;
    private             GameObject                  shipShop            = null;
    public              PlanetController            PlanetController => planetController;

    public              void                        Init(PlanetController   state)
    {
        this.planetController = state;

        Managers.Canvas.AddModule(this.gameObject);

        var rect = this.gameObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(1, 1);
        rect.pivot = new Vector2(0, 0);
        rect.offsetMin = new Vector2(0, 0);
        rect.offsetMax = new Vector2(0, 0);

        CreateRiseButton();
        CreateShipShop();
        CreateItemShop();
    }
    private             void                        Start()
    {
        this.GetComponent<Image>().sprite = planetController.State.Data.IconBG;
    }

    private             void                        CreateRiseButton()
    {
            var buttonLand = new GameObject("Land", typeof(Image), typeof(Button));
            var rect = buttonLand.GetComponent<RectTransform>();
            rect.SetParent(this.gameObject.transform, false);
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(0, 0);
            rect.pivot = new Vector2(1, 0);
            rect.offsetMin = new Vector2(5, 5);
            rect.offsetMax = new Vector2(45, 45);

            var image = buttonLand.GetComponent<Image>();
            image.sprite = Managers.Resources.DownloadData(IconType.Rise);

            var button = buttonLand.GetComponent<Button>();
            button.onClick.AddListener(OnRise);
    }

    private             void                        CreateShipShop()
    {
        var buttonShipShop = new GameObject("openShipShop", typeof(Image), typeof(Button));
        var rect = buttonShipShop.GetComponent<RectTransform>();
        rect.SetParent(this.gameObject.transform, false);
        rect.anchorMin = new Vector2(1, 0);
        rect.anchorMax = new Vector2(1, 0);
        rect.pivot = new Vector2(1, 1);
        rect.offsetMin = new Vector2(-55, 5);
        rect.offsetMax = new Vector2(-5, 55);

        var image = buttonShipShop.GetComponent<Image>();
        image.sprite = Managers.Resources.DownloadData(IconType.ShipShop);

        var button = buttonShipShop.GetComponent<Button>();
        button.onClick.AddListener(OnOpenShipShop);
    }

    private             void                        CreateItemShop()
    {
        if(this.planetController.State.Data.ItemShopType != ItemShopType.ShopEmpty)
        {
            var buttonItemShop = new GameObject("openItemShop", typeof(Image), typeof(Button));
            var rect = buttonItemShop.GetComponent<RectTransform>();
            rect.SetParent(this.gameObject.transform, false);
            rect.anchorMin = new Vector2(1, 0);
            rect.anchorMax = new Vector2(1, 0);
            rect.pivot = new Vector2(1, 1);
            rect.offsetMin = new Vector2(-110, 5);
            rect.offsetMax = new Vector2(-60, 55);

            var image = buttonItemShop.GetComponent<Image>();
            image.sprite = Managers.Resources.DownloadData(IconType.ItemShop);

            var button = buttonItemShop.GetComponent<Button>();
            button.onClick.AddListener(OnOpenItemShop);
        }
    }

    private             void                        OnRise()
    {
        this.planetController.OnRise();
        Managers.Player.Rise();
        Close();
    }

    private             void                        OnOpenShipShop()
    {
        if(shipShop == null)
        {
            shipShop = Instantiate(Managers.Resources.DownloadData(ObjectType.ShipShop));
            Managers.Canvas.AddModule(shipShop);
            shipShop.GetComponent<RectTransform>().SetParent(this.gameObject.transform, false);

            shipShop.GetComponent<ShipShop>().Init();
        }
        else if(shipShop != null)
        {
            shipShop.SetActive(!shipShop.activeInHierarchy);
        }
    }

    private             void                        OnOpenItemShop()
    {
        planetController.OnOpenItemShop();
    }

    private             void                        Close()
    {
        Destroy(this.gameObject);
    }
}
