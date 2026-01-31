using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AllResourcesVisualizationConfig", menuName = "Scriptable Objects/Resource System/AllResourceVisual Config")]
public class AllResourcesVisualizationConfig : ScriptableObject
{
    [SerializeField] private List<ResourceVisualizationConfig> _allResourcesVisConfigs = new();

    public List<ResourceVisualizationConfig> AllResourcesVisConfigs => _allResourcesVisConfigs;
}
