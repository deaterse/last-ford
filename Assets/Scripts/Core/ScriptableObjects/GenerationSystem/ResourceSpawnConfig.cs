using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "AllResourceSpawnConfig", menuName = "Scriptable Objects/AllResourcesSpawn Config")]
public class ResourceSpawnConfig : ScriptableObject
{
    [SerializeField] private List<ResourceSettings> _allResourcesSettings = new List<ResourceSettings>();

    public ResourceSettings GetResourceSettings(ResourceType resourceType)
    {
        return _allResourcesSettings.FirstOrDefault(rs => rs.resourceType == resourceType);
    }
}