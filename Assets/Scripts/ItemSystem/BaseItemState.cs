using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItemState : MonoBehaviour, Interactable
{
    [SerializeField] private BaseItemData data;
    [SerializeField] private int count;

    public BaseItemData Data => data;
    public int Count => count;

    public void Init(BaseItemData data, int count)
    {
        this.data = data;
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
}
