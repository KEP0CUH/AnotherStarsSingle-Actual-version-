public interface IInventory 
{
    //public void AddItem(ItemKind kind, ItemState state);
    //public ItemState GetItem(ItemKind kind);
    //public void RemoveItem(ItemKind kind);

    public void AddItem(ItemState state);
    public ItemState GetItem(int id);
    public void RemoveItem(ItemState state);

}
