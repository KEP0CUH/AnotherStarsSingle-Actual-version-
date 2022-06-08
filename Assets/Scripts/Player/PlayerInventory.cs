using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : IPlayerInventory
{
    private Dictionary<int, ItemState> itemStates;

    public PlayerInventory()
    {
        itemStates = new Dictionary<int, ItemState>();
    }


    #region �������� � ��������� �������
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
            Debug.Log("���������� ��������");
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

    public void ReplaceItem(int id)
    {
        if(itemStates.ContainsKey(id))
        {

        }
    }


    #region �������� ���������
    public void ShowInventory()
    {
        CanvasUI.Inventory.ShowInventory(this, itemStates);
        Managers.Player.Controller.ShowInventory();
    }
    #endregion

    #region �������� ������� ���� ���������, ������� ���� � ������
    public Dictionary<int,ItemState> GetAllItems()
    {
        return this.itemStates;
    }
    #endregion

    #region �������� ���� �� ��������� �� ���, ���� ����� �������
    public ItemState GetItem(int id)
    {
        if (itemStates.ContainsKey(id))
            return itemStates[id];
        return null;
    }
    #endregion


    #region ������� ��������� ������� �� ���������
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
