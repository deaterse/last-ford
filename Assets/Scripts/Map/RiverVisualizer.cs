using UnityEngine;
using UnityEngine.Tilemaps;

public class RiverVisualizer : MonoBehaviour
{
    [SerializeField] private TileBase _waterTile;

    public void VisualizeRiver(Tilemap _terrainTilemap, TerrainMap _terrainMap)
    {
        for(int x = 0; x < _terrainMap.Width; x++)
        {
            for(int y = 0; y < _terrainMap.Height; y++)
            {
                if(_terrainMap.TerrainData[x, y].Type == TerrainType.Water)
                {
                    _terrainTilemap.SetTile(new Vector3Int(x, y, 0), _waterTile);
                }
            }
        }
    }
}
