using UnityEngine;
using System.Collections.Generic;

public class ForestsGenerator: ResourceGenerator
{
    private int _typesCount;

    public ForestsGenerator(TerrainMap terrainMap, ResourceSettings resourceSettings, ResourcesSubtypeConfig resourceSubtypeConfig) : base(terrainMap, resourceSettings)
    {
        _typesCount = resourceSubtypeConfig.GetCountFromResourceType(resourceSettings.resourceType);
    }

    protected override void OnGenerationCompleted()
    {
        ServiceLocator.GetService<EventBus>().Invoke<OnForestsGenerated>(new OnForestsGenerated(_resourcesCount));
    }

    protected override void GenerateClustersCycle(int clustersCount, int clusterSize)
    {
        _resourcesCount = 0;

        for(int i = 0; i < clustersCount; i++)
        {
            int randomType = Random.Range(0, _typesCount);
            
            _resourcesCount += GenerateResourceClast(clusterSize, randomType);
        }
    }
}
