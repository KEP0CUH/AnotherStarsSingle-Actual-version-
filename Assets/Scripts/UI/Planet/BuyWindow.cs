using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class BuyWindow : MonoBehaviour
{
    ItemShop itemShop;
    ItemSlotShop itemSlot;
    
    public void Init(ItemShop shop,Transform parent,ItemSlotShop itemSlot)
    {
        this.itemShop = shop;
        this.GetComponent<RectTransform>().SetParent(parent);
        this.itemSlot = itemSlot;

        CreateIconItem();
    }

    private void CreateIconItem()
    {
        var itemInfo = new GameObject("ItemIcon", typeof(RectTransform),typeof(Image));

        var rect = itemInfo.GetComponent<RectTransform>();
        rect.SetParent(this.gameObject.transform);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.offsetMin = new Vector2(-32, -32 + 48);
        rect.offsetMax = new Vector2(32, 32 + 48);

        var image = itemInfo.GetComponent<Image>();
        image.sprite = this.itemSlot.ItemState.Data.Icon;

        var textBuy = new GameObject("Buy?",typeof(RectTransform),typeof(Text));
        rect = textBuy.GetComponent<RectTransform>();
        rect.SetParent(this.gameObject.transform);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.offsetMin = new Vector2(-32, -96);
        rect.offsetMax = new Vector2(112, -32);

        var text = textBuy.GetComponent<Text>();
        Font font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        text.font = font;
        text.fontSize = 18;
        text.text = "йсохрэ?";
        text.color = new UnityEngine.Color(0,55,255,255) / 255;


        var buttonYes = new GameObject("Yes",typeof(RectTransform),typeof(Image), typeof(Button));
        rect = buttonYes.GetComponent<RectTransform>();
        rect.SetParent(this.gameObject.transform);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.offsetMin = new Vector2(-32, -96);
        rect.offsetMax = new Vector2(0, -64);

        buttonYes.GetComponent<Image>().color = new UnityEngine.Color(24, 171, 64, 255) / 256;
        buttonYes.GetComponent<Button>().onClick.AddListener(ConfirmBuying);

        var buttonNo = new GameObject("No",typeof(RectTransform),typeof(Image),typeof(Button));
        rect = buttonNo.GetComponent<RectTransform>();
        rect.SetParent(this.gameObject.transform);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.offsetMin = new Vector2(0, -96);
        rect.offsetMax = new Vector2(32, -64);

        buttonNo.GetComponent<Image>().color = new UnityEngine.Color(230, 59, 24, 255) / 256;
        buttonNo.GetComponent<Button>().onClick.AddListener(CancelBuying);
    }

    private void ConfirmBuying()
    {
        itemShop.RemoveItemState(this.itemSlot.ItemState);
        Managers.Player.Controller.Inventory.AddItem(this.itemSlot.ItemState);
        Object.Destroy(this.gameObject);
    }

    private void CancelBuying()
    {
        Object.Destroy(this.gameObject);
    }


}
