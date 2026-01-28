using UnityEngine;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour
{
    //remove singleton
    public static BuildingManager Instance { get; private set; }

    private BuildingMap _buildingMap;
    private Dictionary<BuildingData, List<GameObject>> _buildingInstances = new();

    public void Init()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        ServiceLocator.GetService<EventBus>().Subscribe<OnTerrainMapGenerated>(GenerateBuildingMap);

        Instance = this;
    }

    private void OnDisable()
    {
        ServiceLocator.GetService<EventBus>().Unsubscribe<OnTerrainMapGenerated>(GenerateBuildingMap);
    }

    private void GenerateBuildingMap(OnTerrainMapGenerated signal)
    {
        TerrainMap terrainMap = signal._terrainMap;

        _buildingMap = new BuildingMap(terrainMap.Width, terrainMap.Height);
    }

    public bool CanPlaceBuilding(Vector2Int pos, Vector2Int size)
    {
        return _buildingMap.CanPlaceBuilding(pos, size);
    }

    public void AddBuilding(BuildingData data, Vector2Int pos, GameObject buildingObj)
    {
        _buildingMap.PlaceBuilding(pos, data.BuildingSize, data);
        
        if (!_buildingInstances.ContainsKey(data))
            _buildingInstances[data] = new List<GameObject>();
            
        _buildingInstances[data].Add(buildingObj);
    }



    
    // public Building GetNearestBuilding(Vector3Int start)
    // {
    //     Building _nearestBuilding = null;
    //     float _minDistance = 1000;

    //     foreach(Building b in _allBuildings)
    //     {
    //         Vector3Int cellPos = BuildSystem.Instance.BuildingsTilemap.WorldToCell(b.transform.position);

    //         float distance = Vector3Int.Distance(start, cellPos);
    //         if(distance < _minDistance && start != cellPos)
    //         {
    //             _minDistance = distance;
    //             _nearestBuilding = b;
    //         }
    //     }

    //     return _nearestBuilding;
    // }
}
