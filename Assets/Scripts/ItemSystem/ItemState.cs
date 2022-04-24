using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemState : MonoBehaviour, Interactable
{
    private static int ID = 1;
    [SerializeField] protected ItemData data;
    [SerializeField] protected int count;
    [SerializeField] protected bool isSet;
    [SerializeField] protected int id;

    public ItemData Data => data;
    public int Count => count;
    public int Id => id;

    public bool IsSet => isSet;

    public bool IsItem => data.IsItem();
    public bool IsWeapon => data.IsWeapon();
    public bool IsDevice => data.IsDevice();

    public virtual void Init(ItemKind kind, int count)
    {
        this.data = Managers.Resources.DownloadData(kind);
        this.count = count;
        this.isSet = false;
        this.id = ItemState.GetId();
    }

    public virtual void Init(ItemKind kind,int count,int id)
    {
        this.data = Managers.Resources.DownloadData(kind);
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

    public void OnDrop()
    {
        Debug.Log("Item was dropped.");
    }

    public void OnPickup()
    {
        Debug.Log("Item was picked up.");
    }

    public void IncreaseNumber()
    {
        this.count++;
    }
    public void DecreaseNumber()
    {
        this.count--;
    }

    private static int GetId()
    {
        ID++;
        return ID;
    }
}
