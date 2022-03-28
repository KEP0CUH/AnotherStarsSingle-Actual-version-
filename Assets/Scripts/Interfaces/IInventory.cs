public interface IInventory 
{
    public void AddItem(ItemKind kind, BaseItemState state);
    public BaseItemState GetItem(ItemKind kind);
    public void RemoveItem(ItemKind kind);

}
