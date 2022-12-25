///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;

public class PlanetState : MonoBehaviour
{
    private static          int                     FREE_GLOBAL_ID = 1;
    [SerializeField] 
    private                 int                     id;
    [SerializeField]
    private                 PlanetData              data;
    private                 PlanetController        controller;

    public                  int                     Id => id;
    public                  PlanetData              Data => data;

    public                  PlanetState             Init(PlanetController controller,Planet kind)
    {
        this.id = GetId();
        this.controller = controller;
        this.data = Managers.Resources.DownloadData(kind);
        //itemShopController.Init(Data.ItemShopType, id);

        return this;
    }


    public                  void                    CreateItemShop()
    {
        //itemShopController.OpenItemShop();

/*        if(itemShop == null)
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
            itemShop.SetActive(false);*/

            /* itemShop = Instantiate(Managers.Resources.DownloadData(ObjectType.ItemShop));
             Managers.Canvas.AddModule(itemShop);*/
        //}
    }

/*    public                void                    SwitchItemShop()
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
    }*/


    private                 int                     GetId()
    {
        FREE_GLOBAL_ID++;
        return FREE_GLOBAL_ID;
    }
}
