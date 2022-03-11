using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
    [SerializeField]
    private List<BaseItemData> items;

    public ManagerStatus Status {get;private set;}

    public void Startup()
    {
        Debug.Log("Inventory manager started...");

        items = new List<BaseItemData>();

        Status = ManagerStatus.Started;
    }

    private void DisplayItems()
    {
        string itemDisplay = "Items: ";
        foreach(var item in items)
        {
            itemDisplay += $" {item.Title}: {item.Description} ";
        }

        Debug.Log(itemDisplay);
    }

    public void AddItem(BaseItemData data)
    {
        items.Add(data);

        DisplayItems();
    }
}

