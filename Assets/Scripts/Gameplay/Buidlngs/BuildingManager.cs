using UnityEngine;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    private BuildingMap _buildingMap;
    private Dictionary<BuildingData, List<GameObject>> _buildingInstances = new();

    public void Init(int width, int height)
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _buildingMap = new BuildingMap(width, height);
        Instance = this;
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
