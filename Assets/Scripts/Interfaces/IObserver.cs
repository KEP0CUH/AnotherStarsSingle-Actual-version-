public interface IObserver
{
    public void Invoke(EventType eventType,ItemKind kind, BaseItemState state);
}
