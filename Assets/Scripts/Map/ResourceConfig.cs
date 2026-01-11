using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ResourceConfig", menuName = "Scriptable Objects/Resource Config")]
public class ResourceConfig : ScriptableObject
{
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private List<TileBase> _resourceTiles;

    public ResourceType resourceType => _resourceType;
    public List<TileBase> resourceTiles => _resourceTiles;
}
