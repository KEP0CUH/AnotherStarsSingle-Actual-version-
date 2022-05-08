using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Selectable))]
public class DeviceSlot : ItemSlot
{
    private IShipInventory shipInventory;

    public void Init(Transform transform, IShipInventory shipInventory, ItemState state)
    {
        this.parent = transform;
        this.shipInventory = shipInventory;
        this.state = state;

        CreateItemSlot();
        CanvasUI.Inventory.AddDeviceSlot(slot);
    }

    protected override void DropItem()
    {
        if(this.state.Data.ItemKind != ItemKind.EmptyDevice)
        {
            shipInventory.TryDropItemFromShip(this.state);
        }
    }
}