
public interface IShipInventory
{
    public void TryInteractWithItem(ItemState state);
    public void TryDropItemFromShip(ItemState state);
}
