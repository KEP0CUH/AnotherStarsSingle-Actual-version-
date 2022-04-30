using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotShop : ItemSlot
{
    private ItemShop itemShop;
    public ItemState ItemState => state;
    public ItemSlotShop Init(ItemShop itemShop,Transform parent,ItemState state)
    {
        this.itemShop = itemShop;
        this.parent = parent;
        this.state = state;

        CreateItemSlot();
        return this;
    }

    protected override void DropItem()
    {
        Debug.Log("Предмет в магазине нельзя выбрасывать. Надо перегрузить метод создания");
        //Managers.Player.Controller.PlayerState.Ship.Inventory.TryDropItemFromShip(this.state);
    }

    protected override void TryInteract()
    {
        if (state.Data.ItemKind != ItemKind.deviceEmpty && state.Data.ItemKind != ItemKind.weaponEmpty)
        {
            Debug.Log("запрос на покупку итема");
            CreateBuyWindow();
        }
    }

    private void CreateBuyWindow()
    {
        var tradeWindow = new GameObject("Confirm buying.",typeof(RectTransform),typeof(Image));

        var rect = tradeWindow.GetComponent<RectTransform>();
        rect.transform.SetParent(this.itemShop.gameObject.transform);
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.offsetMin = new Vector2(-96, -120);
        rect.offsetMax = new Vector2(96, 70);

        tradeWindow.AddComponent<BuyWindow>().Init(itemShop,tradeWindow.transform, this);
    }
}
