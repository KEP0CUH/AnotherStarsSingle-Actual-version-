public interface IPlayerInventory 
{
    //public void AddItem(ItemKind kind, ItemState state);
    //public ItemState GetItem(ItemKind kind);
    //public void RemoveItem(ItemKind kind);

    public void AddItem(ItemState state,int count = 1, bool needDestroying = false);
    public ItemState GetItem(int id);
    public void RemoveItem(ItemState state, int count = 1, bool needDestroying = false);

}
