using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInventory : IInventory
{
    private Dictionary<ItemKind, ItemState> items;
    private Dictionary<int, ItemState> itemsDic;

    public PlayerInventory()
    {
        items = new Dictionary<ItemKind, ItemState>();
        itemsDic = new Dictionary<int,ItemState>();
    }


    public void AddItem(ItemKind kind, ItemState state)
    {
        foreach (var item in items)
        {
            if (item.Key == kind)
            {
                items[item.Key].IncreaseNumber();
                GameObject.Destroy(state.gameObject);
                ShowInventory();
                return;
            }
        }


        GameObject newItemStateObj;
        ItemState newItemState;

        if (state.IsWeapon)
        {
            newItemStateObj = new GameObject(($"{state.Data.Title}"), typeof(GunState));
            newItemState = newItemStateObj.GetComponent<GunState>();
            newItemState.Init(((GunState)state).Data.ItemKind, state.Count);
        }
        else if(state.IsDevice)
        {
            newItemStateObj = new GameObject(($"{state.Data.Title}"), typeof(DeviceState));
            newItemState = newItemStateObj.GetComponent<DeviceState>();
            newItemState.Init(((DeviceState)state).Data.ItemKind, state.Count);
        }
        else
        {
            newItemStateObj = new GameObject(($"{state.Data.Title}"),typeof(ItemState));
            newItemState = newItemStateObj.GetComponent<ItemState>();
            newItemState.Init(kind, state.Count);
        }
        GameObject.Destroy(state.gameObject);
        items.Add(kind, newItemState);
        ShowInventory();
    }

    public ItemState GetItem(ItemKind kind)
    {
        if (items.ContainsKey(kind))
        {
            return items[kind];
        }
        return null;
    }



    public void RemoveItem(ItemKind kind)
    {
        if (items.ContainsKey(kind))
        {
            items[kind].DecreaseNumber();
            if (items[kind].Count <= 0)
            {
                GameObject.Destroy(items[kind].gameObject);
                items.Remove(kind);
            }

        }
        ShowInventory();
    }



    public void ShowInventory()
    {
        Debug.Log("Показ инвентаря");
        CanvasUI.Inventory.ShowInventory(this, itemsDic);
    }


    public void AddItem(ItemState state)
    {
        if(itemsDic.ContainsKey(state.Id))
        {
            return;
        }
        else
        {
            Debug.Log("Добавление предмета");
            if (state.IsItem)
            {
                foreach (var item in itemsDic.Values)
                {
                    if (item.Data.ItemKind == state.Data.ItemKind)
                    {
                        item.IncreaseNumber();
                        ShowInventory();
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
            itemsDic.Add(newItemState.Id, newItemState);
            ShowInventory();
        }
        
    }
    public ItemState GetItem(int id)
    {
        if (itemsDic.ContainsKey(id))
            return itemsDic[id];
        return null;
    }

    public void RemoveItem(ItemState state)
    {
        if (itemsDic.ContainsKey(state.Id))
        {
            if (state.IsItem)
            {
                itemsDic[state.Id].DecreaseNumber();
                if (itemsDic[state.Id].Count < 0)
                {
                    itemsDic.Remove(state.Id);
                    Object.Destroy(state.gameObject);
                }
                ShowInventory();
            }
            else
            {
                itemsDic.Remove(state.Id);
                Object.Destroy(state.gameObject);
                ShowInventory();
            }
        }
    }

}
