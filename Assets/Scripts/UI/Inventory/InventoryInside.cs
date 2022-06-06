using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInside : MonoBehaviour
{
    [SerializeField] private GameObject gunsList;
    [SerializeField] private GameObject devicesList;
    [SerializeField] private GameObject itemsList;

    private List<GameObject> gunCells = new List<GameObject>();
    private List<GameObject> deviceCells = new List<GameObject>();
    private List<GameObject> itemCells = new List<GameObject>();
    public InventoryInside Init()
    {
        gunCells = new List<GameObject>();
        deviceCells = new List<GameObject>();
        itemCells = new List<GameObject>();

        return this;
    }

    public void ShowInventory(IShipInventory inventory)
    {
        ShowGuns();
        ShowDevices();
        ShowItems();
    }

    private void ShowGuns()
    {

    }

    private void ShowDevices()
    {

    }

    private void ShowItems()
    {

    }
}
