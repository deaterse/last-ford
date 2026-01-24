using UnityEngine;
using System;
using System.Collections.Generic;

public class EventBus
{
    private Dictionary<string, List<object>> _allCallbacks;

    public void Subscribe<T>(Action<T> callback) where T : ISignal
    {
        string name = typeof(T).Name;

        if(_allCallbacks.ContainsKey(name))
        {
            _allCallbacks[name].Add(callback);
        }
        else
        {
            _allCallbacks[name] = new List<object> { callback };
        }
    }

    public void Unsubscribe<T>(Action<T> callback) where T : ISignal
    {
        string name = typeof(T).Name;

        if(_allCallbacks.ContainsKey(name))
        {
            _allCallbacks[name].Remove(callback);
        }
    }

    public void Invoke<T>(T signal) where T : ISignal
    {
        string name = typeof(T).Name;

        if(_allCallbacks.ContainsKey(name))
        {
            foreach(object obj in _allCallbacks[name])
            {
                var callback = obj as Action<T>;
                callback?.Invoke(signal);
            }
        }
    }
}
