using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : IPlayerInventory
{
    private Dictionary<int, ItemState> items;

    public PlayerInventory()
    {
        items = new Dictionary<int, ItemState>();
    }


    #region днаюбхрэ б хмбемрюпэ опедлер
    public void AddItem(ItemState addedItem,int count = 1, bool needDestroying = false)
    {
        if(addedItem.IsEmpty())
        {
            return;
        }

        if (items.ContainsKey(addedItem.Id))
        {
            return;
        }
        else
        {
            Debug.Log("дНАЮБКЕМХЕ ОПЕДЛЕРЮ");
            if (addedItem.IsItem)
            {
                foreach (var existState in items.Values)
                {
                    if (existState.Data.ItemKind == addedItem.Data.ItemKind)
                    {
                        existState.IncreaseNumber(count);
                        if(needDestroying && existState.Count <= 0) Object.Destroy(addedItem.gameObject);
                        ShowInventory();
                        return;
                    }
                }
            }

            GameObject newItemStateObj;
            ItemState newItemState;

            if (addedItem.IsWeapon)
            {
                newItemStateObj = new GameObject(($"{addedItem.Data.Title}"), typeof(GunState));
                newItemState = newItemStateObj.GetComponent<GunState>();
                newItemState.Init((GunState)addedItem);
            }
            else if (addedItem.IsDevice)
            {
                newItemStateObj = new GameObject(($"{addedItem.Data.Title}"), typeof(DeviceState));
                newItemState = newItemStateObj.GetComponent<DeviceState>();
                newItemState.Init((DeviceState)addedItem);
            }
            else
            {
                newItemStateObj = new GameObject(($"{addedItem.Data.Title}"), typeof(ItemState));
                newItemState = newItemStateObj.GetComponent<ItemState>().Init(addedItem);
            }
            if (needDestroying) Object.Destroy(addedItem.gameObject);
            items.Add(newItemState.Id, newItemState);
            ShowInventory();
        }
    }
    #endregion

    public void ReplaceItem(int id)
    {
        if(items.ContainsKey(id))
        {

        }
    }


    #region онйюгюрэ хмбемрюпэ
    public void ShowInventory()
    {
        CanvasUI.Inventory.ShowInventory(this, items);
        Managers.Player.Controller.ShowInventory();
    }
    #endregion

    #region онксвхрэ якнбюпэ бяеу опедлернб, йнрнпше еярэ с хцпнйю
    public Dictionary<int,ItemState> GetAllItems()
    {
        return this.items;
    }
    #endregion

    #region онксвхрэ хрел хг хмбемрюпъ он хдс, еякх рюйни хлееряъ
    public ItemState GetItem(int id)
    {
        if (items.ContainsKey(id))
            return items[id];
        return null;
    }
    #endregion


    #region сдюкхрэ сйюгюммши опедлер хг хмбемрюпъ
    public void RemoveItem(ItemState state,int count = 1,bool needDestroying = false)
    {
        if (items.ContainsKey(state.Id))
        {
            if (state.IsItem)
            {
                items[state.Id].DecreaseNumber(count);
                if (items[state.Id].Count <= 0)
                {
                    items.Remove(state.Id);
                    if(needDestroying) Object.Destroy(state.gameObject);
                }
                ShowInventory();
            }
            else
            {
                items.Remove(state.Id);
                if (needDestroying) Object.Destroy(state.gameObject);
                ShowInventory();
            }
        }
    }
    #endregion

}
