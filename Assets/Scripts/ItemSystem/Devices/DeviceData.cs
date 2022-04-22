using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Device",fileName ="newDevice",order =54)]
public class DeviceData : BaseItemData
{
    public override bool IsDevice()
    {
        return true;
    }
}
