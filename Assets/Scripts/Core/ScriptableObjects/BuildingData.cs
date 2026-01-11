using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Building", menuName = "Scriptable Objects/Building")]
public class BuildingData : ScriptableObject
{
    [SerializeField] private string _displayedName;
    [SerializeField] private int _buldingID;
    [SerializeField] private bool _buildRoad;

    [Header("Prefabs")]
    [SerializeField] private GameObject _objPrefab;
    [SerializeField] private TileBase _tilePrefab;

    public GameObject ObjPrefab => _objPrefab;
    public TileBase TilePrefab => _tilePrefab;
    public bool BuildRoad => _buildRoad;
}
