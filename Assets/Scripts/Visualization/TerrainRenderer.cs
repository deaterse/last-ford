using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TerrainRenderer : MonoBehaviour
{
    [Header("Tilemap")]
    [SerializeField] private Tilemap _terrainTilemap;

    [Header("Visualizers Links")]
    [SerializeField] private FillTerrain _fillTerrain;
    [SerializeField] private HeightMapVisualizer _heightVisualizer;
    [SerializeField] private TerrainVisualizer _terrainVisualizer;

    public void GenerateTerrain(Vector2Int mapSize)
    {
        _fillTerrain.GenerateTerrain(mapSize, _terrainTilemap);
    }

    public void VisualizeHeightMap(HeightMap _heightMap)
    {
        _heightVisualizer.ColoringMap(_heightMap);
    }

    public void Visualize(TerrainMap _terrainMap)
    {
        _terrainVisualizer.Visualize(_terrainMap, _terrainTilemap);
    }
}
