using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : IInventory
{
    private Dictionary<ItemKind, int> items;

    public PlayerInventory()
    {
        items = new Dictionary<ItemKind, int>();
    }

    public void AddItem(ItemKind kind,int count)
    {
        foreach(var item in items)
        {
            if(item.Key == kind)
            {
                items[item.Key] += count;
                return;
            }
        }

        items.Add(kind, count);
    }

    public BaseItemData GetItem(ItemKind kind)
    {
        if(items.ContainsKey(kind))
        {
            var data = Resources.Load<BaseItemData>($"ScriptableObjects/" + kind.ToString());
            return data;
        }
        return null;
    }

    public void RemoveItem(ItemKind kind, int count = 1)
    {
        if (items.ContainsKey(kind))
        {
            items[kind] -= count;
            if(items[kind] <= 0)
                items.Remove(kind);
        }
    }
}
