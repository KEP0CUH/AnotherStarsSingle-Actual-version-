using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItemState : MonoBehaviour, Interactable
{
    [SerializeField] protected BaseItemData data;
    [SerializeField] protected int count;
    [SerializeField] protected bool isWeapon = false;

    public BaseItemData Data => data;
    public int Count => count;

    public bool IsWeapon => isWeapon;

    public virtual void Init(ItemKind kind, int count)
    {
        this.data = Managers.Resources.DownloadData(kind);
        this.count = count;
        this.isWeapon = false;
    }

    public virtual void Init(GunKind kind,int count)
    {
        this.data = Managers.Resources.DownloadData(kind);
        this.count = count;
        this.isWeapon = true;
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
