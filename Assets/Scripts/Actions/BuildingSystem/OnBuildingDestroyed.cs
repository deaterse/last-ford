using UnityEngine;

public class OnBuildingDestroyed: ISignal
{
    public Building _building;

    public OnBuildingDestroyed(Building building)
    {
        _building = building;
    }
}