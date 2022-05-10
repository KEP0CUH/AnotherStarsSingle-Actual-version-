using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotShop : ItemSlot
{
    private ItemShop itemShop;
    private bool forBuy;
    public ItemState ItemState => state;

    public ItemSlotShop Init(ItemShop itemShop,Transform parent,ItemState state,bool forBuy)
    {
        this.itemShop = itemShop;
        this.parent = parent;
        this.state = state;
        this.forBuy = forBuy;

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
        if (state.Data.ItemKind != ItemKind.EmptyDevice && state.Data.ItemKind != ItemKind.EmptyGun)
        {
            Debug.Log("запрос на покупку / продажу итема");
            if(forBuy)
            {
                CreateBuyWindow();
            }
            else if(forBuy == false)
            {
                CreateSellWindow();
            }

        }
    }

    private void CreateBuyWindow()
    {
        var tradeWindowPrefab = Managers.Resources.DownloadData(ObjectType.ConfirmBuying);
        var tradeWindow = Instantiate(tradeWindowPrefab,this.transform);

        var rect = tradeWindow.GetComponent<RectTransform>();
        rect.transform.SetParent(this.itemShop.gameObject.transform);
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.offsetMin = new Vector2(-96, -120);
        rect.offsetMax = new Vector2(96, 70);

        tradeWindow.GetComponent<BuyWindow>().Init(itemShop,tradeWindow.transform, this);
    }

    private void CreateSellWindow()
    {
        var tradeWindowPrefab = Managers.Resources.DownloadData(ObjectType.ConfirmSelling);
        var tradeWindow = Instantiate(tradeWindowPrefab,this.transform);

        var rect = tradeWindow.GetComponent<RectTransform>();
        rect.transform.SetParent(this.itemShop.gameObject.transform);
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.offsetMin = new Vector2(-96, -120);
        rect.offsetMax = new Vector2(96, 70);

        tradeWindow.AddComponent<SellWindow>().Init(itemShop, tradeWindow.transform, this);
    }
}
