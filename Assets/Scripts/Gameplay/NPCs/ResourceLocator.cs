using UnityEngine;
using System.Collections.Generic;

public class ResourceLocator: IService
{
    private TerrainMap _terrainMap;

    public ResourceLocator(TerrainMap terrainMap)
    {
        _terrainMap = terrainMap;
    }

    public ResourceNeighbour GetCellNearResource(Vector2Int gridPos, ResourceType rt, int radius)
    {
        int width = _terrainMap.Width;
        int height = _terrainMap.Height;

        for(int x = gridPos.x - radius; x < (gridPos.x + radius); x++)
        {
            for(int y = gridPos.y - radius; y < (gridPos.y + radius); y++)
            {
                if(_terrainMap.GetResourceType(x, y) == rt)
                {
                    List<Vector2Int> avaliableNeighbours = AvaliableNeighbours(gridPos, x, y);
                    if(avaliableNeighbours != null)
                    {
                        Vector2Int randomNeighbour = avaliableNeighbours[Random.Range(0, avaliableNeighbours.Count)];
                        Debug.Log(randomNeighbour);
                        ResourceNeighbour resourceNeighbour = new ResourceNeighbour(new Vector3Int(randomNeighbour.x, randomNeighbour.y, 0), new Vector3Int(x, y, 0));

                        return resourceNeighbour;
                    }
                }
            }
        }

        //Change later
        Debug.Log($"Cant find Resource: {rt} \n Radius: {radius} \n Pos: {gridPos}");
        return new ResourceNeighbour(new Vector3Int(0, 0, 0), new Vector3Int(0, 0, 0));
    }

    private List<Vector2Int> AvaliableNeighbours(Vector2Int buildingPos, int x, int y)
    {
        List<Vector2Int> neighbours = new();

        neighbours.Add(new Vector2Int(x - 1, y));
        neighbours.Add(new Vector2Int(x + 1, y));
        neighbours.Add(new Vector2Int(x, y - 1));
        neighbours.Add(new Vector2Int(x, y + 1));

        List<Vector2Int> avaliableCells = new();
        foreach(Vector2Int n in neighbours)
        {
            if(ServiceLocator.GetService<Pathfinder>().HasWay(new Vector3Int(buildingPos.x, buildingPos.y, 0), new Vector3Int(n.x, n.y, 0)))
            {
                avaliableCells.Add(new Vector2Int(n.x, n.y));
            }
        }

        if(avaliableCells.Count > 0) return avaliableCells;
        return null;
    }
}