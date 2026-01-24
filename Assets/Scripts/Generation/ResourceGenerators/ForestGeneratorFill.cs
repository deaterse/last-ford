using UnityEngine;
using System.Collections.Generic;

public class ForestsGeneratorFill: ResourceGenerator
{
    public int _typesCount;

    public ForestsGeneratorFill(TerrainMap terrainMap, ResourceSettings resourceSettings, ResourcesSubtypeConfig resourceSubtypeConfig) : base(terrainMap, resourceSettings)
    {
        _typesCount = resourceSubtypeConfig.GetCountFromResourceType(resourceSettings.resourceType);
    }

    public override void Generate()
    {
        GenerateClustersForest();
        FillMap();

        OnGenerationCompleted();
    }

    private void GenerateClustersForest()
    {
        int clustersCount = Random.Range(_resourceSettings.MinClusterCount, _resourceSettings.MaxClusterCount);
        int clusterSize = GetClusterSize(clustersCount);

        _resourcesCount = 0;

        for (int i = 0; i < clustersCount; i++)
        {
            int clusterType = Random.Range(0, _typesCount);
            _resourcesCount += GenerateResourceClast(clusterSize, clusterType);
        }
    }

    private void FillMap()
    {
        int width = _terrainMap.Width;
        int height = _terrainMap.Height;

        int randomType = Random.Range(0, _typesCount);

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if(IsSuitableForForest(x, y))
                {
                    _terrainMap.SetResource(x, y,
                    new Resource(
                        ResourceType.Wood, 
                        100,
                        randomType
                    ));

                    _resourcesCount++;
                }
            }
        }
    }

    protected override void OnGenerationCompleted()
    {
        ServiceLocator.GetEventBus().Invoke<OnForestsGenerated>(new OnForestsGenerated(_resourcesCount));
    }

    private bool IsSuitableForForest(int x, int y)
    {
        return _terrainMap.IsGrass(x, y) && !_terrainMap.HasResource(x, y);
    }
}
