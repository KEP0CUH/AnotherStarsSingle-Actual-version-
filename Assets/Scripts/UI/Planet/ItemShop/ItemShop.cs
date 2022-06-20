using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
        buttonClose.GetComponent<Button>().onClick.AddListener(() => itemShopView.CloseItemShop());
        itemShopView.ShowListItemShop();
        return this;
    }
}
