using UnityEngine;
using UnityEngine.Tilemaps;

public class ShoreVisualizer : MonoBehaviour
{
    [SerializeField] private TileBase _sandTile;
    
    public void VisualizeShore(Tilemap _terrainTilemap, TerrainMap _terrainMap)
    {
        for(int x = 0; x < _terrainMap.Width; x++)
        {
            for(int y = 0; y < _terrainMap.Height; y++)
            {
                if(_terrainMap.TerrainData[x, y].Type == TerrainType.Sand)
                {
                    _terrainTilemap.SetTile(new Vector3Int(x, y, 0), _sandTile);
                }
            }
        }
    }
}
