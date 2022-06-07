using UnityEngine;
public class ShipController : MonoBehaviour
{
    private ShipState shipState;
    private ShipInventory shipInventory;

    public ShipState State => shipState;
    public ShipInventory Inventory => shipInventory;

    public ShipController Init(ShipKind kind)
    {
        this.shipState = this.gameObject.AddComponent<ShipState>().Init(this,kind);
        this.shipInventory = this.State.Inventory;
        
        return this;
    }

    public void AddEquipment(ItemState state,bool needDestroying = false)
    {
        this.Inventory.TryInteractWithItem(state);
    }

    public void RemoveEquipment(ItemState state, bool needDestroying = false)
    {
        this.Inventory.TryInteractWithItem(state);
    }

    public void TryInteractWithItem(ItemState state)
    {
        this.Inventory.TryInteractWithItem(state);
    }
}
