using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour, ISubject
{
    private List<IObserver> _observers = new List<IObserver>();

    public static GameEventManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddObserver(IObserver observer)
    {
        if (!_observers.Contains(observer))
            _observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify(string eventType, object data)
    {
        Debug.Log($"[Event] {eventType}: {data}");
        foreach (var observer in _observers)
        {
            observer.OnNotify(eventType, data);
        }
    }
}