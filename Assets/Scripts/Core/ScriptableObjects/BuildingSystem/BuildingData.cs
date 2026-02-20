using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Building", menuName = "Scriptable Objects/BuildingSystem/Building")]
public class BuildingData : ScriptableObject
{
    [Header("Type")]
    [SerializeField] private BuildingType _buildingType;

    [Header("Stats")]
    [SerializeField] private string _displayedName;
    [SerializeField] private Vector2Int _buildingSize = new Vector2Int(1, 1);
    [SerializeField] private Vector3Int[] _buildSize = new Vector3Int[3];

    [SerializeField] private float _buildingTime = 2f;
    [SerializeField] private int _miningRadius = 5;
    [SerializeField] private JobType _jobType;
    [SerializeField] private ResourceType _resourceType;

    [Header("Prices")]
    [SerializeField] private List<ResourceAmount> _buildCost;
    [SerializeField] private List<ResourceAmount> _upgradeCost;

    [Header("Levels")]
    [SerializeField] private List<BuildingLevelData> _levelsData;

    [SerializeField] private Sprite _buildingFrameSprite;


    public BuildingType buildingType => _buildingType;
    public string displayedName => _displayedName;
    public float BuildingTime => _buildingTime;
    public int MiningRadius => _miningRadius;
    public JobType jobType => _jobType;
    public ResourceType resourceType => _resourceType;

    public Vector3Int[] BuildingSize => _buildSize;

    public List<ResourceAmount> BuildCost => _buildCost;
    public List<ResourceAmount> UpgradeCost => _upgradeCost;

    public int MaxLevel => _levelsData.Count;
    public bool CanUpgrade(int currentLevel) => currentLevel < MaxLevel;
    public List<BuildingLevelData> LevelsData => _levelsData;
    
    public Sprite BuildingFrameSprite => _buildingFrameSprite;

    public BuildingLevelData GetLevel(int lvl)
    {
        int _lvl = Mathf.Abs(lvl);

        if(_lvl > MaxLevel) return null;

        return _levelsData[_lvl - 1];
    } 
}
