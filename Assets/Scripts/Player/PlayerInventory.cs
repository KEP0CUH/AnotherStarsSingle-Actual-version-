using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInventory : IPlayerInventory
{
    private Dictionary<int, ItemState> itemsDic;

    public PlayerInventory()
    {
        itemsDic = new Dictionary<int, ItemState>();
    }


    #region днаюбхрэ б хмбемрюпэ опедлер
    public void AddItem(ItemState state)
    {
        if (state.Data.ItemKind == ItemKind.deviceEmpty || state.Data.ItemKind == ItemKind.weaponEmpty)
            return;

        if (itemsDic.ContainsKey(state.Id))
        {
            return;
        }
        else
        {
            Debug.Log("дНАЮБКЕМХЕ ОПЕДЛЕРЮ");
            if (state.IsItem)
            {
                foreach (var item in itemsDic.Values)
                {
                    if (item.Data.ItemKind == state.Data.ItemKind)
                    {
                        item.IncreaseNumber();
                        Object.Destroy(state.gameObject);
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
    #endregion


    #region онйюгюрэ хмбемрюпэ
    public void ShowInventory()
    {
        CanvasUI.Inventory.ShowInventory(this, itemsDic);
    }
    #endregion

    #region онксвхрэ якнбюпэ бяеу опедлернб, йнрнпше еярэ с хцпнйю
    public Dictionary<int,ItemState> GetAllItems()
    {
        return this.itemsDic;
    }
    #endregion

    #region онксвхрэ хрел хг хмбемрюпъ он хдс, еякх рюйни хлееряъ
    public ItemState GetItem(int id)
    {
        if (itemsDic.ContainsKey(id))
            return itemsDic[id];
        return null;
    }
    #endregion


    #region сдюкхрэ сйюгюммши опедлер хг хмбемрюпъ
    public void RemoveItem(ItemState state)
    {
        if (itemsDic.ContainsKey(state.Id))
        {
            if (state.IsItem)
            {
                itemsDic[state.Id].DecreaseNumber();
                if (itemsDic[state.Id].Count <= 0)
                {
                    itemsDic.Remove(state.Id);
                    //Object.Destroy(state.gameObject);
                }
                ShowInventory();
            }
            else
            {
                itemsDic.Remove(state.Id);
                //Object.Destroy(state.gameObject);
                ShowInventory();
            }
        }
    }
    #endregion

}
