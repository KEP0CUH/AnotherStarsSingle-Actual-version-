using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceState : ItemState, IUsable
{
    public override void Set()
    {
        Debug.Log("������� ����� ���������� �� �������.");
    }

    public override void Unset()
    {
        Debug.Log("������� ����� ���������� � �������");
    }

    public override ItemState Init(ItemKind kind, int count)
    {
        base.Init(kind, count);
        return this;
    }
}
