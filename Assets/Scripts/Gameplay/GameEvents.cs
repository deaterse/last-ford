using UnityEngine;
using System;

public static class GameEvents
{
    public static event Action<TerrainMap> OnTerrainMapGenerated;
    public static event Action<int> OnForestsGenerated;
    public static event Action<int> OnStonesGenerated;

    public static void InvokeOnTerrainMapGenerated(TerrainMap terrainMap)
    {
        OnTerrainMapGenerated?.Invoke(terrainMap);

        Debug.Log("OnTerrainMapGenerated Invoked Succesfully");
    }

    public static void InvokeOnForestsGenerated(int forestsCount)
    {
        OnForestsGenerated?.Invoke(forestsCount);

        Debug.Log("OnForestsGenerated Invoked Succesfully");
    }

    public static void InvokeOnStonesGenerated(int stonesCount)
    {
        OnStonesGenerated?.Invoke(stonesCount);

        Debug.Log("OnStonesGenerated Invoked Succesfully");
    }

    public static void ClearAllEvents()
    {
        OnTerrainMapGenerated = null;
        OnForestsGenerated = null;
        OnStonesGenerated = null;
    }
}
