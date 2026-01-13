using UnityEngine;
using System.Collections.Generic;

public class ForestsGenerator: ResourceGenerator
{
    private int _typesCount;

    public ForestsGenerator(TerrainMap terrainMap, ResourceSettings resourceSettings, int typesCount) : base(terrainMap, resourceSettings)
    {
        _typesCount = typesCount;
    }

    protected override void OnGenerationCompleted()
    {
        GameEvents.InvokeOnForestsGenerated(_resourcesCount);
    }

    protected override int GenerateClustersCycle(int clustersCount, int clusterSize)
    {
        _resourcesCount = 0;

        for(int i = 0; i < clustersCount; i++)
        {
            int randomType = Random.Range(0, _typesCount);
            
            _resourcesCount += GenerateResourceClast(clusterSize, randomType);
        }

        return _resourcesCount;
    }
}
