
public interface IShipInventory 
{
    public void TrySetGun(GunState gun);
    public void TrySetGun(GunState gun, IInventory inventory);
    public void TryUnsetGun(GunState gun);
}
