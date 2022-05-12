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

    public ItemShopState ItemShopState => itemShopState;
    public ItemShopView ItemShopView => itemShopView;

    public void Init(ItemShopType type, int id)
    {
        this.itemShopState = this.gameObject.GetComponent<ItemShopState>().Init(type,id);
        this.itemShopView = this.gameObject.GetComponent<ItemShopView>().Init(this,itemShopState);
    }
}
