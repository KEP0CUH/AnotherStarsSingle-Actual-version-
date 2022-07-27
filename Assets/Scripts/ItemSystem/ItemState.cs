using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemState : MonoBehaviour
{
    private static int ID = 1;
    [SerializeField] public virtual ItemData Data { get; private set; }
    [SerializeField] protected int count;
    [SerializeField] protected bool isSet;
    [SerializeField] protected int id;

    public int Count => count;
    public int Id => id;

    public bool IsSet => isSet;

    public bool IsItem => Data.IsItem();
    public bool IsWeapon => Data.IsWeapon();
    public bool IsDevice => Data.IsDevice();

    public virtual ItemState Init(ItemKind kind, int count)
    {
        this.Data = Managers.Resources.DownloadData(kind);
        this.count = count;
        this.isSet = false;
        this.id = ItemState.GetId();

        return this;
    }

    public virtual ItemState Init(ItemData data,int count)
    {
        this.Data = data;
        this.count = count;
        this.isSet = false;
        this.id = GetId();

        return this;
    }

     public virtual ItemState Init(ItemState state)
    {
        this.Data = Managers.Resources.DownloadData(state.Data.ItemKind);
        this.count = state.Count;
        this.isSet = state.IsSet;
        this.id = state.Id;

        return this;
    }

    public virtual void Init(ItemKind kind,int count,int id)
    {
        this.Data = Managers.Resources.DownloadData(kind);
        this.count = count;
        this.isSet = false;
        this.id = id;
    }

    public void SetIsTrue()
    {
        isSet = true;
    }
    public void SetIsFalse()
    {
        isSet = false;
    }

    public bool IsEmpty()
    {
        if (Data.ItemKind == ItemKind.EmptyDevice || Data.ItemKind == ItemKind.EmptyGun)
            return true;
        return false;
    }

    public void IncreaseNumber(int num = 1)
    {
        this.count += num;
    }
    public void DecreaseNumber(int num = 1)
    {
        this.count -= num;
    }

    private static int GetId()
    {
        ID++;
        return ID;
    }

     
}
