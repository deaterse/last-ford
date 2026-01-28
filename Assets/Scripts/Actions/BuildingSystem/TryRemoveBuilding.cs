using UnityEngine;

public class TryRemoveBuilding: ISignal
{
    public Building _building;

    public TryRemoveBuilding(Building building)
    {
        _building = building;
    }
}