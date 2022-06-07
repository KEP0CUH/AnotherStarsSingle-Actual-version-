using System.Collections.Generic;
using UnityEngine;

public class ShipState : MonoBehaviour
{
    [SerializeField] private ShipData data;
    [SerializeField] private ShipInventory inventory;

    public ShipData Data => data;
    public ShipInventory Inventory => inventory;

    public ShipState Init(ShipKind kind)
    {
        this.data = Managers.Resources.DownloadData(kind);
        inventory = new ShipInventory(this);

        return this;
    }

    public void TryInteractWithItem(ItemState state)
    {
        this.inventory.TryInteractWithItem(state);
    }

    public void TryInteractWithItemFromInventory(ItemState state,IPlayerInventory inventory)
    {
        this.inventory.TryInteractWithItemFromInventory(state, inventory);
    }


}
