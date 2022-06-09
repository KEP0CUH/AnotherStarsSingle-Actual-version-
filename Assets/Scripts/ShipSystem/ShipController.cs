using UnityEngine;
public class ShipController : MonoBehaviour
{
    private ShipState shipState;

    public ShipState State => shipState;

    public ShipController Init(ShipKind kind,InventoryController inventory)
    {
        this.shipState = this.gameObject.AddComponent<ShipState>().Init(this,kind,inventory);
        
        return this;
    }

    public void AddEquipment(ItemState state,bool needDestroying = false)
    {
        this.State.Inventory.TryInteractWithItem(state);
    }

    public void RemoveEquipment(ItemState state, bool needDestroying = false)
    {
        this.State.Inventory.TryInteractWithItem(state);
    }

    public void TryInteractWithItem(ItemState state)
    {
        this.State.Inventory.TryInteractWithItem(state);
    }
}
