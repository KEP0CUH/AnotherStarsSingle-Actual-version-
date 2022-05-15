using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(ScrollRect))]
public class ItemShop : MonoBehaviour
{
    private ItemShopView itemShopView;

    [SerializeField] private GameObject listShopItems;
    [SerializeField] private GameObject listPlayerItems;
    [SerializeField] private GameObject buttonClose;

    public ItemShopView ItemShopView => itemShopView;
    public GameObject ListShopItems => listShopItems;
    public GameObject ListPlayerItems => listPlayerItems;

    public ItemShop Init(ItemShopView itemShopView)
    {
        this.itemShopView = itemShopView;
        var scroll = GetComponent<ScrollRect>();
        scroll.horizontal = true;
        scroll.vertical = false;
        scroll.scrollSensitivity = 15;
        buttonClose.GetComponent<Button>().onClick.AddListener(() => itemShopView.controller.gameObject.SetActive(false));
        itemShopView.ShowListItemShop();
        return this;
    }
}
