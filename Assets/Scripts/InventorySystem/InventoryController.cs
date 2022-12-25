///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;
using System.Collections.Generic;

public class InventoryController
{
    private         Dictionary<int, ItemState>          items;
    private         Transform                           parent;

    public          InventoryController(Transform parent)
    {
        items = new Dictionary<int, ItemState>();
        this.parent = parent;
    }

    /// <summary>
    /// ����� ��� ���������� ������ ����� � ��������� NPC. ������ ������������ ����������� ��� �������� �����,
    /// � ������� ������������ ���������� ����������������. �������� template<class T>, 
    /// ��� � ������ T ����� ��������� ���������: 1) ����� IsEmpty(��������� �� �������� �� ����)
    ///                                           2) ����� IsItem(��������� �������� �� ������� ������� �����)
    ///                                           3) ����� IsGun(��������� �������� �� ������� �������)
    ///                                           4) ����� IsDevice(��������� �������� �� ������� �����������)
    /// </summary>
    /// <param name="addedItem"></param>
    /// <param name="count"></param>
    /// <param name="needDestroying"></param>
    public          void                                AddItem(ItemState addedItem,int count = 1,bool needDestroying = false)
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
                        item.Value.IncreaseCount(count);
                        if(needDestroying)
                        {
                            Object.Destroy(addedItem.gameObject);
                        }
                        else
                        {
                            addedItem.OnPickup();
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

            newItemStateObj.GetComponent<Transform>().SetParent(parent);
            this.items.Add(newItemState.Id, newItemState);
        }
    }

    public          Dictionary<int,ItemState>           GetItems()
    {
        return this.items;
    }

    public           void                               RemoveItem(ItemState removedItem,int count = 1,bool needDestroying = false)
    {
        if(items.ContainsKey(removedItem.Id))
        {
            if(removedItem.IsItem)
            {
                items[removedItem.Id].DecreaseCount(count);
                if(items[removedItem.Id].Count <= 0)
                {
                    items.Remove(removedItem.Id);
                    if(needDestroying) Object.Destroy(removedItem.gameObject);
                }
            }
            else
            {
                items.Remove(removedItem.Id);
                if (needDestroying) Object.Destroy(removedItem.gameObject);
            }
        }
    }
}
