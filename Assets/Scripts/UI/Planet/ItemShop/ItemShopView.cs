using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShopView : MonoBehaviour
{
    private ItemShopController itemShopController;

    private GameObject itemShopObject = null;
    private ItemShop itemShopComponent;
    private bool shopIsOpen = false;

    private Dictionary<int, ItemState> shopItems;
    private Dictionary<int, ItemState> playerItems;

    private List<ItemCell> shopItemsCells;
    private List<ItemCell> playerItemsCells;

    public ItemShopController controller => itemShopController;
    public ItemShop Shop => itemShopComponent;
    public bool ShopIsOpen => shopIsOpen;


    public ItemShopView Init(ItemShopController controller)
    {
        if (controller.State.Data.ItemShopType != ItemShopType.ShopEmpty)
        {
            this.itemShopController = controller;

            playerItems = new Dictionary<int, ItemState>();
            shopItems = new Dictionary<int, ItemState>();
            shopItemsCells = new List<ItemCell>();
            playerItemsCells = new List<ItemCell>();

            OpenItemShop();
            CreateStatesForShopItems();
            ShowListItemShop();
        }
        return this;
    }

    public void OpenItemShop()
    {
        if (itemShopObject == null)
        {
            shopIsOpen = true;
            itemShopObject = Instantiate(Managers.Resources.DownloadData(ObjectType.ItemShop));
            this.itemShopComponent = itemShopObject.GetComponent<ItemShop>();
            Managers.Canvas.AddModule(itemShopObject);
            itemShopComponent = itemShopObject.GetComponent<ItemShop>().Init(this);
        }
    }

    public void CloseItemShop()
    {
        if (itemShopObject != null)
        {
            shopIsOpen = false;
            Object.Destroy(itemShopObject);
            itemShopObject = null;
        }
    }

    private void CreateStatesForShopItems()
    {
        foreach (var itemData in controller.State.Data.ItemsForBuyingData)
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
            else if(itemData.IsItem())
            {
                newItemStateObject.AddComponent<ItemState>().Init(itemData.ItemKind, 1);

                var newItemStateComponent = newItemStateObject.GetComponent<ItemState>();
                AddItem(newItemStateComponent);
            }
        }
    }

    private void CreateStatesShopItems()
    {

    }

    public void AddItem(ItemState state,int count = 1, bool needDestroying = false)
    {
        if(state.IsEmpty())
        {
            return;
        }

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
                        item.IncreaseCount(count);
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
            }
            else if (state.IsDevice)
            {
                newItemStateObj = new GameObject(($"{state.Data.Title}"), typeof(DeviceState));
                newItemState = newItemStateObj.GetComponent<DeviceState>();
            }
            else
            {
                newItemStateObj = new GameObject(($"{state.Data.Title}"), typeof(ItemState));
                newItemState = newItemStateObj.GetComponent<ItemState>();
            }

            newItemState.Init(state);
            newItemStateObj.GetComponent<Transform>().SetParent(this.gameObject.transform);
            if(needDestroying) Object.Destroy(state.gameObject);
            shopItems.Add(newItemState.Id, newItemState);
            ShowListItemShop();
        }
    }

    public void RemoveItem(ItemState state,int count = 1, bool needDestroying = false)
    {
        if(shopItems.ContainsKey(state.Id))
        {
            if(shopItems[state.Id].IsItem)
            {
                shopItems[state.Id].DecreaseCount(count);
                if(shopItems[state.Id].Count <= 0)
                {
                    Object.Destroy(shopItems[state.Id]);
                    shopItems.Remove(state.Id);
                    if (needDestroying) Object.Destroy(state.gameObject);
                }
            }
            else
            {
                Object.Destroy(shopItems[state.Id]);
                shopItems.Remove(state.Id);
                if (needDestroying) Object.Destroy(state.gameObject);
            }
        }
    }

    public void ShowListItemShop()
    {
        Debug.Log("Здесь надо спавнить слоты для состояние в словаре shopItems");

        foreach(var item in shopItemsCells)
        {
            if (item != null)
            {
                Object.Destroy(item.gameObject);
            }
        }
        shopItemsCells.Clear();

        foreach(var item in playerItemsCells)
        {
            if (item != null)
            {
                Object.Destroy(item.gameObject);
            }
        }
        playerItemsCells.Clear();

        foreach(var item in shopItems)
        {
            var cellPrefab = Managers.Resources.DownloadData(ObjectType.ItemCell);
            var newCell = Instantiate(cellPrefab,itemShopComponent.ListShopItems.transform);

            var cellComponent = newCell.GetComponent<ItemCell>().Init(this, item.Value.GetComponent<ItemState>(), true);
            shopItemsCells.Add(cellComponent);
        }

        playerItems = new Dictionary<int, ItemState>();
        playerItems = Managers.Player.Controller.Inventory.GetItems();

        foreach (var item in playerItems)
        {
            Debug.Log($"{item.Value.Data.Title}");
            var cellPrefab = Managers.Resources.DownloadData(ObjectType.ItemCell);
            var newCell = Instantiate(cellPrefab,itemShopComponent.ListPlayerItems.transform);

            var cellComponent = newCell.GetComponent<ItemCell>().Init(this, item.Value.GetComponent<ItemState>(), false);
            playerItemsCells.Add(cellComponent);
        }
    }

}
