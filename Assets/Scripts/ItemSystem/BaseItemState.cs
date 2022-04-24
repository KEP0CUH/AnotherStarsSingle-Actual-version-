using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItemState : MonoBehaviour, Interactable
{
    [SerializeField] protected BaseItemData data;
    [SerializeField] protected int count;
    [SerializeField] protected bool isSet;

    public BaseItemData Data => data;
    public int Count => count;

    public bool IsSet => isSet;

    public bool IsWeapon => data.IsWeapon();
    public bool IsDevice => data.IsDevice();

    public virtual void Init(ItemKind kind, int count)
    {
        this.data = Managers.Resources.DownloadData(kind);
        this.count = count;
        this.isSet = true;
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
}
