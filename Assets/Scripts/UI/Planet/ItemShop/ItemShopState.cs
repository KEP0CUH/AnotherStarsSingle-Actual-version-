///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;

public class ItemShopState : MonoBehaviour
{

    [SerializeField] 
    private             int                     id;
    [SerializeField] 
    private             ItemShopData            data;

    public              int                     Id => id;
    public              ItemShopData            Data => data;
    public              ItemShopState           Init(ItemShopType       type,
                                                     int                id)
    {
        this.id = id;
        this.data = Managers.Resources.DownloadData(type);

        return this;
    }

    public              void                    AddItem()
    {

    }

    public              void                    RemoveItem()
    {

    }

    public              void                    GetAllItems()
    {

    }
}
