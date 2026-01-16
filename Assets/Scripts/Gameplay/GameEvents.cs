using UnityEngine;
using System;

public static class GameEvents
{
    //Map Generation
    public static event Action<TerrainMap> OnTerrainMapGenerated;
    public static event Action<int> OnForestsGenerated;
    public static event Action<int> OnStonesGenerated;

    //Building System
    public static event Action<BuildingData, Vector2Int> OnBuildingBuilded;

    //Input System
    public static event Action<Vector2> OnInputCameraMovement;
    public static event Action<float> OnInputCameraZoom;
    public static event Action OnInputBuildingBuilded;
    public static event Action OnShowFertilityMap;

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
    
    public static void InvokeOnBuildingBuilt(BuildingData buildingData, Vector2Int pos)
    {
        OnBuildingBuilded?.Invoke(buildingData, pos);
    }

    public static void InvokeOnInputCameraMovement(Vector2 movementVector)
    {
        OnInputCameraMovement?.Invoke(movementVector);
    }

    public static void InvokeOnInputCameraZoom(float zoomStrength)
    {
        OnInputCameraZoom?.Invoke(zoomStrength);
    }

    public static void InvokeOnInputBuildingBuilded()
    {
        OnInputBuildingBuilded?.Invoke();
    }

    public static void InvokeOnShowFertilityMap()
    {
        OnShowFertilityMap?.Invoke();
    }

    public static void ClearAllEvents()
    {
        OnTerrainMapGenerated = null;
        OnForestsGenerated = null;
        OnStonesGenerated = null;
    }
}
