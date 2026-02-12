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

        List<Vector2Int> allResourcesCell = new();
        for(int x = gridPos.x - radius; x < (gridPos.x + radius); x++)
        {
            for(int y = gridPos.y - radius; y < (gridPos.y + radius); y++)
            {
                if(_terrainMap.GetResourceType(x, y) == rt)
                {
                    List<Vector2Int> avaliableNeighbours = AvaliableNeighbours(gridPos, x, y);
                    if(avaliableNeighbours != null)
                    {
                        allResourcesCell.Add(new Vector2Int(x, y));
                    }
                }
            }
        }

        //Shuffle
        int n = allResourcesCell.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            Vector2Int value = allResourcesCell[k];
            allResourcesCell[k] = allResourcesCell[n];
            allResourcesCell[n] = value;
        }

        foreach(Vector2Int res in allResourcesCell)
        {
            List<Vector2Int> avaliableNeighbours = AvaliableNeighbours(gridPos, res.x, res.y);
            if(avaliableNeighbours != null)
            {
                Vector2Int randomNeighbour = avaliableNeighbours[Random.Range(0, avaliableNeighbours.Count)];
                ResourceNeighbour resourceNeighbour = new ResourceNeighbour(new Vector3Int(randomNeighbour.x, randomNeighbour.y, 0), new Vector3Int(res.x, res.y, 0), rt);

                return resourceNeighbour;
            }
        }

        //Change later
        Debug.Log($"Cant find Resource: {rt} \n Radius: {radius} \n Pos: {gridPos}");
        return ResourceNeighbour.None;
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