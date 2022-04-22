using System.Collections.Generic;
using UnityEngine;

public class ShipState : MonoBehaviour
{
    [SerializeField] private ShipData data;
    [SerializeField] private ShipInventory inventory;

    private int maxNumGuns = 4;
    private int maxNumDevices = 4;

    public ShipData Data => data;
    public ShipInventory Inventory => inventory;

    public ShipState Init(ShipKind kind)
    {
        this.data = Managers.Resources.DownloadData(kind);

        if(this.inventory != null)
        {
            this.inventory.RemoveAllEquipmentFromShip();
        }

        switch(kind)
        {
            case ShipKind.GreenLinkor:
                maxNumGuns = 4;
                maxNumDevices = 3;
                break;
            case ShipKind.GreenFrigate:
                maxNumGuns = 2;
                maxNumDevices = 3;
                break;
            case ShipKind.GreenKorvet:
                maxNumGuns = 1;
                maxNumDevices = 2;
                break;
        }
        inventory = new ShipInventory(maxNumGuns,maxNumDevices);

        return this;
    }

    public void SetGun(GunState gun)
    {
        this.inventory.TrySetGun(gun);
    }
    public void SetDevice(DeviceState device)
    {
        this.inventory.TrySetDevice(device);
    }
    public void SetGun(GunState gun,IInventory inventory)
    {
        this.inventory.TrySetGun(gun,inventory);
    }
    public void SetDevice(DeviceState device, IInventory inventory)
    {
        this.inventory.TrySetDevice(device,inventory);
    }
    public void SetGun(ItemKind gunKind)
    {
        inventory.TrySetGun(gunKind);
    }
    public void SetDevice(ItemKind deviceKind)
    {
        inventory.TrySetDevice(deviceKind);
    }


}
