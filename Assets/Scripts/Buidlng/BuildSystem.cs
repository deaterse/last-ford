using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class BuildSystem : MonoBehaviour
{
    public static BuildSystem Instance { get; private set; }

    [SerializeField] private Tilemap _buildingsTilemap;
    [SerializeField] private Tilemap _resourceTilemap;
    [SerializeField] private Tilemap _waterTilemap;
    [SerializeField] private Tilemap _roadsTilemap;

    public Tilemap RoadsTilemap => _roadsTilemap;
    public Tilemap BuildingsTilemap => _buildingsTilemap;

    [SerializeField] private TileBase _roadTile;

    [Header("Building Colors")]
    [SerializeField] private Color _canBuildColor = Color.green;
    [SerializeField] private Color _cantBuildColor = Color.red;

    private GameObject _currentPrefab;
    private BuildingData _currentData;
    
    private bool _canBuild = true;
    private bool _buiding = false;

    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        if(_currentPrefab != null)
        {
            _buiding = true;
            Vector3 centredCellPos = _buildingsTilemap.GetCellCenterWorld(MousePositionOnTile());
            _currentPrefab.transform.position = centredCellPos;

            _canBuild = IsPlaceEmpty();

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(_currentPrefab);

                _currentPrefab = null;
                _currentData = null;
            }
            else if(Input.GetMouseButtonDown(0) && _canBuild)
            {
                PlacePrefab();
            }
        }
        else
        {
            _buiding = false;
        }
    }

    public bool isBuilding()
    {
        return _buiding;
    }

    private bool IsPlaceEmpty()
    {
        Vector3Int _mousePos = MousePositionOnTile();

        TileBase _currentTile = _buildingsTilemap.GetTile(_mousePos);
        TileBase _resourceTile = _resourceTilemap.GetTile(_mousePos);
        TileBase _waterTile = _waterTilemap.GetTile(_mousePos);

        if(_currentTile == null && _resourceTile == null && _waterTile == null)
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
        Destroy(_currentPrefab);
        
        _currentPrefab = null;
        _currentData = null;

        GameObject _buildingPrefab = _buildingData.ObjPrefab;

        _currentPrefab = Instantiate(_buildingPrefab, MousePositionOnTile(), Quaternion.identity);
        _currentData = _buildingData;

        ChangeBuildingColor(_canBuildColor);
    }

    private void ChangeBuildingColor(Color _color)
    {
        if(_currentPrefab != null)
        {
            SpriteRenderer _spriteRenderer = _currentPrefab.GetComponent<SpriteRenderer>();
            _spriteRenderer.color = _color;
        }
    }

    public void BuildRoad(Vector3Int start, Vector3Int end)
    {
        _roadsTilemap.SetTile(start, _roadTile);
        _roadsTilemap.SetTile(end, _roadTile);

        Vector3Int startDot = new Vector3Int(start.x, start.y - 1, 0);
        Vector3Int endDot = new Vector3Int(end.x, end.y - 1, 0);

        Vector3Int delta = endDot - startDot;
        Vector3Int absDelta = new Vector3Int(Mathf.Abs(delta.x), Mathf.Abs(delta.y), 0);
        
        bool xIsMajor = absDelta.x > absDelta.y;

        int majorStep = xIsMajor ? absDelta.x : absDelta.y;
        int minorStep = xIsMajor ? absDelta.y : absDelta.x;
        
        int stepX = (int)Mathf.Sign(delta.x);
        int stepY = (int)Mathf.Sign(delta.y);
        
        Vector3Int currentPos = startDot;

        float error = 0;
        float errorStep = (float) minorStep / majorStep;

        bool stepCorner = false;
        for(int i = 0; i <= majorStep; i++)
        {
            if(stepCorner)
            {
                Vector3Int cornerPos;
            
                if(xIsMajor)
                {
                    cornerPos = new Vector3Int(currentPos.x - 1 * stepX, currentPos.y, 0);
                }
                else
                {
                    cornerPos = new Vector3Int(currentPos.x, currentPos.y - 1 * stepY, 0);
                }

                _roadsTilemap.SetTile(cornerPos, _roadTile);
            }
            stepCorner = false;

            _roadsTilemap.SetTile(currentPos, _roadTile);
            
            if(xIsMajor) currentPos.x += stepX;
            else currentPos.y += stepY;
            
            error += errorStep;
            if(error >= 0.5f)
            {
                if(xIsMajor) currentPos.y += stepY;
                else currentPos.x += stepX;
                error -= 1f;

                stepCorner = true;
            }
        }
    }

    private Vector3Int MousePositionOnTile()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3Int cellMousePos = _buildingsTilemap.WorldToCell(mousePosWorld);

        return cellMousePos;
    }

    private void PlacePrefab()
    {
        Vector3Int cellMousePos = MousePositionOnTile();

        TileBase buildingTile = _currentData.TilePrefab;

        _buildingsTilemap.SetTile(cellMousePos, buildingTile);
        GameObject _currentTile = _buildingsTilemap.GetInstantiatedObject(cellMousePos);

        BuildingManager.Instance.AddBuilding(_currentData, _currentTile.gameObject.GetComponent<Building>());

        _currentPrefab = null;
        
        StartBuilding(_currentData);
    }
}
