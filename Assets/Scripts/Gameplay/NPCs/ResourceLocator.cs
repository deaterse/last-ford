using UnityEngine;
using System.Collections.Generic;

public class ResourceLocator: IService
{
    private TerrainMap _terrainMap;

    public ResourceLocator(TerrainMap terrainMap)
    {
        _terrainMap = terrainMap;
    }

    public Vector3Int GetNearestResource(Vector2Int gridPos, ResourceType rt, int radius)
    {
        int width = _terrainMap.Width;
        int height = _terrainMap.Height;

        for(int x = gridPos.x - radius; x < (gridPos.x + radius); x++)
        {
            for(int y = gridPos.y - radius; y < (gridPos.y + radius); y++)
            {
                if(_terrainMap.GetResourceType(x, y) == rt)
                {
                    List<Vector2Int> avaliableNeighbours = AvaliableNeighbours(x, y);
                    if(avaliableNeighbours != null)
                    {
                        Vector2Int randomNeighbour = avaliableNeighbours[Random.Range(0, avaliableNeighbours.Count)];
                        return new Vector3Int(randomNeighbour.x, randomNeighbour.y, 0);
                    }
                }
            }
        }

        //Change later
        Debug.Log($"Cant find Resource: {rt} \n Radius: {radius} \n Pos: {gridPos}");
        return new Vector3Int(0, 0, 0);
    }

    private List<Vector2Int> AvaliableNeighbours(int x, int y)
    {
        //change later, can have problems (change using pathfinder)
        List<Vector2Int> neighbours = new();

        neighbours.Add(new Vector2Int(x - 1, y));
        neighbours.Add(new Vector2Int(x + 1, y));
        neighbours.Add(new Vector2Int(x, y - 1));
        neighbours.Add(new Vector2Int(x, y + 1));

        List<Vector2Int> avaliableCells = new();
        foreach(Vector2Int n in neighbours)
        {
            if(_terrainMap.IsWalkable(n.x, n.y))
            {
                avaliableCells.Add(new Vector2Int(n.x, n.y));
            }
        }

        if(avaliableCells.Count > 0) return avaliableCells;
        return null;
    }
}