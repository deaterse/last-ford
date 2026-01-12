using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainModifier
{
    private TerrainMap _terrainMap;
    private ResourceSpawnConfig _resourceSpawnConfig;

    public TerrainModifier(TerrainMap terrainMap, ResourceSpawnConfig resourceSpawnConfig)
    {
        _terrainMap = terrainMap;
        _resourceSpawnConfig = resourceSpawnConfig;
    }

    public void Modify()
    {
        int width = _terrainMap.Width;
        int height = _terrainMap.Height;

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if(_terrainMap.HasResource(x, y))
                {
                    ResourceSettings rs = _resourceSpawnConfig.GetResourceSettings(_terrainMap.GetResourceType(x, y));
                    if(rs.terrainType != TerrainType.None)
                    {
                        _terrainMap.SetTerrainType(x, y, rs.terrainType);
                    }
                }
            }
        }
    }
}