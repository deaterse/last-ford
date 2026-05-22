using UnityEngine;

public class OnBuildingBuilded: ISignal
{
    public Building _building;
    public BuildingData _buildingData;
    public Vector2Int _pos;

    public OnBuildingBuilded(Building building, BuildingData buildingData, Vector2Int pos)
    {
        _building = building;
        _buildingData = buildingData;
        _pos = pos;
    }
}