using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
public class ResourcesVisualizer : MonoBehaviour
{
    [SerializeField] private List<ResourceConfig> _resourceConfigs;

    public void Visualize(TerrainMap _terrainMap, Tilemap _resourceTilemap)
    {
        _resourceTilemap.ClearAllTiles(); 

        int width = _terrainMap.Width;
        int height = _terrainMap.Height;

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                ResourceType currentResourceType = _terrainMap.TerrainData[x, y].Resource.Type;
                foreach(ResourceConfig rc in _resourceConfigs)
                {
                    if(rc.resourceType == currentResourceType)
                    {
                        _resourceTilemap.SetTile(new Vector3Int(x, y, 0), RandomTile(rc.resourceTiles));
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
