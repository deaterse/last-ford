using UnityEngine;
using System.Collections.Generic;

public abstract class ResourceGenerator
{
    protected TerrainMap _terrainMap;

    protected ResourceType _resourceType;
    protected TerrainType _terrainType;

    protected ResourceSettings _resourceSettings;
    protected int _resourcesCount;

    protected ResourceGenerator(TerrainMap terrainMap, ResourceSettings resourceSettings)
    {
        _terrainMap = terrainMap;

        _resourceSettings = resourceSettings;
        _resourceType = _resourceSettings.resourceType;
        _terrainType = _resourceSettings.terrainType;
    }

    public void Generate()
    {
        GenerateClusters();
        OnGenerationCompleted();
    }

    protected void GenerateClusters()
    {
        int clustersCount = Random.Range(_resourceSettings.MinClusterCount, _resourceSettings.MaxClusterCount);
        int clusterSize = GetClusterSize(clustersCount);

        GenerateClustersCycle(clustersCount, clusterSize);
    }

    protected virtual int GenerateClustersCycle(int clustersCount, int clusterSize)
    {
        _resourcesCount = 0;

        for(int i = 0; i < clustersCount; i++)
        {
            _resourcesCount += GenerateResourceClast(clusterSize);
        }

        return _resourcesCount;
    }

    protected abstract void OnGenerationCompleted();

    protected virtual void PlaceResource(Vector3Int pos, int subType)
    {
        _terrainMap.SetResource(
            pos.x, pos.y, 
            new Resource(
                _resourceType,
                100,
                subType
        ));
    }

    protected int GenerateResourceClast(int clusterSize, int subType = 0)
    {
        Vector3Int current = RandomPosMap();

        int placedCount = 0;
        int iterations = 0;

        if(current != Vector3Int.zero)
        {
            while (placedCount < clusterSize && iterations < clusterSize * 5)
            {
                iterations++;

                if (IsEligibleTile(current) && !HasResource(current))
                {
                    PlaceResource(current, subType);

                    placedCount++;
                }
                
                List<Vector3Int> allNeighbours = GetNeighbours(current);
                if (allNeighbours.Count == 0) break;
                
                foreach (Vector3Int neighbour in allNeighbours)
                {
                    if (placedCount >= clusterSize) break;

                    if (IsEligibleTile(neighbour))
                    {
                        if(!HasResource(neighbour))
                        {
                            PlaceResource(neighbour, subType);

                            placedCount++;
                        }
                    }
                }
                
                Vector3Int randomN = allNeighbours[Random.Range(0, allNeighbours.Count)];
                current = randomN;
            }
        }
        else
        {
            Debug.LogWarning("Didnt found any RandomPos!");
        }

        Debug.Log($"Successfully placed {_resourceType.ToString()}: {placedCount} / {clusterSize}");

        return placedCount;
    }

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
