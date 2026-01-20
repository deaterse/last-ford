using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class BuildSystem : MonoBehaviour
{
    // remove singlton later (now no scripts using it)
    public static BuildSystem Instance { get; private set; }

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
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        GameEvents.OnTerrainMapGenerated += SetTerrainMap;
        GameEvents.OnInputBuildingBuilded += PlacePrefab;
    }

    private void OnDisable()
    {
        GameEvents.OnTerrainMapGenerated -= SetTerrainMap;
        GameEvents.OnInputBuildingBuilded -= PlacePrefab;
    }

    private void SetTerrainMap(TerrainMap terrainMap)
    {
        _terrainMap = terrainMap;
    }

    private void Update()
    {
        if(_currentPrefab != null)
        {
            _isBuilding = true;

            Vector3 centredCellPos = _buildingsTilemap.GetCellCenterWorld(MousePosOnTile());
            _currentPrefab.transform.position = centredCellPos;

            _canBuild = IsPlaceEmpty();

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

    private bool IsPlaceEmpty()
    {
        Vector3Int mousePos = MousePosOnTile();
        Vector2Int startPos = new Vector2Int(mousePos.x - (_currentData.BuildingSize.x / 2), mousePos.y - (_currentData.BuildingSize.y / 2));

        if(_terrainMap.CanBuild(startPos, _currentData.BuildingSize) && BuildingManager.Instance.CanPlaceBuilding(startPos, _currentData.BuildingSize))
        {
            ChangeBuildingColor(_canBuildColor);
            return true;
        }
        else
        {
            ChangeBuildingColor(_cantBuildColor);
            return false;
        }
    }

    public void StartBuilding(BuildingData _buildingData)
    {
        ClearCurrent();

        GameObject _buildingPrefab = _buildingData.ObjPrefab;

        _currentPrefab = Instantiate(_buildingPrefab, MousePosOnTile(), Quaternion.identity);
        _currentData = _buildingData;
    }

    private void PlacePrefab()
    {
        if(_currentPrefab != null && _canBuild)
        {
            Vector3Int cellMousePos = MousePosOnTile();
            Vector2Int startPos;
            
            if(_currentData.BuildingSize.x > 1 && _currentData.BuildingSize.y > 1)
            {
                startPos = new Vector2Int(cellMousePos.x - (_currentData.BuildingSize.x / 2), cellMousePos.y - (_currentData.BuildingSize.y / 2));
            }
            else
            {
                startPos = (Vector2Int) cellMousePos;
            }

            if (!BuildingManager.Instance.CanPlaceBuilding(startPos, _currentData.BuildingSize) || !_terrainMap.CanBuild(startPos, _currentData.BuildingSize)) return;

            GameObject buildingObj = Instantiate(_currentData.ObjPrefab);
            buildingObj.transform.position = new Vector3(cellMousePos.x + 0.5f, cellMousePos.y + 0.5f, 0);
            
            BuildingManager.Instance.AddBuilding(_currentData, startPos, buildingObj);
            
            GameEvents.InvokeOnBuildingBuilt(_currentData, startPos);
            
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
