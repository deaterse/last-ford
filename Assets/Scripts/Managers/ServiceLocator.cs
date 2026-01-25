using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    private static Dictionary<string, object> _allServices = new();

    public static void ProvideService<T>(T service) where T : IService
    {
        string serviceName = typeof(T).Name;

        if (_allServices.ContainsKey(serviceName))
        {
            Debug.Log($"Overwriting {serviceName}.");
        }

        _allServices[serviceName] = service;
    }
    
    public static T GetService<T>() where T : IService
    {
        string serviceName = typeof(T).Name;

        if(_allServices.ContainsKey(serviceName))
        {
            return (T) _allServices[serviceName];
        }

        Debug.LogWarning($"{serviceName} not founded!");
        return default;
    }
}