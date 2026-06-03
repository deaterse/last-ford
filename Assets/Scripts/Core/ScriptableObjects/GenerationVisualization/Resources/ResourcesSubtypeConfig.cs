using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ResourcesSubtypeConfig", menuName = "Scriptable Objects/GenerationSystem/ResourcesSubtype Config")]
public class ResourcesSubtypeConfig : ScriptableObject
{
    [SerializeField] private List<ResourceTypesConfig> _resourceTypesConfig;

    public List<ResourceTypesConfig> TypesConfig => _resourceTypesConfig;

    public ResourceTypesConfig GetTypeFromResourceType(ResourceType _resourceType)
    {
        foreach(ResourceTypesConfig rtc in _resourceTypesConfig)
        {
            if(rtc.ResourceType == _resourceType)
            {
                return rtc;
            }
        }

        Debug.LogWarning($"Didnt found any resource {_resourceType.ToString()}");
        return null;
    }

    public int GetCountFromResourceType(ResourceType _resourceType)
    {
        foreach(ResourceTypesConfig rtc in _resourceTypesConfig)
        {
            if(rtc.ResourceType == _resourceType)
            {
                return rtc.TypesCount();
            }
        }

        Debug.LogWarning($"Didnt found any resource {_resourceType.ToString()}");
        return 0;
    }

    public ResourceTypesConfig GetConfigByType(ResourceType resourceType)
    {
        foreach(ResourceTypesConfig rtc in _resourceTypesConfig)
        {
            if(rtc.ResourceType == resourceType)
            {
                return rtc;
            }
        }

        Debug.LogWarning($"Config not found for resource type: {resourceType.ToString()}");
        return null;
    }
}
