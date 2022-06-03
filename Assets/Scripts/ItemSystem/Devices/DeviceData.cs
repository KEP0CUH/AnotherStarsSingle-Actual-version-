using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Items/Devices/newDevice",fileName ="newDevice",order =54)]
public class DeviceData : ItemData
{
    public override bool IsItem()
    {
        return false;
    }

    public override bool IsDevice()
    {
        return true;
    }
}
