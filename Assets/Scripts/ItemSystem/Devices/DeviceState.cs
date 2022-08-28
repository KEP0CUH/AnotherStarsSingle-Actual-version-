using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceState : ItemState, IUsable
{
    public override void Set()
    {
        Debug.Log("Попытка одеть устройство на корабль.");
    }

    public override void Unset()
    {
        Debug.Log("Попытка снять устройство с корабля");
    }

    public override ItemState Init(ItemKind kind, int count)
    {
        base.Init(kind, count);
        return this;
    }
}
