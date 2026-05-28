using UnityEngine;

public class Visualizer : MonoBehaviour
{
    [SerializeField] private TerrainRenderer _terrainRenderer;
    [SerializeField] private ResourcesRenderer _resourcesRenderer;

    public void VisualizeEverything(TerrainMap _terrainMap, MapGenerateConfig _mapGenerateConfig, HeightMap _heightMap, FertilityMap _fertilityMap, ResourcesSubtypeConfig _resourcesSubtypeConfig)
    {
        _terrainRenderer.Visualize(_terrainMap, _mapGenerateConfig, _heightMap, _fertilityMap);
        _resourcesRenderer.Visualize(_terrainMap, _resourcesSubtypeConfig);
    }
}
