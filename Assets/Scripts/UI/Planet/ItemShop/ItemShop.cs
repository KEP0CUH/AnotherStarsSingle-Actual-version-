using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(ScrollRect))]
public class ItemShop : MonoBehaviour
{
    private List<ItemData> itemsForBuyingData;
    private List<ItemSlotShop> itemsForBuyingSlots;

    private Dictionary<int,ItemState> shopItems;

    private GameObject shopList;

    public void Init()
    {
        var scroll = GetComponent<ScrollRect>();
        scroll.horizontal = true;
        scroll.vertical = false;
        scroll.scrollSensitivity = 15;

        CreateUpPart();

        itemsForBuyingData = new List<ItemData>();
        itemsForBuyingData.Add(Managers.Resources.DownloadData(ItemKind.rudaGold));
        itemsForBuyingData.Add(Managers.Resources.DownloadData(ItemKind.rudaFerrum));
        itemsForBuyingData.Add(Managers.Resources.DownloadData(ItemKind.weaponMultiblaster));
        itemsForBuyingData.Add(Managers.Resources.DownloadData(ItemKind.weaponDesintegrator));


        itemsForBuyingSlots = new List<ItemSlotShop>();

        shopItems = new Dictionary<int, ItemState>();

        CreateStatesForData();
        ShowItems();
    }

    public void RemoveItemState(ItemState state)
    {
        if(shopItems.ContainsKey(state.Id))
        {
            shopItems.Remove(state.Id);

            foreach (var item in itemsForBuyingSlots)
            {
                if(item.gameObject != null)
                    Object.Destroy(item.gameObject);
            }

            itemsForBuyingSlots.Clear();
            CreateStatesForData();
            ShowItems();
        }
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
        Debug.Log("Показ магазина предметов");

        foreach(var item in shopItems)
        {
            var newObj = new GameObject($"{item.Value.Data.Title}", typeof(ItemSlotShop));
            itemsForBuyingSlots.Add(newObj.GetComponent<ItemSlotShop>().Init(this, shopList.transform, item.Value.GetComponent<ItemState>()));
        }

    }

    private void CreateStatesForData()
    {
        for (int i = 0; i < itemsForBuyingData.Count; i++)
        {
            var newStateObj = new GameObject($"ItemForBuying:{itemsForBuyingData[i].Title}");
            if (itemsForBuyingData[i].IsWeapon())
            {
                newStateObj.AddComponent<GunState>().Init(itemsForBuyingData[i].ItemKind, 1);

                var state = newStateObj.GetComponent<GunState>();
                AddItem(state);
            }
            else if (itemsForBuyingData[i].IsDevice())
            {
                newStateObj.AddComponent<DeviceState>().Init(itemsForBuyingData[i].ItemKind, 1);

                var state = newStateObj.GetComponent<DeviceState>();
                AddItem(state);
            }
            else
            {
                newStateObj.AddComponent<ItemState>().Init(itemsForBuyingData[i].ItemKind, 1);

                var state = newStateObj.GetComponent<ItemState>();
                AddItem(state);

            }
            Object.Destroy(newStateObj);
        }
    }

    #region ДОБАВИТЬ В ИНВЕНТАРЬ ПРЕДМЕТ
    public void AddItem(ItemState state)
    {
        if (state.Data.ItemKind == ItemKind.deviceEmpty || state.Data.ItemKind == ItemKind.weaponEmpty)
            return;

        if (shopItems.ContainsKey(state.Id))
        {
            return;
        }
        else
        {
            Debug.Log("Добавление предмета");
            if (state.IsItem)
            {
                foreach (var item in shopItems.Values)
                {
                    if (item.Data.ItemKind == state.Data.ItemKind)
                    {
                        item.IncreaseNumber();
                        Object.Destroy(state.gameObject);
                        ShowItems();
                        return;
                    }
                }

            }

            GameObject newItemStateObj;
            ItemState newItemState;

            if (state.IsWeapon)
            {
                newItemStateObj = new GameObject(($"{state.Data.Title}"), typeof(GunState));
                newItemState = newItemStateObj.GetComponent<GunState>();
                newItemState.Init((GunState)state);
            }
            else if (state.IsDevice)
            {
                newItemStateObj = new GameObject(($"{state.Data.Title}"), typeof(DeviceState));
                newItemState = newItemStateObj.GetComponent<DeviceState>();
                newItemState.Init((DeviceState)state);
            }
            else
            {
                newItemStateObj = new GameObject(($"{state.Data.Title}"), typeof(ItemState));
                newItemState = newItemStateObj.GetComponent<ItemState>();
                newItemState.Init(state);
            }
            Object.Destroy(state.gameObject);
            shopItems.Add(newItemState.Id, newItemState);
            ShowItems();
        }
    }
    #endregion

}
