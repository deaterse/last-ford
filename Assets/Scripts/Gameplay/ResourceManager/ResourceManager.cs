using UnityEngine;
using System.Collections.Generic;

public class ResourceManager: IService
{
    private Dictionary<ResourceType, int> _resources;

    public ResourceManager(StartResourcesConfig startResourcesConfig)
    {
        _resources = new Dictionary<ResourceType, int>();

        InitializeStartingResources(startResourcesConfig);
    }

    private void InitializeStartingResources(StartResourcesConfig config)
    {
        if (config.ResourcesStartTypes.Count != config.ResourcesStartValues.Count)
        {
            Debug.Log("Check your StartResourceConfig!");
            return;
        }
        
        foreach(ResourceType rt in config.ResourcesStartTypes)
        {
            int resourceIndex = config.ResourcesStartTypes.IndexOf(rt);

            AddResource(rt, config.ResourcesStartValues[resourceIndex]);

            ServiceLocator.GetService<EventBus>().Invoke<OnResourceChanged>(new OnResourceChanged(rt, _resources[rt]));
        }
    }

    public void AddResource(ResourceType type, int value)
    {
        if(_resources.ContainsKey(type))
        {
            _resources[type] += value;
        }
        else
        {
            _resources[type] = value;
        }

        ServiceLocator.GetService<EventBus>().Invoke<OnResourceChanged>(new OnResourceChanged(type, _resources[type]));
    }

    public bool TrySpendResource(ResourceType type, int value)
    {
        if(!_resources.ContainsKey(type) || _resources[type] < value)
        {
            Debug.Log($"Not enough resources.\n {type.ToString()}:{(_resources.ContainsKey(type) ? _resources[type] : 0)}/{value}");

            return false;
        }

        _resources[type] -= value;
        ServiceLocator.GetService<EventBus>().Invoke<OnResourceChanged>(new OnResourceChanged(type, _resources[type]));

        return true;
    }

    public bool IsResourceEnough(ResourceType type, int value)
    {
        if(!_resources.ContainsKey(type)) return false;

        if(_resources[type] >= value)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
