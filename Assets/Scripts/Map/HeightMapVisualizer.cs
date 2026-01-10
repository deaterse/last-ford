using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class HeightMapVisualizer : MonoBehaviour
{
    [SerializeField] private List<TileBase> _heightTiles;

    public void ColoringMap(Tilemap _terrainTilemap, HeightMap heightMap)
    {
        // HeightColorMapper colMapper = new HeightColorMapper();
        
        // for(int x = 0; x < heightMap.Width; x++)
        // {  
        //     for(int y = 0; y < heightMap.Height; y++)
        //     {
        //         _terrainTilemap.SetTile(new Vector3Int(x, y, 0), null);

        //         int listNum = colMapper.GetColorInt(heightMap.HeightData[x, y]);

        //         _terrainTilemap.SetTile(new Vector3Int(x, y, 0), _heightTiles[listNum]);
        //     }
        // }
    }
}
