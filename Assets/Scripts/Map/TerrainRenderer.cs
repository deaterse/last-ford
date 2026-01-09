using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TerrainRenderer : MonoBehaviour
{
    [SerializeField] private TilesConfig _tilesConfig;
    [SerializeField] private Tilemap _terrainTilemap;

    [SerializeField] private HeightMapVisualizer _heightVisualizer;
    [SerializeField] private RiverVisualizer _riverVisualizer;

    private Vector2Int _currentMapSize;

    public void GenerateTerrain(Vector2Int mapSize)
    {
        _currentMapSize = mapSize;

        for(int x = 0; x < mapSize.x; x++)
        {  
            for(int y = 0; y < mapSize.y; y++)
            {
                TileBase currentTile = RandomTile(_tilesConfig._grassTiles);
                Vector3Int tilePosition = new Vector3Int(x, y, 0);

                _terrainTilemap.SetTile(tilePosition, currentTile);
            }
        }
    }

    public void VisualizeHeightMap(HeightMap _heightMap)
    {
        _heightVisualizer.ColoringMap(_terrainTilemap, _heightMap);
    }

    public void VisualizeRiver(TerrainMap _terrainMap)
    {
        _riverVisualizer.VisualizeRiver(_terrainTilemap, _terrainMap);
    }

    private TileBase RandomTile(List<TileBase> tileList)
    {
        return tileList[Random.Range(0, tileList.Count)];
    }
}
