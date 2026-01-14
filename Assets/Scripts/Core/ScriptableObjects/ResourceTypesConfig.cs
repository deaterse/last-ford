using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "ResourceTypesConfig", menuName = "Scriptable Objects/ResourceTypes Config")]
public class ResourceTypesConfig: ScriptableObject
{
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private List<VisualResourceType> _resourceTypes;

    public ResourceType ResourceType => _resourceType;
    public List<VisualResourceType> ResourceTypes => _resourceTypes;

    public List<TileBase> GetTilesByIndex(int index)
    {
        if(index < _resourceTypes.Count)
        {
            return _resourceTypes[index].ResourceTiles;
        }
        else if(_resourceTypes.Count != 0)
        {
            return _resourceTypes[0].ResourceTiles;
        }
        else
        {
            Debug.LogWarning($"ResourceTypesConfig [{_resourceType.ToString()}] is empty!");

            return null;
        }
    }

    public int TypesCount()
    {
        return _resourceTypes.Count;
    }
}
