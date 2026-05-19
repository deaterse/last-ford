using UnityEngine;

public class CanDestroyBuilding : ISignal
{
    public Building _building;
    public CanDestroyBuilding(Building building) => _building = building;
}
