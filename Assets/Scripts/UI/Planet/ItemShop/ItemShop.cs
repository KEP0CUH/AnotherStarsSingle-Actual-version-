using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(ScrollRect))]
public class ItemShop : MonoBehaviour
{
    private List<ItemData> itemsForBuyingData;
    private List<ItemState> itemsForBuyingStates;
    private List<ItemSlotShop> itemsForBuyingSlots;

    private GameObject shopList;

    public void Init()
    {
        var scroll = GetComponent<ScrollRect>();
        scroll.horizontal = true;
        scroll.vertical = false;
        scroll.scrollSensitivity = 15;

        CreateUpPart();
        itemsForBuyingData = new List<ItemData>();
        itemsForBuyingStates = new List<ItemState>();
        itemsForBuyingSlots = new List<ItemSlotShop>();

        ShowItems();
    }

    private void CreateUpPart()
    {
        shopList = new GameObject("ShopList", typeof(Image), typeof(GridLayoutGroup), typeof(ContentSizeFitter));
        var rect = shopList.GetComponent<RectTransform>();
        rect.SetParent(this.gameObject.transform, false);
        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(0, 1);
        rect.pivot = new Vector2(0, 1);
        rect.offsetMin = new Vector2(5, -55);
        rect.offsetMax = new Vector2(490, -5);

        var image = shopList.GetComponent<Image>();
        image.color = new UnityEngine.Color(134, 183, 219, 90) / 256f;
        image.raycastTarget = false;

        var layout = shopList.GetComponent<GridLayoutGroup>();
        layout.cellSize = new Vector2(64, 64);
        layout.spacing = new Vector2(15, 15);
        layout.padding.top = 5;
        layout.padding.left = 5;
        layout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        layout.constraintCount = 2;

        var fitter = shopList.GetComponent<ContentSizeFitter>();
        fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;


        this.gameObject.GetComponent<ScrollRect>().content = shopList.GetComponent<RectTransform>();
    }

    private void ShowItems()
    {
        itemsForBuyingData.Add(Managers.Resources.DownloadData(ItemKind.rudaGold));
        itemsForBuyingData.Add(Managers.Resources.DownloadData(ItemKind.rudaFerrum));
        itemsForBuyingData.Add(Managers.Resources.DownloadData(ItemKind.weaponMultiblaster));
        itemsForBuyingData.Add(Managers.Resources.DownloadData(ItemKind.weaponDesintegrator));


        for(int i = 0; i < itemsForBuyingData.Count; i++)
        {
            var newState = new GameObject($"ItemForBuying:{itemsForBuyingData[i].Title}", typeof(ItemState));
            newState.GetComponent<ItemState>().Init(itemsForBuyingData[i].ItemKind, 1);
            itemsForBuyingStates.Add(newState.GetComponent<ItemState>());
        }

        for(int i = 0; i < itemsForBuyingStates.Count;i++)
        {
            var newObj = new GameObject($"{itemsForBuyingStates[i].Data.Title}", typeof(ItemSlotShop));
            newObj.GetComponent<ItemSlot>().Init(shopList.transform, Managers.Player.Controller.Inventory, itemsForBuyingStates[i].GetComponent<ItemState>());
            itemsForBuyingSlots.Add(newObj.GetComponent<ItemSlotShop>().Init(shopList.transform, itemsForBuyingStates[i].GetComponent<ItemState>()));
        }

    }
}
