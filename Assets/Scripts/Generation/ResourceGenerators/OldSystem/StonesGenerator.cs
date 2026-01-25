using UnityEngine;
using System.Collections.Generic;

public class StonesGenerator: ResourceGenerator
{
    public StonesGenerator(TerrainMap terrainMap, ResourceSettings resourceSettings) : base(terrainMap, resourceSettings) { }

    protected override void OnGenerationCompleted()
    {
        ServiceLocator.GetService<EventBus>().Invoke<OnStonesGenerated>(new OnStonesGenerated(_resourcesCount));
    }
}