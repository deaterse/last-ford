using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TerrainRenderer : MonoBehaviour
{
    [Header("Tilemap")]
    [SerializeField] private Tilemap _terrainTilemap;

    [Header("Visualizers Links")]
    [SerializeField] private HeightMapVisualizer _heightVisualizer;
    [SerializeField] private FertilityMapVisualizer _fertilityVisualizer;
    [SerializeField] private TerrainVisualizer _terrainVisualizer;

    public void VisualizeHeightMap(HeightMap _heightMap)
    {
        _heightVisualizer.ColoringMap(_heightMap);
    }
    
    public void VisualizeFertilityMap(FertilityMap _fertilityMap)
    {
        _fertilityVisualizer.ColoringMap(_fertilityMap);
    }

    public void Visualize(TerrainMap _terrainMap)
    {
        _terrainVisualizer.Visualize(_terrainMap, _terrainTilemap);
    }

    public void CleanTerrainTilemap()
    {
        _terrainTilemap.ClearAllTiles();
    }
}
