using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class ForestVisualizer : MonoBehaviour
{
    [SerializeField] private List<TileBase> _forestTiles;

    public void VisualizeForests(TerrainMap _terrainMap, Tilemap _resourceTilemap)
    {
        for(int x = 0; x < _terrainMap.Width; x++)
        {
            for(int y = 0; y < _terrainMap.Width; y++)
            {
                if(_terrainMap.TerrainData[x, y].Resource.Type == ResourceType.Wood)
                {
                    _resourceTilemap.SetTile(new Vector3Int(x, y, 0), RandomTile(_forestTiles));
                }
            }
        }
    }

    private TileBase RandomTile(List<TileBase> tileList)
    {
        return tileList[Random.Range(0, tileList.Count)];
    }

}
