using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class FillTerrain
{
    private TerrainMap _terrainMap;

    public FillTerrain(TerrainMap terrainMap)
    {
        _terrainMap = terrainMap;
    }

    public void GenerateTerrain()
    {
        int width = _terrainMap.Width;
        int height = _terrainMap.Height;

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                _terrainMap.SetTerrainType(x, y, TerrainType.Grass);
            }
        }
    }
}
