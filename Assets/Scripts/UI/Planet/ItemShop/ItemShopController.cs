using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(ItemShopState))]
[RequireComponent(typeof(ItemShopView))]
public class ItemShopController : MonoBehaviour
{
    private ItemShopState itemShopState;
    private ItemShopView itemShopView;

    public void Init(ItemShopType type)
    {
        this.itemShopState = this.gameObject.GetComponent<ItemShopState>().Init(type);
        this.itemShopView = this.gameObject.GetComponent<ItemShopView>().Init(itemShopState);
    }
}
