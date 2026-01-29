using UnityEngine;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour, IService
{
    private BuildingMap _buildingMap;
    private List<GameObject> _buildingInstances = new();

    public void Init()
    {
        ServiceLocator.ProvideService<BuildingManager>(this);

        ServiceLocator.GetService<EventBus>().Subscribe<OnTerrainMapGenerated>(GenerateBuildingMap);
        ServiceLocator.GetService<EventBus>().Subscribe<TryUpdateBuilding>(TryToUpgrade);
        ServiceLocator.GetService<EventBus>().Subscribe<TryRemoveBuilding>(TryToRemove);
    }

    private void OnDisable()
    {
        ServiceLocator.GetService<EventBus>().Unsubscribe<OnTerrainMapGenerated>(GenerateBuildingMap);
        ServiceLocator.GetService<EventBus>().Unsubscribe<TryUpdateBuilding>(TryToUpgrade);
        ServiceLocator.GetService<EventBus>().Unsubscribe<TryRemoveBuilding>(TryToRemove);
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

    public void AddBuilding(GameObject buildingObj, Vector2Int pos)
    {
        BuildingData data;
        Building building;
        if(buildingObj.TryGetComponent<Building>(out Building _building))
        {
            data = _building.buildingData;
            building = _building;
        }
        else
        {
            Debug.LogWarning("U are trying to add not a building!");
            return;
        }

        _buildingMap.PlaceBuilding(pos, data.BuildingSize, building);
        
        if (_buildingInstances.Contains(buildingObj))
        {
            Debug.LogWarning("Building already added to BuildingManager!");
            return;
        }
        
        _buildingInstances.Add(buildingObj);
    }

    private void TryToUpgrade(TryUpdateBuilding signal)
    {
        BuildingData buildingData = signal._building.buildingData;
        
        if(signal._building.Level < buildingData.MaxLevel)
        {
            List<BuildingCost> upgradeCost = buildingData.UpgradeCost;
            
            foreach(BuildingCost bc in upgradeCost)
            {
                bool enoughRes = ServiceLocator.GetService<ResourceManager>().IsResourceEnough(bc.Type, bc.Amount);
                
                if(!enoughRes)
                {
                    Debug.Log("U dont have enough resouce to upgrade building.");
                    return;
                }
            }

            foreach(BuildingCost bc in upgradeCost)
            {
                ServiceLocator.GetService<ResourceManager>().TrySpendResource(bc.Type, bc.Amount);
            }

            signal._building.Upgrade();
        }
        else
        {
            Debug.Log("This building already has reached max level.");
        }
    }

    private void TryToRemove(TryRemoveBuilding signal)
    {
        Building building = signal._building;
        BuildingData buildingData = building.buildingData;

        if(_buildingInstances.Contains(building.gameObject))
        {
            _buildingInstances.Remove(building.gameObject);
            _buildingMap.RemoveBuilding(building);
            building.Destroy();

            Debug.Log("Building successfully destroyed.");

            return;
        }
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
