using UnityEngine;

public class OnBuildingBuilded: ISignal
{
    public BuildingData _buildingData;
    public Vector2Int _pos;

    public OnBuildingBuilded(BuildingData buildingData, Vector2Int pos)
    {
        _buildingData = buildingData;
        _pos = pos;
    }
}