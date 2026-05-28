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
        if(_heightMap != null)
        {
            _heightVisualizer.ColoringMap(_heightMap);
        }
    }
    
    public void VisualizeFertilityMap(FertilityMap _fertilityMap)
    {
        if(_fertilityMap != null)
        {
            _fertilityVisualizer.ColoringMap(_fertilityMap);
        }
    }

    public void Visualize(TerrainMap _terrainMap, MapGenerateConfig _mapGenerateConfig, HeightMap _heightMap, FertilityMap _fertilityMap)
    {
        CleanTerrainTilemap();
        
        _terrainVisualizer.Visualize(_terrainMap, _terrainTilemap);
        if(_mapGenerateConfig.GenerateHeight)
        {
           VisualizeHeightMap(_heightMap);
        }
        if(_mapGenerateConfig.GenerateFertility)
        {
            VisualizeFertilityMap(_fertilityMap);
        }
    }

    public void CleanTerrainTilemap()
    {
        _terrainTilemap.ClearAllTiles();
    }
}
