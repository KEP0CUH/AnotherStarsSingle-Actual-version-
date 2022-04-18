using System.Collections.Generic;
using UnityEngine;

public class ShipState : MonoBehaviour
{
    [SerializeField] private ShipData data;
    [SerializeField] private ShipInventory inventory;

    private int maxNumGuns = 4;

    public ShipData Data => data;
    public ShipInventory Inventory => inventory;

    public ShipState Init(ShipKind kind)
    {
        this.data = Managers.Resources.DownloadData(kind);

        if(this.inventory != null)
        {
            this.inventory.RemoveAllItems();
        }

        switch(kind)
        {
            case ShipKind.GreenLinkor:
                maxNumGuns = 4;
                break;
            case ShipKind.GreenFrigate:
                maxNumGuns = 2;
                break;
        }
        inventory = new ShipInventory(maxNumGuns);

        return this;
    }

    public void SetGun(GunState gun)
    {
        this.inventory.AddItem(gun);
    }
    public void SetGun(GunState gun,IInventory inventory)
    {
        this.inventory.AddItem(gun,inventory);
    }

    public void SetGun(ItemKind gunKind)
    {
        inventory.AddItem(gunKind);
    }
}
