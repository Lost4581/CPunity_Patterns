public interface IObserver
{
    void OnNotify(string eventType, object data);
}