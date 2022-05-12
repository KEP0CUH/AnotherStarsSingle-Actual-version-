using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetState : MonoBehaviour
{
    private static int ID = 1;
    [SerializeField] private int id;
    [SerializeField] private PlanetData data;
    private PlanetController planetController;
    private ItemShopController itemShopController;

    public int Id => id;
    public PlanetData Data => data;
    public ItemShopController ItemShopController => itemShopController;

    public PlanetState Init(PlanetController controller,Planet kind)
    {
        this.id = GetId();
        this.planetController = controller;
        this.data = Managers.Resources.DownloadData(kind);
        this.CreateItemShop();

        return this;
    }

    public void SwitchItemShop()
    {
        if(itemShopController.gameObject.activeInHierarchy)
        {
            itemShopController.gameObject.SetActive(false);
        }
        else
        {
            itemShopController.ItemShopView.ShowListItemShop();
            itemShopController.gameObject.SetActive(true);
        }
    }

    private int GetId()
    {
        ID++;
        return ID;
    }

    private void CreateItemShop()
    {
        var itemShop = new GameObject("ItemShop", typeof(ItemShopController));
        itemShopController = itemShop.GetComponent<ItemShopController>();

        var rect = itemShop.GetComponent<RectTransform>();
        Managers.Canvas.AddModule(itemShop);
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.offsetMin = new Vector2(-250, -200);
        rect.offsetMax = new Vector2(250, 200);

        itemShop.GetComponent<ItemShopController>().Init(this.Data.ItemShopType, this.Id);
        itemShop.SetActive(false);
    }
}
