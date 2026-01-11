using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class BuildSystem : MonoBehaviour
{
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

    private void OnEnable()
    {
        GameEvents.OnTerrainMapGenerated += Init;
    }

    private void OnDisable()
    {
        GameEvents.OnTerrainMapGenerated -= Init;
    }

    private void Init(TerrainMap terrainMap)
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        _terrainMap = terrainMap;
    
        if (_terrainMap == null)
        {
            Debug.LogError("BuildSystem Get Incorrect TerrainMap!");
            enabled = false;
        }
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
            else if(Input.GetMouseButtonDown(0) && _canBuild)
            {
                PlacePrefab();
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
        Vector3Int _mousePos = MousePosOnTile();

        if(_terrainMap.CanBuild(_mousePos.x, _mousePos.y) && BuildingManager.Instance.IsEmpty((Vector2Int) _mousePos))
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
        Vector3Int cellMousePos = MousePosOnTile();

        TileBase buildingTile = _currentData.TilePrefab;

        _buildingsTilemap.SetTile(cellMousePos, buildingTile);
        GameObject _currentTile = _buildingsTilemap.GetInstantiatedObject(cellMousePos);

        BuildingManager.Instance.AddBuilding(_currentData, (Vector2Int) cellMousePos, _currentTile.gameObject.GetComponent<Building>());

        StartBuilding(_currentData);
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

    private Vector3Int MousePosOnTile()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3Int cellMousePos = _buildingsTilemap.WorldToCell(mousePosWorld);

        return cellMousePos;
    }

    private void ChangeBuildingColor(Color _color)
    {
        if(_currentPrefab != null)
        {
            SpriteRenderer _spriteRenderer = _currentPrefab.GetComponent<SpriteRenderer>();
            _spriteRenderer.color = _color;
        }
    }

}
