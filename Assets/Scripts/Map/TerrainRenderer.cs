using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TerrainRenderer : MonoBehaviour
{
    [SerializeField] private Tilemap _terrainTilemap;

    [SerializeField] private FillTerrain _fillTerrain;
    [SerializeField] private HeightMapVisualizer _heightVisualizer;
    [SerializeField] private RiverVisualizer _riverVisualizer;

    public void GenerateTerrain(Vector2Int mapSize)
    {
        _fillTerrain.GenerateTerrain(mapSize, _terrainTilemap);
    }

    public void VisualizeHeightMap(HeightMap _heightMap)
    {
        _heightVisualizer.ColoringMap(_terrainTilemap, _heightMap);
    }

    public void VisualizeRiver(TerrainMap _terrainMap)
    {
        _riverVisualizer.VisualizeRiver(_terrainTilemap, _terrainMap);
    }
}
