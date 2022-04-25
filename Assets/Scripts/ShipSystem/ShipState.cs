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
        inventory = new ShipInventory(this,maxNumGuns,maxNumDevices);

        return this;
    }

    public void TryInteractWithItem(ItemState state)
    {
        this.inventory.TryInteractWithItem(state);
    }

    public void TryInteractWithItemFromInventory(ItemState state,IInventory inventory)
    {
        this.inventory.TryInteractWithItemFromInventory(state, inventory);
    }


}
