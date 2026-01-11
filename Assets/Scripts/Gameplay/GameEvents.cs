using UnityEngine;
using System;

public static class GameEvents
{
    public static event Action<TerrainMap> OnTerrainMapGenerated;

    public static void InvokeOnTerrainMapGenerated(TerrainMap terrainMap)
    {
        OnTerrainMapGenerated?.Invoke(terrainMap);

        Debug.Log("OnTerrainMapGenerated Invoked Succesfully");
    }

    public static void ClearAllEvents()
    {
        OnTerrainMapGenerated = null;
    }
}
