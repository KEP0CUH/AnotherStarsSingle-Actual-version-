using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : IInventory
{
    private Dictionary<ItemKind, BaseItemState> items;

    public PlayerInventory()
    {
        items = new Dictionary<ItemKind, BaseItemState>();
    }

    public void AddItem(ItemKind kind, BaseItemState state)
    {
        foreach (var item in items)
        {
            if (item.Key == kind)
            {
                items[item.Key].IncreaseNumber();
                ShowInventory();
                return;
            }
        }


        GameObject newItemStateObj;
        BaseItemState newItemState;

        if (state.IsWeapon)
        {
            newItemStateObj = new GameObject(($"{state.Data.Title}"), typeof(GunState));
            newItemState = newItemStateObj.GetComponent<GunState>();
            newItemState.Init(((GunState)state).GunKind, state.Count);
        }
        else
        {
            newItemStateObj = new GameObject(($"{state.Data.Title}"),typeof(BaseItemState));
            newItemState = newItemStateObj.GetComponent<BaseItemState>();
            newItemState.Init(kind, state.Count);
        }
        items.Add(kind, newItemState);
        ShowInventory();
    }

    public BaseItemState GetItem(ItemKind kind)
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
        foreach(var item in items)
        {
            Debug.Log($"{item.Key}: {item.Value}".SetColor(Color.Magenta));
        }
        CanvasUI.Inventory.ShowInventory(this, items);
    }


}
