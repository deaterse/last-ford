using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class FillTerrain : MonoBehaviour
{
    [SerializeField] private TilesConfig _tilesConfig;

    public void GenerateTerrain(Vector2Int mapSize, Tilemap terrainTilemap)
    {
        for(int x = 0; x < mapSize.x; x++)
        {  
            for(int y = 0; y < mapSize.y; y++)
            {
                TileBase currentTile = RandomTile(_tilesConfig._grassTiles);
                Vector3Int tilePosition = new Vector3Int(x, y, 0);

                terrainTilemap.SetTile(tilePosition, currentTile);
            }
        }
    }

    private TileBase RandomTile(List<TileBase> tileList)
    {
        return tileList[Random.Range(0, tileList.Count)];
    }
}
