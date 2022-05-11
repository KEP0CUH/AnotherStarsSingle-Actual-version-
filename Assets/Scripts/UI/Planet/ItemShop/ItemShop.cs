using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(ScrollRect))]
public class ItemShop : MonoBehaviour
{
    [SerializeField] private ItemShopType shopType;
    private ItemShopView itemShopView;

    private List<ItemData> itemsForBuyingData;
    private List<ItemSlotShop> itemsForBuyingSlots;
    private Dictionary<int,ItemState> playerItems;

    private Dictionary<int,ItemState> shopItems;

    [SerializeField] private GameObject listShopItems;
    [SerializeField] private GameObject listPlayerItems;

    public ItemShopView ItemShopView => itemShopView;
    public GameObject ListShopItems => listShopItems;
    public GameObject ListPlayerItems => listPlayerItems;

    public ItemShop Init(ItemShopView itemShopView)
    {
        this.itemShopView = itemShopView;
        this.shopType = ItemShopType.GreenShop1;
        var scroll = GetComponent<ScrollRect>();
        scroll.horizontal = true;
        scroll.vertical = false;
        scroll.scrollSensitivity = 15;



        itemsForBuyingSlots = new List<ItemSlotShop>();

        playerItems = new Dictionary<int, ItemState>();
        shopItems = new Dictionary<int, ItemState>();

        itemShopView.ShowListItemShop();
        return this;
    }

    public void RemoveItemState(ItemState state)
    {
        if(shopItems.ContainsKey(state.Id))
        {
            shopItems.Remove(state.Id);
            ShowItems();
        }
    }

    public void ShowItems()
    {
        Debug.Log("Показ магазина предметов");

        foreach(var item in itemsForBuyingSlots)
        {
            if(item != null)
            {
                Object.Destroy(item.gameObject);
            }
        }

        itemsForBuyingSlots.Clear();

        foreach(var item in shopItems)
        {
            var newObj = new GameObject($"{item.Value.Data.Title}", typeof(ItemSlotShop));
            itemsForBuyingSlots.Add(newObj.GetComponent<ItemSlotShop>().Init(itemShopView,this, listShopItems.transform, item.Value.GetComponent<ItemState>(),true));
        }

        Debug.Log("Показ инвентаря игрока в магазине предметов");

        playerItems = Managers.Player.Controller.Inventory.GetAllItems();

        foreach (var item in playerItems)
        {
            var newObj = new GameObject($"{item.Value.Data.Title}", typeof(ItemSlotShop));
            itemsForBuyingSlots.Add(newObj.GetComponent<ItemSlotShop>().Init(itemShopView,this,listPlayerItems.transform,item.Value.GetComponent<ItemState>(),false));
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
                Object.Destroy(state.gameObject);
            }
            else if (itemsForBuyingData[i].IsDevice())
            {
                newStateObj.AddComponent<DeviceState>().Init(itemsForBuyingData[i].ItemKind, 1);

                var state = newStateObj.GetComponent<DeviceState>();
                AddItem(state);
                Object.Destroy(state.gameObject);
            }
            else
            {
                newStateObj.AddComponent<ItemState>().Init(itemsForBuyingData[i].ItemKind, 1);

                var state = newStateObj.GetComponent<ItemState>();
                AddItem(state);
                Object.Destroy(state.gameObject);
            }
            Object.Destroy(newStateObj);
        }
    }

    #region ДОБАВИТЬ В ИНВЕНТАРЬ МАГАЗИНА ПРЕДМЕТ
    public void AddItem(ItemState state)
    {
        if (state.Data.ItemKind == ItemKind.EmptyDevice || state.Data.ItemKind == ItemKind.EmptyGun)
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
                        //Object.Destroy(state.gameObject);
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
            //Object.Destroy(state.gameObject);
            shopItems.Add(newItemState.Id, newItemState);
            ShowItems();
        }
    }
    #endregion

}
