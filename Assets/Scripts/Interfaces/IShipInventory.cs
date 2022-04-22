
public interface IShipInventory
{
    public void TrySetGun(GunState gun);
    public void TrySetGun(GunState gun, IInventory inventory);
    public void TryUnsetGun(GunState gun);

    public void TrySetDevice(DeviceState device);
    public void TrySetDevice(DeviceState device, IInventory inventory);
    public void TryUnsetDevice(DeviceState device);
}
