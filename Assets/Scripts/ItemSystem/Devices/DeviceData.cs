using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Items/Devices/newDevice",fileName ="newDevice",order =54)]
public class DeviceData : ItemData
{
    [SerializeField] private int energyCost;
    [SerializeField] private float cooldown;
    public int EnergyCost => energyCost;

    protected override void OnValidate()
    {
        base.OnValidate();

        switch (ItemKind)
        {
            case ItemKind.TourbineDevice:
                energyCost = 25;
                cooldown = 65.0f;
                break;
        }
    }

    public override bool IsItem()
    {
        return false;
    }

    public override bool IsDevice()
    {
        return true;
    }
}
