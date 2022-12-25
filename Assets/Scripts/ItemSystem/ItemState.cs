///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;

public class ItemState : MonoBehaviour, IInventoryHandler, IUsable
{
    private static      int             FREE_GLOBAL_ID = 1;
    [SerializeField]
    protected           int             count;
    [SerializeField]
    protected           bool            isSet;
    [SerializeField]
    protected           int             id;

    public              int             Count => count;
    public              int             Id => id;

    public              bool            IsSet => isSet;

    public              bool            IsItem => Data.IsItem();

    public              bool            IsWeapon => Data.IsWeapon();

    public              bool            IsDevice => Data.IsDevice();

    public              ItemData        Data { get; private set; }

    public virtual      ItemState       Init(ItemKind kind, int count)
    {
        this.Data = Managers.Resources.DownloadData(kind);
        this.count = count;
        this.isSet = false;
        this.id = ItemState.GetId();

        return this;
    }

    public virtual      ItemState       Init(ItemData data,int count)
    {
        this.Data = data;
        this.count = count;
        this.isSet = false;
        this.id = GetId();

        return this;
    }

    public virtual      ItemState       Init(ItemState state)
     {
        this.Data = Managers.Resources.DownloadData(state.Data.ItemKind);
        this.count = state.Count;
        this.isSet = state.IsSet;
        this.id = state.Id;

        return this;
     }

    public virtual      void            Init(ItemKind kind,int count,int id)
    {
        this.Data = Managers.Resources.DownloadData(kind);
        this.count = count;
        this.isSet = false;
        this.id = id;
    }

    public              void            OnPickup()
    {
        Debug.Log("Итем взят.");
        Object.Destroy(this.gameObject);
    }

    public              void            OnDrop()
    {
        Debug.Log("Итем выброшен.");
    }

    public              void            SetIsTrue()
    {
        isSet = true;
    }

    public              void            SetIsFalse()
    {
        isSet = false;
    }

    public virtual      void            Set()
    {
        Debug.Log("Это обычный предмет, установка/снятие на корабль невозможны.");
    }

    public virtual      void            Unset()
    {
        Debug.Log("Это обычный предмет, установка/снятие на корабль невозможны.");
    }

    public              bool            IsEmpty()
    {
        if (Data.ItemKind == ItemKind.EmptyDevice || Data.ItemKind == ItemKind.EmptyGun)
            return true;
        return false;
    }

    public              void            IncreaseCount(int num = 1)
    {
        this.count += num;
    }

    public              void            DecreaseCount(int num = 1)
    {
        this.count -= num;
    }

    private static      int             GetId()
    {
        FREE_GLOBAL_ID++;
        return FREE_GLOBAL_ID;
    }

}
