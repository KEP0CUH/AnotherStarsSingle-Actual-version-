using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInventory : IPlayerInventory
{
    private Dictionary<int, ItemState> itemStates;

    public PlayerInventory()
    {
        itemStates = new Dictionary<int, ItemState>();
    }


    #region днаюбхрэ б хмбемрюпэ опедлер
    public void AddItem(ItemState addedState,int count = 1, bool needDestroying = false)
    {
        if(addedState.IsEmpty())
        {
            return;
        }

        if (itemStates.ContainsKey(addedState.Id))
        {
            return;
        }
        else
        {
            Debug.Log("дНАЮБКЕМХЕ ОПЕДЛЕРЮ");
            if (addedState.IsItem)
            {
                foreach (var existState in itemStates.Values)
                {
                    if (existState.Data.ItemKind == addedState.Data.ItemKind)
                    {
                        existState.IncreaseNumber(count);
                        if(needDestroying && existState.Count <= 0) Object.Destroy(addedState.gameObject);
                        ShowInventory();
                        return;
                    }
                }

            }

            GameObject newItemStateObj;
            ItemState newItemState;

            if (addedState.IsWeapon)
            {
                newItemStateObj = new GameObject(($"{addedState.Data.Title}"), typeof(GunState));
                newItemState = newItemStateObj.GetComponent<GunState>();
                newItemState.Init((GunState)addedState);
            }
            else if (addedState.IsDevice)
            {
                newItemStateObj = new GameObject(($"{addedState.Data.Title}"), typeof(DeviceState));
                newItemState = newItemStateObj.GetComponent<DeviceState>();
                newItemState.Init((DeviceState)addedState);
            }
            else
            {
                newItemStateObj = new GameObject(($"{addedState.Data.Title}"), typeof(ItemState));
                newItemState = newItemStateObj.GetComponent<ItemState>().Init(addedState);
            }
            if (needDestroying) Object.Destroy(addedState.gameObject);
            itemStates.Add(newItemState.Id, newItemState);
            ShowInventory();
        }
    }
    #endregion


    #region онйюгюрэ хмбемрюпэ
    public void ShowInventory()
    {
        CanvasUI.Inventory.ShowInventory(this, itemStates);
    }
    #endregion

    #region онксвхрэ якнбюпэ бяеу опедлернб, йнрнпше еярэ с хцпнйю
    public Dictionary<int,ItemState> GetAllItems()
    {
        return this.itemStates;
    }
    #endregion

    #region онксвхрэ хрел хг хмбемрюпъ он хдс, еякх рюйни хлееряъ
    public ItemState GetItem(int id)
    {
        if (itemStates.ContainsKey(id))
            return itemStates[id];
        return null;
    }
    #endregion


    #region сдюкхрэ сйюгюммши опедлер хг хмбемрюпъ
    public void RemoveItem(ItemState state,int count = 1,bool needDestroying = false)
    {
        if (itemStates.ContainsKey(state.Id))
        {
            if (state.IsItem)
            {
                itemStates[state.Id].DecreaseNumber(count);
                if (itemStates[state.Id].Count <= 0)
                {
                    itemStates.Remove(state.Id);
                    if(needDestroying) Object.Destroy(state.gameObject);
                }
                ShowInventory();
            }
            else
            {
                itemStates.Remove(state.Id);
                if (needDestroying) Object.Destroy(state.gameObject);
                ShowInventory();
            }
        }
    }
    #endregion

}
