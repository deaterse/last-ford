using UnityEngine;
using System.Collections.Generic;

public abstract class ResourceGenerator
{
    protected TerrainMap _terrainMap;

    protected ResourceSettings _resourceSettings;

    protected ResourceGenerator(TerrainMap terrainMap, ResourceSettings resourceSettings)
    {
        _terrainMap = terrainMap;
        _resourceSettings = resourceSettings;
    }

    public abstract void Generate();

    protected List<Vector3Int> GetNeighbours(Vector3Int pos)
    {
        List<Vector3Int> allNeighbours = new List<Vector3Int>();

        allNeighbours.Add(new Vector3Int(pos.x-1, pos.y, 0));
        allNeighbours.Add(new Vector3Int(pos.x+1, pos.y, 0));
        allNeighbours.Add(new Vector3Int(pos.x, pos.y-1, 0));
        allNeighbours.Add(new Vector3Int(pos.x, pos.y+1, 0));

        return allNeighbours;
    }

    protected int GetClusterSize(int count)
    {
        return (int) ((_resourceSettings.ResourceDensity * (_terrainMap.Width * _terrainMap.Height)) / count);
    }

    protected Vector3Int RandomPosMap()
    {
        int iterations = 100;
        while(iterations > 0)
        {
            Vector3Int randomPos = new Vector3Int(Random.Range(0, _terrainMap.Width), Random.Range(0, _terrainMap.Height), 0);
            if(IsEligibleTile(randomPos) && !HasResource(randomPos))
            {
                return randomPos;
            }

            iterations--;
        }

        return Vector3Int.zero;
    }

    protected bool IsEligibleTile(Vector3Int pos) 
    {
        if(pos.x >= _terrainMap.Width || pos.y >= _terrainMap.Height || pos.x < 0 || pos.y < 0)
        {
            return false;
        }

        return _terrainMap.IsGrass(pos.x, pos.y);
    }

    protected bool HasResource(Vector3Int pos)
    {
        return _terrainMap.HasResource(pos.x, pos.y);
    }
}
