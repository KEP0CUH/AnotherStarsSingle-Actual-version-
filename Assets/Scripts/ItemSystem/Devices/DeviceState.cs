using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceState : ItemState
{
    public override ItemState Init(ItemKind kind, int count)
    {
        base.Init(kind, count);
        return this;
    }
}
