using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourcesRenderer : MonoBehaviour
{
    [SerializeField] private ResourcesVisualizer _resourcesVisualizer;

    public void Visualize(TerrainMap _terrainMap, ResourcesSubtypeConfig _resourcesSubtypeConfig)
    {
        _resourcesVisualizer.Visualize(_terrainMap, _resourcesSubtypeConfig);
    }
}
