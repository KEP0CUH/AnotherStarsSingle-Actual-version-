using UnityEngine;
using System.Collections.Generic;

public class InventoryController
{
    private Dictionary<int, ItemState> items;

    public InventoryController()
    {
        items = new Dictionary<int, ItemState>();
    }

    public void AddItem(ItemState addedItem,int count = 1,bool needDestroying = false)
    {
        if (addedItem.IsEmpty())
        {
            return;
        }

        if(items.ContainsKey(addedItem.Id))
        {
            return;
        }
        else
        {
            if(addedItem.IsItem)
            {
                foreach(var item in items)
                {
                    if(item.Value.Data.ItemKind == addedItem.Data.ItemKind)
                    {
                        item.Value.IncreaseNumber(count);
                        if(needDestroying)
                        {
                            Object.Destroy(addedItem.gameObject);
                        }
                        return;
                    }
                }
            }

            GameObject newItemStateObj;
            ItemState newItemState;

            if (addedItem.IsWeapon)
            {
                newItemStateObj = new GameObject(($"{addedItem.Data.Title}"));
                newItemState = newItemStateObj.AddComponent<GunState>().Init((GunState)addedItem);
            }
            else if (addedItem.IsDevice)
            {
                newItemStateObj = new GameObject(($"{addedItem.Data.Title}"));
                newItemState = newItemStateObj.AddComponent<DeviceState>().Init((DeviceState)addedItem);
            }
            else
            {
                newItemStateObj = new GameObject(($"{addedItem.Data.Title}"));
                newItemState = newItemStateObj.AddComponent<ItemState>().Init(addedItem);
            }
            if (needDestroying) Object.Destroy(addedItem.gameObject);

            items.Add(newItemState.Id, newItemState);
        }
    }

    public Dictionary<int,ItemState> GetItems()
    {
        return this.items;
    }

    public void RemoveItem(ItemState removedItem,int count = 1,bool needDestroying = false)
    {
        if(items.ContainsKey(removedItem.Id))
        {
            if(removedItem.IsItem)
            {
                items[removedItem.Id].DecreaseNumber(count);
                if(items[removedItem.Id].Count <= 0)
                {
                    items.Remove(removedItem.Id);
                    if(needDestroying) Object.Destroy(removedItem.gameObject);
                }
            }
        }
    }
}
