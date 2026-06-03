using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ResourceVisualizeConfig", menuName = "Scriptable Objects/GenerationSystem/Resource Visualize Config")]
public class ResourceVisualizeConfig : ScriptableObject
{
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private List<VisualResourceType> _resourceVisualTypes;

    public ResourceType resourceType => _resourceType;
    public List<VisualResourceType> resourceVisualTypes => _resourceVisualTypes;
}
