
public interface IShipInventory
{
    public void TryInteractWithItem(ItemState state);
    public void TryInteractWithItemFromInventory(ItemState state, IInventory inventory);
}
