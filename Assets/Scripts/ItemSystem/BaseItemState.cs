using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItemState : MonoBehaviour, Interactable
{
    [SerializeField] private BaseItemData data;
    [SerializeField] protected int count;

    public BaseItemData Data => data;
    public int Count => count;

    public void Init(ItemKind kind, int count)
    {
        this.data = Managers.Resources.DownloadData(kind);
        this.count = count;
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

    public virtual bool IsWeapon()
    {
        return false;
    }
}
