using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShopState : MonoBehaviour
{
    private static int ID = 1;

    private int id;
    private ItemShopData data;

    public int Id => id;
    public ItemShopData Data => data;

    public ItemShopState Init(ItemShopType type)
    {
        id = GetId();
        this.data = Managers.Resources.DownloadData(type);


        return this;
    }

    private static int GetId()
    {
        ID++;
        return ID;
    }
}
