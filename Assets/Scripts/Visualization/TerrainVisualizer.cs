using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class TerrainVisualizer : MonoBehaviour
{
    [SerializeField] private List<TerrainConfig> _terrainConfigs;

    public void Visualize(TerrainMap _terrainMap, Tilemap _terrainTilemap)
    {
        int width = _terrainMap.Width;
        int height = _terrainMap.Height;

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                TerrainType currentTerrainType = _terrainMap.TerrainData[x, y].Type;
                foreach(TerrainConfig tc in _terrainConfigs)
                {
                    if(tc.terrainType == currentTerrainType)
                    {
                        _terrainTilemap.SetTile(new Vector3Int(x, y, 0), null);
                        _terrainTilemap.SetTile(new Vector3Int(x, y, 0), RandomTile(tc.terrainTiles));
                    }
                }
            }
        }
    }

    private TileBase RandomTile(List<TileBase> tileList)
    {
        return tileList[Random.Range(0, tileList.Count)];
    }
}
