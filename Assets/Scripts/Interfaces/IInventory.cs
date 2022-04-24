public interface IInventory 
{
    public void AddItem(ItemKind kind, ItemState state);
    public ItemState GetItem(ItemKind kind);
    public void RemoveItem(ItemKind kind);

}
