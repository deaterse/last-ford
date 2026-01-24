using UnityEngine;

public class OnTerrainMapGenerated: ISignal
{
    public TerrainMap _terrainMap;
    public OnTerrainMapGenerated(TerrainMap terrainMap) => _terrainMap = terrainMap;
}