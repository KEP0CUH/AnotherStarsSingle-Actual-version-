using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItemState : MonoBehaviour
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
}
