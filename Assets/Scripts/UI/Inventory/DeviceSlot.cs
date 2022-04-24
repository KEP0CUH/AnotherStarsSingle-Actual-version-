using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Selectable))]
public class DeviceSlot : ItemSlot
{
    private IShipInventory inventory;

    public void Init(Transform transform, IShipInventory shipInventory, BaseItemState state)
    {
        this.parent = transform;
        this.inventory = shipInventory;
        this.state = state;

        CreateItemSlot();
        CanvasUI.Inventory.AddDeviceSlot(slot);
    }

    [ContextMenu("Set Device")]
    private void SetDevice()
    {
        Debug.Log($"{this.state.IsDevice}");
        if (this.state.IsDevice)
        {
            var deviceState = (DeviceState)this.state;
            Managers.Player.Controller.PlayerState.Ship.SetDevice(deviceState);
        }

    }

    [ContextMenu("DropItem")]
    private void DropItem()
    {
        if (state.Data.ItemKind != ItemKind.deviceEmpty)
        {
            inventory.TryUnsetDevice((DeviceState)state);
            var item = new GameObject("Item" + state.Data.Title, typeof(ItemViewGame));
            item.GetComponent<ItemViewGame>().Init(((DeviceState)state).Data.ItemKind, 1);
        }


        //Destroy(this.gameObject);
    }

    protected override void TryInteract()
    {
        if (this.state.Data.ItemKind != ItemKind.deviceEmpty)
        {
            Debug.Log("Interact with device finished.");
            if (this.state.IsSet)
            {
                inventory.TryUnsetDevice((DeviceState)this.state);
            }
            else if (this.state.IsSet == false)
            {
                this.inventory.TrySetDevice((DeviceState)this.state);
            }
        }
    }
}