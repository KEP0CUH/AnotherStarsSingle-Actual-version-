using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShopState : MonoBehaviour
{

    [SerializeField] private int id;
    [SerializeField] private ItemShopData data;

    public int Id => id;
    public ItemShopData Data => data;
    public ItemShopState Init(ItemShopType type, int id)
    {
        this.id = id;
        this.data = Managers.Resources.DownloadData(type);


        return this;
    }
}
