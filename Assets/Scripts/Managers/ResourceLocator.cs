using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;

public class ResourceLocator: IService
{
    private TerrainMap _terrainMap;


    private Dictionary<Vector2Int, List<Vector2Int>> _allResources = new();
    private List<Vector2Int> _randomKeys;

    private List<Vector2Int> _neighbours = new();
    private List<Vector2Int> _avaliableNeighbours = new();

    private ResourceNeighbour _resourceNeighbour;


    public ResourceLocator(TerrainMap terrainMap)
    {
        _terrainMap = terrainMap;
    }

    public ResourceNeighbour GetCellNearResource(Vector2Int buildingPos, ResourceType rt, int radius)
    {
        _allResources.Clear();

        FindResources(buildingPos, rt, radius);

        ShuffleResources();

        if(_allResources.Count > 0)
        {
            Vector2Int resourcePos = _randomKeys[0];
            _avaliableNeighbours = _allResources[resourcePos];

            if(_avaliableNeighbours.Count > 0)
            {
                Vector2Int randomCell = _avaliableNeighbours[Random.Range(0, _avaliableNeighbours.Count)];
                _resourceNeighbour = new ResourceNeighbour(new Vector3Int(randomCell.x, randomCell.y, 0), new Vector3Int(resourcePos.x, resourcePos.y, 0), rt);

                return _resourceNeighbour;
            }
        }

        Debug.Log($"Cant find Resource: {rt} \n Radius: {radius} \n Pos: {buildingPos}");
        return ResourceNeighbour.None;
    }

    private void FindResources(Vector2Int buildingPos, ResourceType rt, int radius)
    {
        for(int x = buildingPos.x - radius; x < (buildingPos.x + radius + 1); x++)
        {
            for(int y = buildingPos.y - radius; y < (buildingPos.y + radius + 1); y++)
            {
                if(_terrainMap.GetResourceType(x, y) == rt)
                {
                    AvaliableNeighbours(buildingPos, x, y);
                    
                    if(_avaliableNeighbours.Count > 0)
                    {
                        _allResources[new Vector2Int(x, y)] = new List<Vector2Int>(_avaliableNeighbours);
                    }
                }
            }
        }
    }

    private void ShuffleResources()
    {
        if (_randomKeys == null)
            _randomKeys = new List<Vector2Int>(_allResources.Keys);
        else
            _randomKeys.Clear();
        
        _randomKeys.AddRange(_allResources.Keys);
        
        int n = _randomKeys.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            var temp = _randomKeys[k];
            _randomKeys[k] = _randomKeys[n];
            _randomKeys[n] = temp;
        }
    }

    private void AvaliableNeighbours(Vector2Int startPos, int x, int y)
    {
        _neighbours.Clear();
        _avaliableNeighbours.Clear();

        _neighbours.Add(new Vector2Int(x - 1, y));
        _neighbours.Add(new Vector2Int(x + 1, y));
        _neighbours.Add(new Vector2Int(x, y - 1));
        _neighbours.Add(new Vector2Int(x, y + 1));

        Vector3Int startPosConverted = new Vector3Int(startPos.x, startPos.y);

        foreach(Vector2Int n in _neighbours)
        {
            if(ServiceLocator.GetService<Pathfinder>().HasWay(startPosConverted, new Vector3Int(n.x, n.y, 0)))
            {
                _avaliableNeighbours.Add(new Vector2Int(n.x, n.y));
            }
        }
    }
}