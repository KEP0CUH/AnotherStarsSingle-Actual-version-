public interface IObservable
{
    public void AddObserver(IObserver observer,EventType eventType);
    public void RemoveObserver(IObserver observer);
}
