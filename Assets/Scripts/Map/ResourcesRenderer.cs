using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourcesRenderer : MonoBehaviour
{
    [SerializeField] private Tilemap _resourceTilemap;

    [SerializeField] private ForestVisualizer _forestVisualizer;

    public void VisualizeForests(TerrainMap _terrainMap)
    {
        _forestVisualizer.VisualizeForests(_terrainMap ,_resourceTilemap);
    }
}
