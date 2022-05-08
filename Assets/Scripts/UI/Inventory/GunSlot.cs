using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Selectable))]
public class GunSlot : ItemSlot
{
    private IShipInventory shipInventory;

    public void Init(Transform transform, IShipInventory shipInventory, ItemState state)
    {
        this.parent = transform;
        this.shipInventory = shipInventory;
        this.state = state;

        CreateItemSlot();
        CanvasUI.Inventory.AddGunSlot(slot);
    }

    protected override void DropItem()
    {
        if(this.state.Data.ItemKind != ItemKind.EmptyGun)
        {
            shipInventory.TryDropItemFromShip(this.state);
        }
    }
}
