///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;

public class ShipState : MonoBehaviour
{
    [SerializeField]
    private             ShipData                        data;
    [SerializeField]
    private             ShipInventory                   inventory;
    private             InventoryController             inventoryController;
    private             ShipController                  controller;
    public              ShipData                        Data => data;
    public              ShipInventory                   Inventory => inventory;
    public              ShipController                  Controller => controller;
    public              InventoryController             InventoryController => inventoryController;

    public              ShipState                       Init(ShipKind               kind,
                                                             InventoryController    inventoryController)
    {
        this.data = Managers.Resources.DownloadData(kind);
        this.inventoryController = inventoryController;
        this.inventory = new ShipInventory(this.transform,this,inventoryController);

        return this;
    }

    public              ShipState                       Init(ShipController         controller,
                                                             ShipKind               kind,
                                                             InventoryController    inventoryController)
    {
        this.controller = controller;
        this.data = Managers.Resources.DownloadData(kind);
        this.inventoryController = inventoryController;
        this.inventory = new ShipInventory(this.transform,this,this.inventoryController);

        return this;
    }
}
