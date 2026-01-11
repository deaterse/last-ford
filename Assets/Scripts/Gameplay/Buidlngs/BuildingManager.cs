using UnityEngine;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    private Dictionary<Vector2Int, Building> _allBuildings = new Dictionary<Vector2Int, Building>();

    private void Start()
    {
        Instance = this;
    }

    public void AddBuilding(BuildingData _data, Vector2Int pos, Building newBuilding)
    {
        _allBuildings[pos] = newBuilding;

        // if(_allBuildings.Count > 1)
        // {
        //     Vector3Int start = BuildSystem.Instance.RoadsTilemap.WorldToCell(newBuilding.transform.position);
        //     Building endBuilding = GetNearestBuilding(start);

        //     Vector3Int end = BuildSystem.Instance.RoadsTilemap.WorldToCell(endBuilding.transform.position);

        //     if(_data.BuildRoad)
        //     {
        //         BuildSystem.Instance.BuildRoad(start, end);
        //     }
        // }
    }

    public bool IsEmpty(Vector2Int pos)
    {
        return !_allBuildings.ContainsKey(pos);
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
