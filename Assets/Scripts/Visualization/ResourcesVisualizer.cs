using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
public class ResourcesVisualizer : MonoBehaviour
{

    public void Visualize(TerrainMap _terrainMap, Tilemap _resourceTilemap, ResourcesSubtypeConfig _resourcesSubtypeConfig)
    {
        int width = _terrainMap.Width;
        int height = _terrainMap.Height;

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                ResourceType currentResourceType = _terrainMap.TerrainData[x, y].Resource.Type;
                int currentSubType = _terrainMap.TerrainData[x, y].Resource.SubType;

                foreach(ResourceTypesConfig rc in _resourcesSubtypeConfig.TypesConfig)
                {
                    if(rc.ResourceType == currentResourceType)
                    {
                        List<TileBase> resourceTiles = _resourcesSubtypeConfig.GetTypesFromResourceType(currentResourceType).GetTilesByIndex(currentSubType);

                        _resourceTilemap.SetTile(new Vector3Int(x, y, 0), RandomTile(resourceTiles));
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
