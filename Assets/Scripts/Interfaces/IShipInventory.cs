
public interface IShipInventory 
{
    public void AddItem(GunState gun);
    public void AddItem(GunState gun, IInventory inventory);
    public void RemoveItem(GunState gun);
}
