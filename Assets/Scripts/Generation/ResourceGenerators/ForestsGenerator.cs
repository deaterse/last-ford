using UnityEngine;
using System.Collections.Generic;

public class ForestsGenerator: ResourceGenerator
{
    public ForestsGenerator(TerrainMap terrainMap, ResourceSettings resourceSettings) : base(terrainMap, resourceSettings) { }

    protected override void OnGenerationCompleted()
    {
        GameEvents.InvokeOnForestsGenerated(_resourcesCount);
    }
}
