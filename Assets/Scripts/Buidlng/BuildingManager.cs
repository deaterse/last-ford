using UnityEngine;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    [SerializeField] private List<Building> _allBuildings;

    private void Start()
    {
        Instance = this;
    }

    public void AddBuilding(BuildingData _data, Building newBuilding)
    {
        _allBuildings.Add(newBuilding);

        if(_allBuildings.Count > 1)
        {
            Vector3Int start = BuildSystem.Instance.RoadsTilemap.WorldToCell(newBuilding.transform.position);
            Building endBuilding = GetNearestBuilding(start);

            Vector3Int end = BuildSystem.Instance.RoadsTilemap.WorldToCell(endBuilding.transform.position);

            if(_data.BuildRoad)
            {
                BuildSystem.Instance.BuildRoad(start, end);
            }
        }
    }

    public Building GetNearestBuilding(Vector3Int start)
    {
        Building _nearestBuilding = null;
        float _minDistance = 1000;

        foreach(Building b in _allBuildings)
        {
            Vector3Int cellPos = BuildSystem.Instance.BuildingsTilemap.WorldToCell(b.transform.position);

            float distance = Vector3Int.Distance(start, cellPos);
            if(distance < _minDistance && start != cellPos)
            {
                _minDistance = distance;
                _nearestBuilding = b;
            }
        }

        return _nearestBuilding;
    }
}
