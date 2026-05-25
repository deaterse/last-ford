using UnityEngine;
using System.Collections.Generic;
using System.Numerics;

public class BuildingManager : MonoBehaviour, IService
{
    private BuildingMap _buildingMap;
    private List<GameObject> _storagesInstances = new();
    private List<GameObject> _buildingInstances = new();

    public BuildingMap buildingMap => _buildingMap;
    public int _buildingsCount => _buildingInstances.Count;

    public void Init()
    {
        ServiceLocator.ProvideService<BuildingManager>(this);

        ServiceLocator.GetService<EventBus>().Subscribe<OnBuildingBuilded>(AddBuilding);
        ServiceLocator.GetService<EventBus>().Subscribe<OnTerrainMapGenerated>(GenerateBuildingMap);
        ServiceLocator.GetService<EventBus>().Subscribe<TryUpdateBuilding>(TryToUpgrade);
        ServiceLocator.GetService<EventBus>().Subscribe<TryRemoveBuilding>(TryToRemove);

        ServiceLocator.GetService<EventBus>().Subscribe<OnEmptyClicked>(HideAllUIs);
    }

    private void OnDisable()
    {
        ServiceLocator.GetService<EventBus>().Unsubscribe<OnBuildingBuilded>(AddBuilding);
        ServiceLocator.GetService<EventBus>().Unsubscribe<OnTerrainMapGenerated>(GenerateBuildingMap);
        ServiceLocator.GetService<EventBus>().Unsubscribe<TryUpdateBuilding>(TryToUpgrade);
        ServiceLocator.GetService<EventBus>().Unsubscribe<TryRemoveBuilding>(TryToRemove);

        ServiceLocator.GetService<EventBus>().Unsubscribe<OnEmptyClicked>(HideAllUIs);
    }

    private void GenerateBuildingMap(OnTerrainMapGenerated signal)
    {
        TerrainMap terrainMap = signal._terrainMap;

        _buildingMap = new BuildingMap(terrainMap.Width, terrainMap.Height);
    }

    public bool CanPlaceBuilding(Vector2Int pos, Vector3Int[] sizeArray)
    {
        return _buildingMap.CanPlaceBuilding(pos, sizeArray);
    }

    public Vector2Int GetBuidlingPos(int index)
    {
        return _buildingInstances[index].GetComponent<Building>().GridPosition;
    }

    public void AddBuilding(OnBuildingBuilded signal)
    {
        Debug.Log("Called");
        BuildingData data;
        Building building;

        GameObject buildingObj = signal._building.gameObject;
        Vector2Int buildingPos = signal._building.GridPosition; 
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

        _buildingMap.PlaceBuilding(buildingPos, data.BuildingSize, building);
        
        if (_buildingInstances.Contains(buildingObj))
        {
            Debug.LogWarning("Building already added to BuildingManager!");
            return;
        }
        
        _buildingInstances.Add(buildingObj);
        if(buildingObj.TryGetComponent<Storage>(out Storage _storage))
        {
            _storagesInstances.Add(buildingObj);
        }
    }

    public Vector3Int GetNearestStorage(Vector3Int pos)
    {
        if(_storagesInstances.Count > 0)
        {
            Vector2Int pos2int = _storagesInstances[Random.Range(0, _storagesInstances.Count)].GetComponent<Building>().GridPosition;
            Vector3Int pos3int = new Vector3Int(pos2int.x, pos2int.y, 0);

            return pos3int;
        }

        return new Vector3Int(-1,-1,-1);
    }

    private void TryToUpgrade(TryUpdateBuilding signal)
    {
        BuildingData buildingData = signal._building.buildingData;
        
        if(signal._building.Level < buildingData.MaxLevel)
        {
            List<ResourceAmount> upgradeCost = buildingData.UpgradeCost;
            
            foreach(ResourceAmount bc in upgradeCost)
            {
                bool enoughRes = ServiceLocator.GetService<ResourceManager>().IsResourceEnough(bc.Type, bc.Amount);
                
                if(!enoughRes)
                {
                    Debug.Log("U dont have enough resouce to upgrade building.");
                    return;
                }
            }

            foreach(ResourceAmount bc in upgradeCost)
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
            building.DestroyMethod();

            Debug.Log("Building successfully destroyed.");

            return;
        }
    }

    private void HideAllUIs(OnEmptyClicked signal)
    {
        foreach(GameObject b in _buildingInstances)
        {
            if(b.TryGetComponent<BuildingUI>(out BuildingUI bui))
            {
                bui.HideUI();
            }
        }
    }
}
