using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AllBuildingsConfig", menuName = "Scriptable Objects/BuildingSystem/AllBuildings Config")]
public class AllBuildingsConfig : ScriptableObject
{
    [SerializeField] private List<BuildingData> _allBuildingsData;

    public List<BuildingData> AllBuildingsData => _allBuildingsData;
}
