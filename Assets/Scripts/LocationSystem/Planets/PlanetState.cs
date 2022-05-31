using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemShopController))]
public class PlanetState : MonoBehaviour
{
    private static int ID = 1;
    [SerializeField] private int id;
    [SerializeField] private PlanetData data;
    private PlanetController controller;
    private ItemShopController itemShopController;
    private GameObject itemShop = null;

    public int Id => id;
    public PlanetData Data => data;
    public ItemShopController ItemShopController => itemShopController;

    public PlanetState Init(PlanetController controller,Planet kind)
    {
        this.id = GetId();
        this.controller = controller;
        this.data = Managers.Resources.DownloadData(kind);
        this.itemShopController = this.gameObject.GetComponent<ItemShopController>();
        itemShopController.Init(Data.ItemShopType, id);

        return this;
    }


    public void CreateItemShop()
    {
        if(itemShop == null)
        {
            itemShop = new GameObject("ItemShop", typeof(ItemShopController));
            itemShopController = itemShop.GetComponent<ItemShopController>();

            Managers.Canvas.AddModule(itemShop);

            var rect = itemShop.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.offsetMin = new Vector2(-250, -200);
            rect.offsetMax = new Vector2(250, 200);

            itemShop.GetComponent<ItemShopController>().Init(this.Data.ItemShopType, this.Id);
            itemShop.SetActive(false);
        }
    }

    public void SwitchItemShop()
    {
        if(itemShop != null)
        {
            if (itemShopController.gameObject.activeInHierarchy)
            {
                itemShopController.gameObject.SetActive(false);
            }
            else
            {
                itemShopController.View.ShowListItemShop();
                itemShopController.gameObject.SetActive(true);
            }
        }
    }


    private int GetId()
    {
        ID++;
        return ID;
    }
}
