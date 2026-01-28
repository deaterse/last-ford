using UnityEngine;

public class TryUpdateBuilding: ISignal
{
    public Building _building;

    public TryUpdateBuilding(Building building)
    {
        _building = building;
    }
}