using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotShop : ItemSlot
{
    public ItemSlotShop Init(Transform parent,ItemState state)
    {
        this.parent = transform;
        this.state = state;

        CreateItemSlot();
        return this;
    }

    protected override void DropItem()
    {
        Debug.Log("Предмет в магазине нельзя выбрасывать. Надо перезагрузить метод создания");
        //Managers.Player.Controller.PlayerState.Ship.Inventory.TryDropItemFromShip(this.state);
    }

    protected override void TryInteract()
    {
        if (state.Data.ItemKind != ItemKind.deviceEmpty && state.Data.ItemKind != ItemKind.weaponEmpty)
        {
            Debug.Log("запрос на покупку итема");
        }
    }
}
