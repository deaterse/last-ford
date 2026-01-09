using UnityEngine;
using UnityEngine.Tilemaps;

public class HeightMapVisualizer : MonoBehaviour
{
    public void ColoringMap(Tilemap _terrainTilemap, HeightMap heightMap)
    {
        HeightColorMapper colMapper = new HeightColorMapper();
        
        for(int x = 0; x < heightMap.Width; x++)
        {  
            for(int y = 0; y < heightMap.Height; y++)
            {
                GameObject currentTileObj = _terrainTilemap.GetInstantiatedObject(new Vector3Int(x, y, 0));
                SpriteRenderer currentRenderer = currentTileObj.GetComponent<SpriteRenderer>();

                currentRenderer.color = colMapper.GetColor(heightMap.HeightData[x, y]);
            }
        }
    }
}
