public interface IInventory 
{
    public void AddItem(ItemKind kind, int count);
    public BaseItemData GetItem(ItemKind kind);
    public void RemoveItem(ItemKind kind,int count);

}
