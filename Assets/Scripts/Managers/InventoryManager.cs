using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
    [SerializeField]
    private Dictionary<string,int> items;

    public ManagerStatus Status {get;private set;}

    public void Startup()
    {
        Debug.Log("Inventory manager starting...".SetColor(Color.Yellow));

        items = new Dictionary<string, int>();

        Status = ManagerStatus.Started;
        Debug.Log("Inventory manager started.".SetColor(Color.Green));
    }

    public List<string> GetItemList()
    {
        List<string> list = new List<string>(items.Keys);
        return list;
    }

    public int GetItemCount(string item)
    {
        if (items.ContainsKey(item))
            return items[item];
        return 0;
    }

    public void AddItem(BaseItemData data)
    {
        if(items.ContainsKey(data.Title))
        {
            items[data.Title] += 1;
        }
        else
        {
            items.Add(data.Title, 1);
        }

        DisplayItems();
    }

    public void RemoveItem(BaseItemData data)
    {
        if(items.ContainsKey(data.Title))
        {
            items.Remove(data.Title);
        }
    }

    private void DisplayItems()
    {
        string itemDisplay = "Items: ";
        foreach (var item in items)
        {
            itemDisplay += $" {item.Key}: {item.Value}; ";
        }

        Debug.Log(itemDisplay);
    }
}

