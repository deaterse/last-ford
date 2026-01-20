using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Building", menuName = "Scriptable Objects/BuildingSystem/Building")]
public class BuildingData : ScriptableObject
{
    [SerializeField] private string _displayedName;
    [SerializeField] private int _buldingID;
    [SerializeField] private bool _buildRoad;
    [SerializeField] private Vector2Int _buildingSize = new Vector2Int(1, 1);

    [Header("Prefabs")]
    [SerializeField] private TileBase _tilePrefab;
    [SerializeField] private GameObject _objPrefab;

    public TileBase TilePrefab => _tilePrefab;
    public GameObject ObjPrefab => _objPrefab;

    public bool BuildRoad => _buildRoad;
    public Vector2Int BuildingSize => _buildingSize;
}
