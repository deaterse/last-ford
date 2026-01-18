using UnityEngine;
using System.Collections.Generic;

public class ShoreGenerator
{
    private TerrainMap _terrainMap;

    public ShoreGenerator(TerrainMap terrainMap)
    {
        _terrainMap = terrainMap;
    }

    public void GenerateShore()
    {
        for(int x = 0; x < _terrainMap.Width; x++)
        {
            for(int y = 0; y < _terrainMap.Height; y++)
            {
                List<Vector2Int> allNeighbours = GetNeighbours(x, y);

                foreach(Vector2Int neighbour in allNeighbours)
                {
                    if(neighbour.x >= 0 && neighbour.x < _terrainMap.Width && neighbour.y >= 0 && neighbour.y < _terrainMap.Height)
                    {
                        if(_terrainMap.IsWater(neighbour.x, neighbour.y))
                        {
                            TileData sandTile = new TileData(
                                TerrainType.Sand,
                                Resource.None
                            );

                            _terrainMap.SetTile(x, y, sandTile);
                        }
                    }
                }
            }
        }
    }

    private List<Vector2Int> GetNeighbours(int x, int y)
    {
        List<Vector2Int> allNeighbours = new List<Vector2Int>();

        if(x >= 0 && y >= 0 && x < _terrainMap.Width && y < _terrainMap.Height)
        {
            TerrainType currentType = _terrainMap.GetTerrainType(x, y);

            if(currentType != TerrainType.Water)
            {
                Vector2Int n1 = new Vector2Int(x + 1, y);
                Vector2Int n2 = new Vector2Int(x + 1, y + 1);
                Vector2Int n3 = new Vector2Int(x - 1, y - 1);
                Vector2Int n4 = new Vector2Int(x + 1, y - 1);
                Vector2Int n5 = new Vector2Int(x - 1, y + 1);
                Vector2Int n6 = new Vector2Int(x, y + 1);
                Vector2Int n7 = new Vector2Int(x, y - 1);
                Vector2Int n8 = new Vector2Int(x - 1, y);

                allNeighbours.Add(n1);
                allNeighbours.Add(n2);
                allNeighbours.Add(n3);
                allNeighbours.Add(n4);
                allNeighbours.Add(n5);
                allNeighbours.Add(n6);
                allNeighbours.Add(n7);
                allNeighbours.Add(n8);
            }
        }

        return allNeighbours;
    }
}
