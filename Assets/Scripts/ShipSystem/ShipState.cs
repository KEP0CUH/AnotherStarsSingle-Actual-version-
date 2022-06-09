using System.Collections.Generic;
using UnityEngine;

public class ShipState : MonoBehaviour
{
    [SerializeField] private ShipData data;
    [SerializeField] private ShipInventory inventory;
    private InventoryController inventoryController;
    private ShipController controller;
    public ShipData Data => data;
    public ShipInventory Inventory => inventory;
    public ShipController Controller => controller;
    public InventoryController InventoryController => inventoryController;

    public ShipState Init(ShipKind kind,InventoryController inventoryController)
    {
        this.data = Managers.Resources.DownloadData(kind);
        inventory = new ShipInventory(this);
        this.inventoryController = inventoryController;

        return this;
    }

    public ShipState Init(ShipController controller,ShipKind kind,InventoryController inventoryController)
    {
        this.controller = controller;
        this.data = Managers.Resources.DownloadData(kind);
        inventory = new ShipInventory(this);
        this.inventoryController = inventoryController;

        return this;
    }
}
