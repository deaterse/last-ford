using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class BuildSystem : MonoBehaviour, IService
{

    [Header("Tilemap Components")]
    [SerializeField] private Tilemap _buildingsTilemap;

    [Header("Building Colors")]
    [SerializeField] private Color _canBuildColor = Color.green;
    [SerializeField] private Color _cantBuildColor = Color.red;

    private GameObject _currentPrefab;
    private BuildingData _currentData;
    private TerrainMap _terrainMap;
    
    private bool _canBuild = true;
    private bool _isBuilding = false;

    public bool isBuilding => _isBuilding;

    public void Init()
    {
        ServiceLocator.ProvideService<BuildSystem>(this);

        ServiceLocator.GetService<EventBus>().Subscribe<OnTerrainMapGenerated>(SetTerrainMap);
        ServiceLocator.GetService<EventBus>().Subscribe<OnInputBuildingBuilded>(PlacePrefab);
    }

    private void OnDisable()
    {
        ServiceLocator.GetService<EventBus>().Unsubscribe<OnTerrainMapGenerated>(SetTerrainMap);
        ServiceLocator.GetService<EventBus>().Unsubscribe<OnInputBuildingBuilded>(PlacePrefab);
    }

    private void SetTerrainMap(OnTerrainMapGenerated signal)
    {
        _terrainMap = signal._terrainMap;
    }

    private void Update()
    {
        if(_currentPrefab != null)
        {
            _isBuilding = true;

            Vector3 centredCellPos = _buildingsTilemap.GetCellCenterWorld(MousePosOnTile());
            _currentPrefab.transform.position = centredCellPos;

            _canBuild = CanBuild();

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                ClearCurrent();
            }
        }
        else if(_currentPrefab == null && _isBuilding != false)
        {
            _isBuilding = false;
        }
    }

    private void ClearCurrent()
    {
        Destroy(_currentPrefab);

        _currentPrefab = null;
        _currentData = null;
    }

    private bool CanBuild()
    {
        bool isEmpty = IsPlaceEmpty();
        bool isResourcesEnough = IsResourcesEnough();
        
        if(isEmpty && isResourcesEnough)
        {
            ChangeBuildingColor(_canBuildColor);
            return true;
        }
        else
        {
            ChangeBuildingColor(_cantBuildColor);
        }

        return false;
    }

    private bool IsPlaceEmpty()
    {
        Vector3Int mousePos = MousePosOnTile();
        Vector2Int startPos = new Vector2Int(mousePos.x - 1, mousePos.y + 1);

        if(_terrainMap.CanBuild(startPos, _currentData.BuildingSize) && ServiceLocator.GetService<BuildingManager>().CanPlaceBuilding(startPos, _currentData.BuildingSize))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StartBuilding(BuildingData _buildingData)
    {
        ClearCurrent();

        GameObject _buildingPrefab = _buildingData.GetLevel(1).ObjPrefab;

        _currentPrefab = Instantiate(_buildingPrefab, MousePosOnTile(), Quaternion.identity);
        _currentData = _buildingData;
    }

    private bool IsResourcesEnough()
    {
        foreach(var bc in _currentData.BuildCost)
        {
            ResourceType currentResourceType = bc.Type;
            int currentPrice = bc.Amount;

            if(!ServiceLocator.GetService<ResourceManager>().IsResourceEnough(currentResourceType, currentPrice))
            {
                return false;
            }
        }
        
        return true;
    }

    public bool IsResourcesEnoughPublic(BuildingData buildingData)
    {
        foreach(var bc in buildingData.BuildCost)
        {
            ResourceType currentResourceType = bc.Type;
            int currentPrice = bc.Amount;

            if(!ServiceLocator.GetService<ResourceManager>().IsResourceEnough(currentResourceType, currentPrice))
            {
                return false;
            }
        }
        
        return true;
    }

    private void PlacePrefab(OnInputBuildingBuilded signal)
    {
        if(_currentPrefab != null && _canBuild)
        {
            bool isResourcesEnough = IsResourcesEnough();
            if(!isResourcesEnough)
            {
                Debug.Log("Player dont have enough resources.");
                return;
            }

            foreach(var bc in _currentData.BuildCost)
            {
                ResourceType currentResourceType = bc.Type;
                int currentPrice = bc.Amount;

                ServiceLocator.GetService<ResourceManager>().TrySpendResource(currentResourceType, currentPrice);
            }

            Vector3Int cellMousePos = MousePosOnTile();
            Vector2Int startPos;
            
            startPos = new Vector2Int(cellMousePos.x - 1, cellMousePos.y + 1);

            if (!ServiceLocator.GetService<BuildingManager>().CanPlaceBuilding(startPos, _currentData.BuildingSize) || !_terrainMap.CanBuild(startPos, _currentData.BuildingSize)) return;

            GameObject buildingObj = Instantiate(_currentData.GetLevel(1).ObjPrefab);
            buildingObj.transform.position = new Vector3(cellMousePos.x + 0.5f, cellMousePos.y + 0.5f, 0);

            buildingObj.GetComponent<Building>().Init(_currentData, startPos);
            
            ServiceLocator.GetService<BuildingManager>().AddBuilding(buildingObj, startPos);
            
            ServiceLocator.GetService<EventBus>().Invoke<OnBuildingBuilded>(new OnBuildingBuilded(_currentData, startPos));
            
            StartBuilding(_currentData);
        }
    }

    private Vector3Int MousePosOnTile()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3Int cellMousePos = _buildingsTilemap.WorldToCell(mousePosWorld);

        return cellMousePos;
    }

    private void ChangeBuildingColor(Color color)
    {
        if(_currentPrefab != null)
        {
            _currentPrefab.GetComponent<Building>().ChangeColor(color);
        }
    }


    // public void BuildRoad(Vector3Int start, Vector3Int end)
    // {
    //     _roadsTilemap.SetTile(start, _roadTile);
    //     _roadsTilemap.SetTile(end, _roadTile);

    //     Vector3Int startDot = new Vector3Int(start.x, start.y - 1, 0);
    //     Vector3Int endDot = new Vector3Int(end.x, end.y - 1, 0);

    //     Vector3Int delta = endDot - startDot;
    //     Vector3Int absDelta = new Vector3Int(Mathf.Abs(delta.x), Mathf.Abs(delta.y), 0);
        
    //     bool xIsMajor = absDelta.x > absDelta.y;

    //     int majorStep = xIsMajor ? absDelta.x : absDelta.y;
    //     int minorStep = xIsMajor ? absDelta.y : absDelta.x;
        
    //     int stepX = (int)Mathf.Sign(delta.x);
    //     int stepY = (int)Mathf.Sign(delta.y);
        
    //     Vector3Int currentPos = startDot;

    //     float error = 0;
    //     float errorStep = (float) minorStep / majorStep;

    //     bool stepCorner = false;
    //     for(int i = 0; i <= majorStep; i++)
    //     {
    //         if(stepCorner)
    //         {
    //             Vector3Int cornerPos;
            
    //             if(xIsMajor)
    //             {
    //                 cornerPos = new Vector3Int(currentPos.x - 1 * stepX, currentPos.y, 0);
    //             }
    //             else
    //             {
    //                 cornerPos = new Vector3Int(currentPos.x, currentPos.y - 1 * stepY, 0);
    //             }

    //             _roadsTilemap.SetTile(cornerPos, _roadTile);
    //         }
    //         stepCorner = false;

    //         _roadsTilemap.SetTile(currentPos, _roadTile);
            
    //         if(xIsMajor) currentPos.x += stepX;
    //         else currentPos.y += stepY;
            
    //         error += errorStep;
    //         if(error >= 0.5f)
    //         {
    //             if(xIsMajor) currentPos.y += stepY;
    //             else currentPos.x += stepX;
    //             error -= 1f;

    //             stepCorner = true;
    //         }
    //     }
    // }
}
