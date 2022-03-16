public interface IInventory 
{
    public void AddItem();
    public BaseItemData GetItem(ItemKind kind);
    public void RemoveItem();

    
}
