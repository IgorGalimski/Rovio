using System;
using System.Collections.Generic;

public static class MessageSystem
{
    private static Dictionary<Type, Delegate> _events = new();

    public static void AddListener<T>(Action<T> listener)
    {
        if (!_events.ContainsKey(typeof(T)))
            _events[typeof(T)] = null;

        _events[typeof(T)] = (Action<T>)_events[typeof(T)] + listener;
    }

    public static void RemoveListener<T>(Action<T> listener)
    {
        if (_events.ContainsKey(typeof(T)))
            _events[typeof(T)] = (Action<T>)_events[typeof(T)] - listener;
    }

    public static void SendMessage<T>(T message)
    {
        if (_events.ContainsKey(typeof(T)))
            (_events[typeof(T)] as Action<T>)?.Invoke(message);
    }
}