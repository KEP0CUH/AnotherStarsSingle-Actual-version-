
public interface IShipInventory
{
    public void TryInteractWithItem(ItemState state);
    public void TryInteractWithItemFromInventory(ItemState state, IPlayerInventory inventory);

    public void TryDropItemFromShip(ItemState state);
}
