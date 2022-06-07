using System.Collections.Generic;
using UnityEngine;

public class ShipState : MonoBehaviour
{
    [SerializeField] private ShipData data;
    [SerializeField] private ShipInventory inventory;
    private ShipController controller;
    public ShipData Data => data;
    public ShipInventory Inventory => inventory;
    public ShipController Controller => controller;

    public ShipState Init(ShipKind kind)
    {
        this.data = Managers.Resources.DownloadData(kind);
        inventory = new ShipInventory(this);

        return this;
    }

    public ShipState Init(ShipController controller,ShipKind kind)
    {
        this.controller = controller;
        this.data = Managers.Resources.DownloadData(kind);
        inventory = new ShipInventory(this);

        return this;
    }
}
