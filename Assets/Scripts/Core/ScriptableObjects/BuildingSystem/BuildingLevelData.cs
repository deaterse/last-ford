using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BuildingLevel", menuName = "Scriptable Objects/BuildingSystem/Building Level")]
public class BuildingLevelData : ScriptableObject
{
    [SerializeField] private BuildingData _buildingData;
    [SerializeField] private int _level;

    [Header("Visual")]
    [SerializeField] private TileBase _tilePrefab;
    [SerializeField] private GameObject _objPrefab;

    [SerializeField] private int _workerSlots;
    [SerializeField] private float _productionRate;

    //UI
    public BuildingData buildingData => _buildingData;
    public int Level => _level; 

    public TileBase TilePrefab => _tilePrefab;
    public GameObject ObjPrefab => _objPrefab;

    public int WorkerSlots => _workerSlots;
    public float ProductionRate => _productionRate;
}
