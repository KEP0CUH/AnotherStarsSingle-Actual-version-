///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;

public class DeviceState : ItemState, IUsable
{
    public override         void            Set()
    {
        Debug.Log("������� ����� ���������� �� �������.");
    }
    public override         void            Unset()
    {
        Debug.Log("������� ����� ���������� � �������");
    }

    public override         ItemState       Init(ItemKind kind, int count)
    {
        base.Init(kind, count);
        return this;
    }
}
