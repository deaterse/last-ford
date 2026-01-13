using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

//ITS ALL RESOURCES

[CreateAssetMenu(fileName = "ResourcesSubtypeConfig", menuName = "Scriptable Objects/ResourcesSubtype Config")]
public class ResourcesSubtypeConfig : ScriptableObject
{
    [SerializeField] private List<ResourceTypesConfig> _resourceTypesConfig;

    public List<ResourceTypesConfig> TypesConfig => _resourceTypesConfig;

    public ResourceTypesConfig GetTypesFromResourceType(ResourceType _resourceType)
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
}
