using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TerrainRenderer : MonoBehaviour
{
    [SerializeField] private Tilemap _terrainMap;
    [SerializeField] private TilesConfig _tilesConfig;

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

                _terrainMap.SetTile(tilePosition, currentTile);
            }
        }
    }

    public void ColoringMap(HeightMap heightMap)
    {
        HeightColorMapper colMapper = new HeightColorMapper();

        Debug.Log(heightMap.Height);

        for(int x = 0; x < heightMap.Width; x++)
        {  
            for(int y = 0; y < heightMap.Height; y++)
            {
                GameObject currentTileObj = _terrainMap.GetInstantiatedObject(new Vector3Int(x, y, 0));
                SpriteRenderer currentRenderer = currentTileObj.GetComponent<SpriteRenderer>();

                currentRenderer.color = colMapper.GetColor(heightMap.HeightData[x, y]);
            }
        }
    }

    private TileBase RandomTile(List<TileBase> tileList)
    {
        return tileList[Random.Range(0, tileList.Count)];
    }

    public bool CanPlace(Vector3Int pos)
    {
        return _terrainMap.GetTile(pos);
    }
}
