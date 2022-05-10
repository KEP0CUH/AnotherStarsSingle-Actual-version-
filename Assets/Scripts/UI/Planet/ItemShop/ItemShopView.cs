using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShopView : MonoBehaviour
{
    private ItemShopState itemShopState;

    private GameObject prefabItemShop;
    private GameObject objectItemShop;

    private Dictionary<int, ItemState> playerItems;
    private Dictionary<int, ItemState> shopItems;

    public ItemShopView Init(ItemShopState state)
    {
        if (state.Data.ItemShopType != ItemShopType.ShopEmpty)
        {
            this.itemShopState = state;
            OpenItemShop();
            playerItems = new Dictionary<int, ItemState>();
            shopItems = new Dictionary<int, ItemState>();

            CreateStatesForShopItems();
        }
        return this;
    }

    private void OpenItemShop()
    {
        if (prefabItemShop == null)
        {
            var prefabItemShop = Managers.Resources.DownloadData(ObjectType.ItemShop);
            objectItemShop = Instantiate(prefabItemShop, this.transform);
            var rect = objectItemShop.GetComponent<RectTransform>();
            rect.SetParent(this.gameObject.transform, false);
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.offsetMin = new Vector2(-250, -200);
            rect.offsetMax = new Vector2(250, 200);

            //objectItemShop.GetComponent<ItemShop>().Init();
        }
        else if (objectItemShop != null)
        {
            objectItemShop.SetActive(!objectItemShop.activeInHierarchy);
        }
    }

    private void CreateStatesForShopItems()
    {
        foreach (var itemData in this.itemShopState.Data.ItemsForBuyingData)
        {
            var newItemStateObject = new GameObject("shopItem");
            if (itemData.IsWeapon())
            {
                newItemStateObject.AddComponent<GunState>().Init(itemData.ItemKind, 1);

                var newItemStateComponent = newItemStateObject.GetComponent<GunState>();
                AddItem(newItemStateComponent);
            }
            else if (itemData.IsDevice())
            {
                newItemStateObject.AddComponent<DeviceState>().Init(itemData.ItemKind, 1);

                var newItemStateComponent = newItemStateObject.GetComponent<DeviceState>();
                AddItem(newItemStateComponent);
            }
            else
            {
                newItemStateObject.AddComponent<ItemState>().Init(itemData.ItemKind, 1);

                var newItemStateComponent = newItemStateObject.GetComponent<ItemState>();
                AddItem(newItemStateComponent);
            }
        }
    }

    public void AddItem(ItemState state, bool needDestroying = true)
    {
        if (state.Data.ItemKind == ItemKind.EmptyDevice ||
            state.Data.ItemKind == ItemKind.EmptyGun ||
            state.Data.ItemKind == ItemKind.EmptyDevice)
            return;

        if (shopItems.ContainsKey(state.Id))
        {
            return;
        }
        else
        {
            Debug.Log("Предмет добавляется в магазин...");
            if (state.IsItem)
            {
                foreach (var item in shopItems.Values)
                {
                    if (item.Data.ItemKind == state.Data.ItemKind)
                    {
                        item.IncreaseNumber(state.Count);
                        if (needDestroying) Object.Destroy(state.gameObject);
                        ShowListItemShop();
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
            if(needDestroying) Object.Destroy(state.gameObject);
            shopItems.Add(newItemState.Id, newItemState);
            ShowListItemShop();
        }
    }

    private void ShowListItemShop()
    {
        Debug.Log("Здесь надо спавнить слоты для состояние в словаре shopItems");
    }

}
