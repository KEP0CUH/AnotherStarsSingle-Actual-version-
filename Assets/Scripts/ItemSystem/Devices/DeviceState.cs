using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceState : BaseItemState
{
    public override void Init(ItemKind kind, int count)
    {
        this.data = Managers.Resources.DownloadData(kind);
        this.count = count;
    }
}
