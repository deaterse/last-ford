using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Building", menuName = "Scriptable Objects/BuildingSystem/Building")]
public class BuildingData : ScriptableObject
{
    [SerializeField] private BuildingType _buildingType;
    [SerializeField] private string _displayedName;
    [SerializeField] private int _buldingID;
    [SerializeField] private bool _buildRoad;
    [SerializeField] private Vector2Int _buildingSize = new Vector2Int(1, 1);

    [Header("Prefabs")]
    [SerializeField] private TileBase _tilePrefab;
    [SerializeField] private GameObject _objPrefab;
    [Header("Needed Resources")]
    [SerializeField] private List<ResourceType> _neededResourcesTypesList;
    [SerializeField] private List<int> _neededResourcesIntsList;

    //UI
    public BuildingType buildingType => _buildingType;
    public string displayedName => _displayedName;

    public TileBase TilePrefab => _tilePrefab;
    public GameObject ObjPrefab => _objPrefab;

    public bool BuildRoad => _buildRoad;
    public Vector2Int BuildingSize => _buildingSize;

    public List<ResourceType> NeededResourcesTypesList => _neededResourcesTypesList;
    public List<int> NeededResourcesIntsList => _neededResourcesIntsList;
}
