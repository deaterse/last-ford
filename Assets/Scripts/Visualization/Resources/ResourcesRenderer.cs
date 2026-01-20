using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourcesRenderer : MonoBehaviour
{
    [SerializeField] private Tilemap _resourceTilemap;
    [SerializeField] private ResourcesVisualizer _resourcesVisualizer;

    public void Visualize(TerrainMap _terrainMap, ResourcesSubtypeConfig _resourcesSubtypeConfig)
    {
        _resourcesVisualizer.Visualize(_terrainMap, _resourceTilemap, _resourcesSubtypeConfig);
    }

    public void CleanResourcesTilemap()
    {
        _resourceTilemap.ClearAllTiles(); 
    }
}
