///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(ItemShopState))]
[RequireComponent(typeof(ItemShopView))]
public class ItemShopController : MonoBehaviour
{
    private             ItemShopState           itemShopState;
    private             ItemShopView            itemShopView;

    public              ItemShopState           State => itemShopState;
    public              ItemShopView            View => itemShopView;

    public              ItemShopController      Init(ItemShopType       type,
                                                     int                id)
    {
        if(type != ItemShopType.ShopEmpty)
        {
            this.itemShopState = this.gameObject.GetComponent<ItemShopState>().Init(type, id);
            this.itemShopView = this.gameObject.GetComponent<ItemShopView>().Init(this);

            return this;
        }
        else
        {
            return null;
        }
    }

    public              void                    SwitchItemShop()
    {
        if(this.View.ShopIsOpen)
        {
            this.View.CloseItemShop();
        }
        else
        {
            this.View.OpenItemShop();
        }
    }

    public              void                    OpenItemShop()
    {
        this.View.OpenItemShop();
    }

    public              void                    CloseItemShop()
    {
        this.View.CloseItemShop();
    }
}
