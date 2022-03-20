using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
    [SerializeField]
    private Dictionary<BaseItemData,int> items;

    public ManagerStatus Status {get;private set;}

    public void Startup()
    {
        Debug.Log("Inventory manager starting...".SetColor(Color.Yellow));

        items = new Dictionary<BaseItemData, int>();

        Status = ManagerStatus.Started;
        Debug.Log("Inventory manager started.".SetColor(Color.Green));
    }

    public Dictionary<BaseItemData, int> GetItemList()
    {
        Dictionary<BaseItemData, int> items = new Dictionary<BaseItemData, int>(this.items);
        return items;
    }

    public int GetItemCount(BaseItemData item)
    {
        if(items.ContainsKey(item))
            return items[item];
        return 0;
    }

    public void AddItem(BaseItemData data)
    {
        foreach(var item in items)
        {
            if(item.Key.Title == data.Title)
            {
                items[item.Key] += 1;
                DisplayItems();
                return;
            }
        }

        items.Add(data, 1);
        DisplayItems();
    }

    public void RemoveItem(BaseItemData data)
    {
        if(items.ContainsKey(data))
        {
            items.Remove(data);
        }

        DisplayItems();
    }

    private void DisplayItems()
    {
        string itemDisplay = "Items: ";
        foreach (var item in items)
        {
            itemDisplay += $" {item.Key}: {item.Value}; ";
        }

        Debug.Log(itemDisplay);

        CanvasUI.Inventory.ShowInventory(items);
    }
}

