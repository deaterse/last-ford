using UnityEngine;

public class OnBuildingFinished: ISignal
{
    public Building building;
    public int WorkersCount;

    public OnBuildingFinished(Building _building, int count)
    {
        building = _building;
        WorkersCount = count;
    }
}