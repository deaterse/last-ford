using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourcesRenderer : MonoBehaviour
{
    [SerializeField] private Tilemap _resourceTilemap;
    [SerializeField] private ResourcesVisualizer _resourcesVisualizer;

    public void Visualize(TerrainMap _terrainMap)
    {
        _resourcesVisualizer.Visualize(_terrainMap, _resourceTilemap);
    }
}
