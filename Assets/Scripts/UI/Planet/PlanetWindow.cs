using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class PlanetWindow : MonoBehaviour
{
    private PlanetState planetState;
    private GameObject shipShop = null;
    private GameObject itemShop = null;
    private Dictionary<int,ItemShopController> itemShops;
    //private ItemShopController ItemShopController
    public PlanetState PlanetState => planetState;

    public void Init(PlanetState state)
    {
        this.planetState = state;
        itemShops = new Dictionary<int, ItemShopController>();
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
    private void Start()
    {
        this.GetComponent<Image>().sprite = planetState.Data.IconBG;
    }




    private void CreateRiseButton()
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

    private void CreateShipShop()
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

    private void CreateItemShop()
    {
        if(this.planetState.Data.ItemShopType != ItemShopType.ShopEmpty)
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

    private void OnRise()
    {
        Managers.Player.Rise();
        Close();
    }

    private void OnOpenShipShop()
    {
        if(shipShop == null)
        {
            shipShop = new GameObject("ShipShop", typeof(Image), typeof(ShipShop),typeof(Mask));
            var rect = shipShop.GetComponent<RectTransform>();
            rect.SetParent(this.gameObject.transform, false);
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.offsetMin = new Vector2(-250, -150);
            rect.offsetMax = new Vector2(250, 150);

            shipShop.GetComponent<ShipShop>().Init();
        }
        else if(shipShop != null)
        {
            shipShop.SetActive(!shipShop.activeInHierarchy);
        }
    }

    private void OnOpenItemShop()
    {
        if (itemShops.ContainsKey(planetState.Id))
        {
            itemShops[planetState.Id].gameObject.SetActive(!itemShops[planetState.Id].gameObject.activeInHierarchy);
            return;
        }
        else
        {
            itemShop = new GameObject("ItemShop", typeof(ItemShopController));

            var rect = itemShop.GetComponent<RectTransform>();
            rect.SetParent(this.gameObject.transform, false);
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.offsetMin = new Vector2(-250, -200);
            rect.offsetMax = new Vector2(250, 200);

            itemShop.GetComponent<ItemShopController>().Init(planetState.Data.ItemShopType,planetState.Id);
            itemShops.Add(planetState.Id, itemShop.GetComponent<ItemShopController>());
        }
    }

    private void Close()
    {
        Destroy(this.gameObject);
    }
}
